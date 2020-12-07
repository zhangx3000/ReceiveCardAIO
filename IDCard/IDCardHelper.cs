using ArcFaceSharp.Model;
using ArcFaceSharp.Util;
using IDCard.Model;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;

namespace IDCard
{
    /// <summary>
    /// ID卡身份证帮助类
    /// </summary>
    public class IDCardHelper
    {
        public static bool IsConnected = false;

        public static bool IsAuthenticate = false;

        public static bool IsRead_Content = false;
        public static int Port = 0;
        public static int ComPort = 0;
        public const int cbDataSize = 128;
        public const int GphotoSize = 256 * 1024;

        //读卡的状态标志位
        private int ret = 0;
        private int nRet = 0;
        //可执行程序所在的目录
        public static string AvatarPath { get; set; }

        //读卡器获得的信息
        public IDCardInfo idInfo = null;

        //提取特征值IntPtr
        public static IntPtr pImageEngine = IntPtr.Zero;

        //服务线程
        private Thread serviceTread;
        public void DoService()
        {
            while (true)
            {
                AlwaysReader();
                //线程休眠10S
                Thread.Sleep(200);
            }
        }
        /// <summary>
        /// 关闭服务
        /// </summary>
        public void CloseService()
        {
            serviceTread.Abort();
            while(true)
            {
                if(serviceTread.ThreadState!=ThreadState.Aborted)
                {
                    Thread.Sleep(100);
                }else
                {
                    break;
                }
            }
        }

        public IDCardHelper()
        {
            int AutoSearchReader = IDCardLib.InitCommExt();
            if (AutoSearchReader > 0)
            {
                Port = AutoSearchReader;
                IsConnected = true;
                StringBuilder sb = new StringBuilder();
                IDCardLib.GetSAMID(sb);
                return;
            }
            else
            {
                IsConnected = false;
            }
        }
        /// <summary>
        /// /获取头像信息，并开启线程
        /// </summary>
        /// <param name="imgEngine"></param>
        /// <param name="avatarPath"></param>
        public void GetAvatarInfo(IntPtr imgEngine, string avatarPath)
        {
            pImageEngine = imgEngine;
            AvatarPath = avatarPath;

            serviceTread = new Thread(new ThreadStart(DoService));
            serviceTread.Priority = ThreadPriority.Lowest;
            serviceTread.Start();
        }
        public void AlwaysReader()
        {
            ret = 0;
            int i = 0;
            //如果身份证没有认证就会一直循环
            while ((ret = IDCardLib.Authenticate()) != 1)
            {
                i++;
                Thread.Sleep(200);
                //放卡
            }

            if (1 == ret)
            {
                //卡认证成功
                //从身份证中读出信息
                //形成文字信息文件 WZ.TXT、相片文件 XP.WLT、 ZP.BMP，如果有指纹信息形成指纹信息文件 FP.DAT
                nRet = IDCardLib.Read_Content(1);
                if (1 == nRet)
                {
                    //信息读取正确...
                    //存放相片信息
                    int dddd = IDCardLib.GetBmpPhotoExt();
                    int cbPhoto = 256 * 1024;
                    StringBuilder sbPhoto = new StringBuilder(cbPhoto);
                    int nRet1 = IDCardLib.getBMPPhotoBase64(sbPhoto, cbPhoto);
                    byte[] byPhoto = Convert.FromBase64String(sbPhoto.ToString());
                    if (nRet1 == 1)
                    {
                        MemoryStream ms = new MemoryStream(byPhoto);
                        Image img = Image.FromStream(ms);
                        //赋值内存中的身份证数据  
                        idInfo = new IDCardInfo();
                        idInfo.image = img;
                        //进行身份证的信息拿到内存中来
                        GetContentFromIDCard(idInfo);
                        //IDPbox.Image = idInfo.image;
                    }
                }
                else
                {
                    IDCardLib.CloseComm();
                    Port = IDCardLib.InitCommExt();
                }
            }
        }

        /// <summary>
        /// 从身份证获得基本信息
        /// </summary>
        /// <param name="idInfo"></param>
        /// <returns></returns>
        public int GetContentFromIDCard(IDCardInfo idInfo)
        {
            string strTemp = "";
            //将读卡生成的本地文件读取到字符串中
            string filename = AvatarPath + @"\wz.txt";
            FileStream fs = new FileStream(filename, FileMode.Open);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Unicode);
            string filecontent = sr.ReadToEnd();
            sr.Close();
            //获得姓名
            idInfo.name = filecontent.Substring(0, 15);
            //获得性别
            strTemp = filecontent.Substring(15, 1);
            if (@"1" == strTemp)
            {
                idInfo.sex = @"男";
            }
            else
            {
                idInfo.sex = @"女";
            }

            //获得民族
            strTemp = filecontent.Substring(16, 2);//nationality
            int i = Convert.ToInt32(strTemp);
            idInfo.nation = GetNation(i);

            //获得出生年月日
            strTemp = filecontent.Substring(18, 8);//birthdate
            idInfo.birthY = strTemp.Substring(0, 4);
            idInfo.birthM = strTemp.Substring(4, 2);
            idInfo.birthD = strTemp.Substring(6, 2);

            //获得住址
            idInfo.address = filecontent.Substring(26, 35);
            idInfo.newAddress = "";

            //获得身份证号码
            idInfo.ID = filecontent.Substring(61, 18);

            //获得签发机关
            idInfo.org = filecontent.Substring(79, 15);//org

            //获得有效期
            strTemp = filecontent.Substring(94, 16);//period
            idInfo.vaildity = strTemp.Substring(0, 4) + @"." + strTemp.Substring(4, 2) + @"." + strTemp.Substring(6, 2) + @" - " +
                          strTemp.Substring(8, 4) + @"." + strTemp.Substring(12, 2) + @"." + strTemp.Substring(14, 2);

            //获得证件照的特征值
            GetFeatureFromIDImage(idInfo);
            idInfo.isRight = true; //这个标志用来控制是否进行人脸比对
            return 0;
        }
        /// <summary>
        /// 从证件照中提取特征值
        /// </summary>
        /// <param name="idInfo"></param>
        /// <returns></returns>
        private IntPtr GetFeatureFromIDImage(IDCardInfo idInfo)
        {
            if (idInfo != null)
            {
                Image image = idInfo.image;
                //提取证件照的特征
                IntPtr featureTemp = GetFeatureFromImage(image);
                //messageBox.AppendText("身份证特征提取成功...\n");
                idInfo.imageFeature = featureTemp;
                return featureTemp;
            }
            else
            {
                return IntPtr.Zero;
            }
        }
        /// <summary>
        /// 从Image图片中提取特征
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        private IntPtr GetFeatureFromImage(Image img)
        {
            Image image = img;
            if (image.Width % 4 != 0)
            {
                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
            }
            ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, image);

            if (multiFaceInfo.faceNum > 0)
            {
                //裁剪照片到识别人脸的框的大小
                MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects);
                image = ImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
            }
            //提取人脸特征
            ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
            IntPtr feature = FaceUtil.ExtractFeature(pImageEngine, image, out singleFaceInfo);

            if (singleFaceInfo.faceRect.left == 0 && singleFaceInfo.faceRect.right == 0)
            {
                //messageBox.AppendText("无法提取身份证件照特征...\n");
                //无法提取身份证件照特征..
            }
            else
            {
                //成功提取到的图像特征值
                return feature;
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// 获得民族的函数
        /// </summary>
        /// <param name="nNation"></param>
        /// <returns></returns>
        private string GetNation(int nNation)
        {
            string strText = "";
            switch (nNation)
            {
                case 01:
                    {
                        strText = @"汉";
                        break;
                    }
                case 02:
                    {
                        strText = @"蒙古";
                        break;
                    }
                case 03:
                    {
                        strText = @"回";
                        break;
                    }
                case 04:
                    {
                        strText = @"藏";
                        break;
                    }
                case 05:
                    {
                        strText = @"维吾尔";
                        break;
                    }
                case 06:
                    {
                        strText = @"苗";
                        break;
                    }
                case 07:
                    {
                        strText = @"彝";
                        break;
                    }
                case 08:
                    {
                        strText = @"壮";
                        break;
                    }
                case 09:
                    {
                        strText = @"布依";
                        break;
                    }
                case 10:
                    {
                        strText = @"朝鲜";
                        break;
                    }
                case 11:
                    {
                        strText = @"满";
                        break;
                    }
                case 12:
                    {
                        strText = @"侗";
                        break;
                    }
                case 13:
                    {
                        strText = @"瑶";
                        break;
                    }
                case 14:
                    {
                        strText = @"白";
                        break;
                    }
                case 15:
                    {
                        strText = @"土家";
                        break;
                    }
                case 16:
                    {
                        strText = @"哈尼";
                        break;
                    }
                case 17:
                    {
                        strText = @"哈萨克";
                        break;
                    }
                case 18:
                    {
                        strText = @"傣";
                        break;
                    }
                case 19:
                    {
                        strText = @"黎";
                        break;
                    }
                case 20:
                    {
                        strText = @"傈僳";
                        break;
                    }
                case 21:
                    {
                        strText = @"佤";
                        break;
                    }
                case 22:
                    {
                        strText = @"畲";
                        break;
                    }
                case 23:
                    {
                        strText = @"高山";
                        break;
                    }
                case 24:
                    {
                        strText = @"拉祜";
                        break;
                    }
                case 25:
                    {
                        strText = @"水";
                        break;
                    }
                case 26:
                    {
                        strText = @"东乡";
                        break;
                    }
                case 27:
                    {
                        strText = @"纳西";
                        break;
                    }
                case 28:
                    {
                        strText = @"景颇";
                        break;
                    }
                case 29:
                    {
                        strText = @"柯尔克孜";
                        break;
                    }
                case 30:
                    {
                        strText = @"土";
                        break;
                    }
                case 31:
                    {
                        strText = @"达斡尔";
                        break;
                    }
                case 32:
                    {
                        strText = @"仫佬";
                        break;
                    }
                case 33:
                    {
                        strText = @"羌";
                        break;
                    }
                case 34:
                    {
                        strText = @"布朗";
                        break;
                    }
                case 35:
                    {
                        strText = @"撒拉";
                        break;
                    }
                case 36:
                    {
                        strText = @"毛南";
                        break;
                    }
                case 37:
                    {
                        strText = @"仡佬";
                        break;
                    }
                case 38:
                    {
                        strText = @"锡伯";
                        break;
                    }
                case 39:
                    {
                        strText = @"阿昌";
                        break;
                    }
                case 40:
                    {
                        strText = @"普米";
                        break;
                    }
                case 41:
                    {
                        strText = @"塔吉克";
                        break;
                    }
                case 42:
                    {
                        strText = @"怒";
                        break;
                    }
                case 43:
                    {
                        strText = @"乌孜别克";
                        break;
                    }
                case 44:
                    {
                        strText = @"俄罗斯";
                        break;
                    }
                case 45:
                    {
                        strText = @"鄂温克";
                        break;
                    }
                case 46:
                    {
                        strText = @"德昂";
                        break;
                    }
                case 47:
                    {
                        strText = @"保安";
                        break;
                    }
                case 48:
                    {
                        strText = @"裕固";
                        break;
                    }
                case 49:
                    {
                        strText = @"京";
                        break;
                    }
                case 50:
                    {
                        strText = @"塔塔尔";
                        break;
                    }
                case 51:
                    {
                        strText = @"独龙";
                        break;
                    }
                case 52:
                    {
                        strText = @"鄂伦春";
                        break;
                    }
                case 53:
                    {
                        strText = @"赫哲";
                        break;
                    }
                case 54:
                    {
                        strText = @"门巴";
                        break;
                    }
                case 55:
                    {
                        strText = @"珞巴";
                        break;
                    }
                case 56:
                    {
                        strText = @"基诺";
                        break;
                    }
                default:
                    break;
            }
            return strText;
        }
    }
}
