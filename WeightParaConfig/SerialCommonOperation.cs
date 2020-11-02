using CRCXMODEM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightParaConfig
{
    //调用方式
    //SerialCommonOperation trans = new SerialCommonOperation("GetPoundParaInfoById", "F5CFC56B-DBB4-4B32-8538-7E71EF790BFF");
    //trans.setWeightResultValue += new setResultValue(GetValue);

    //static void GetValue(double retValue)
    //{
    //    bool isOk = FilterHelper.LimitFilterAD(retValue);
    //    Console.WriteLine(isOk);
    //    if (isOk)
    //    {
    //        Console.WriteLine(retValue);
    //    }
    //}


    // 第一步：声明一个委托。（根据自己的需求）
    public delegate void setResultValue(double retValue);
    public class SerialCommonOperation
    {
        string fail = "";
        //第二步：声明一个委托类型的事件
        public event setResultValue setWeightResultValue;
        public SerialCommonOperation(string tagName, string key)
        {
            try
            {
                //ParaInfo para = new ParaInfo();//根据业务编号，从数据查询数据
                ParaInfo para = new ParaInfo
                {
                    ReceiveDataLength = 13,//接收字节总长度
                    StartMark = "2B",//开始字节  -  或
                    EndMark = "6B670D0A",//结束字节位置
                    WeightDataLength = 8,//重量数据总长度
                    WeightDataStartBit = 0,//起始位置
                    WeightDataOrderMode = 0,//0正序，1反序
                    ConvertRatio = 1,//转换系数

                    Port = 3,
                    Baudrate = 9600,
                    DataBit = 8,
                    CheckBit = 0,
                    StopBit = 1,
                };

                Dictionary<int, string> endMarkDic = StrCommon.GetDictByHexStr(para.EndMark);
                Dictionary<int, string> startMarkDic = StrCommon.GetDictByHexStr(para.StartMark);

                SerialPortClient portClient = new SerialPortClient("COM" + para.Port, para.Baudrate.ToString(), para.CheckBit.ToString(), para.DataBit.ToString(), para.StopBit.ToString());

                portClient.Open();
                portClient.DataReceived += new Action<List<byte>>(datapool =>
                {
                    //匹配数据帧，此处以2B为帧头、6B670D0A为帧尾
                    while (true)
                    {
                        try
                        {
                            if (datapool.Count < para.ReceiveDataLength - 1) break;
                            if (datapool.Count > 1000)
                            {
                                datapool.RemoveRange(0, datapool.Count);
                                break;
                            }
                            if (datapool[0] == StrCommon.GetByteArrayByHexStr(startMarkDic.Count > 0 ? startMarkDic[0] : "")[0])
                            {
                                for (int d = 0; d < startMarkDic.Count; d++)
                                {
                                    if (datapool[d] == StrCommon.GetByteArrayByHexStr(startMarkDic[d])[0])
                                    {
                                        datapool.RemoveAt(0);
                                    }
                                }
                                continue;
                            }
                            int index = -1;//正确作法是记住上次最末尾的索引，避免重复匹配
                            for (int i = 0; i < datapool.Count - 1; i++)
                            {
                                if (datapool[i] == StrCommon.GetByteArrayByHexStr(endMarkDic.Count > 0 ? endMarkDic[0] : "")[0])
                                {
                                    index = i;
                                    break;
                                }
                            }
                            if (index != -1)
                            {
                                byte[] data = datapool.GetRange(0, index + endMarkDic.Count).ToArray();
                                datapool.RemoveRange(0, index + endMarkDic.Count);
                                //委托传值
                                string msg = StrCommon.GetHexStrByByteArray(data);
                                setWeightResultValue(GetWeightData(para.StartMark + msg, para));
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            break;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取称重数据
        /// </summary>
        /// <param name="receiveData"></param>
        /// <param name="paraInfo"></param>
        /// <returns></returns>
        public double GetWeightData(string receiveData, ParaInfo paraInfo)
        {
            double retValue = 0;
            try
            {
                receiveData = receiveData.Replace(" ", "");
                byte[] receiveByte = StrCommon.GetByteArrayByHexStr(receiveData);
                if (receiveByte.Length == paraInfo.ReceiveDataLength)//首先判断接收数据字节数长度是否匹配
                {
                    string weightData = StrCommon.MidStrMain(receiveData, paraInfo.StartMark, paraInfo.EndMark);
                    if (!string.IsNullOrEmpty(weightData))
                    {
                        byte[] weightDataByte = StrCommon.GetByteArrayByHexStr(weightData);
                        if (weightDataByte.Length == paraInfo.WeightDataLength)
                        {
                            int retLength = weightDataByte.Length - paraInfo.WeightDataStartBit;
                            if (retLength > 0)
                            {
                                byte[] new_WeightDataByte = weightDataByte.Skip(paraInfo.WeightDataStartBit).Take(retLength).ToArray();
                                string retString;
                                if (paraInfo.WeightDataOrderMode == 0)//正序
                                {
                                    retString = StrCommon.HexToStr(StrCommon.GetHexStrByByteArray(new_WeightDataByte));
                                }
                                else//反序
                                {
                                    retString = StrCommon.HexToStrReverseOrder(StrCommon.GetHexStrByByteArray(new_WeightDataByte));
                                }
                                retValue = double.Parse(retString) * paraInfo.ConvertRatio;
                            }
                        }
                        else
                        {
                            Console.WriteLine("重量数据长度和配置重量数据长度不一致：" + weightData);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("接收数据长度和配置接收数据长度不一致：" + receiveData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return retValue;
        }
    }
}
