using System;
using System.IO;

namespace ReceiveCardAIO.Common
{
    public class FileHelper
    {
        /// <summary>
        /// 删除上次生成的文件   
        /// 每次循环的时候都要删除上一次刷卡生成的照片，文件之类的东西
        /// </summary>
        /// <param name="srcPath"></param>
        public static void DeleteFile(string srcPath)
        {
            try
            {
                string filename = srcPath + @"\wz.txt";
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                filename = srcPath + @"\zp.bmp";
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                filename = srcPath + @"\xp.wlt";
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
