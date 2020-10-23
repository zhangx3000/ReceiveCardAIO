using CRCXMODEM;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiveCardAIO.Common
{
    public class SerialHelper
    {
        //声明串口类实例
        private static SerialPort port = null;
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="command"></param>
        public static void ExCommand(string command)
        {
            // 配置串口
            port = new SerialPort("COM3");
            port.BaudRate = 115200;
            port.DataBits = 8;
            port.Parity = Parity.Even;
            port.StopBits = StopBits.One;
            port.Open();

            // 打开
            if (port.IsOpen)
            {
                Console.WriteLine("串口打开成功");

                SendData(command, "初始化不移动卡");

                port.Close();
            }
            else
            {
                Console.WriteLine("串口打开失败");
            }
        }
        /// <summary>
        /// 发送线程
        /// </summary>
        /// <param name="comStr"></param>
        /// <param name="cmdName"></param>
        public static void SendData(string comStr, string cmdName)
        {
            byte[] cmdByteArr = StrCommon.GetByteArrayByHexStr(comStr);
            port.Write(cmdByteArr, 0, cmdByteArr.Length);
        }
    }
}
