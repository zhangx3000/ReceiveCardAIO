using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCXMODEM
{
    /// <summary>
    /// CRC-XMODEM 算法
    /// </summary>
    public class CRCCore
    {
        /// <summary>
        /// 获取CRC校验数组
        /// </summary>
        /// <param name="x"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte[] GetCrcByByteArray(byte[] x, int len) //CRC校验函数
        {
            byte[] temdata = new byte[2];
            UInt16 crc = 0;
            byte da;
            int i = 0;
            UInt16[] yu = { 0x0000,0x1021,0x2042,0x3063,0x4084,0x50a5,0x60c6,0x70e7,
                0x8108,0x9129,0xa14a,0xb16b,0xc18c,0xd1ad,0xe1ce,0xf1ef };
            while (len-- != 0)
            {
                da = (byte)(((byte)(crc / 256)) / 16);
                crc <<= 4;
                crc ^= yu[da ^ x[i] / 16];
                da = (byte)(((byte)(crc / 256)) / 16);
                crc <<= 4;
                crc ^= yu[da ^ x[i] & 0x0f];
                i++;
            }
            #region 低位在前，高位在后
            //temdata[0] = (byte)(crc & 0xFF);
            //temdata[1] = (byte)(crc >> 8);
            #endregion
            temdata[0] = (byte)(crc >> 8);
            temdata[1] = (byte)(crc & 0xFF);
            return temdata;
        }
    }
}
