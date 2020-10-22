using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiveCardAIO.Common
{
    public class FileHelper
    {
        /// <summary>
        /// 删除目录下所有文件及子目录
        /// </summary>
        /// <param name="srcPath"></param>
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();//返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)//判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);//删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);//删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取目录下的所有文件
        /// </summary>
        /// <param name="srcPath">文件夹目录</param>
        /// <param name="idCardImg">身份证头像</param>
        /// <param name="captureImg">抓拍头像</param>
        /// <returns></returns>
        public static List<string> FileList(string srcPath,string idCardImg,string captureImg)
        {
            List<string> fileList = new List<string>();
            string[] Files = Directory.GetFiles(srcPath);
            foreach(string file in Files)
            {
                if (file.Contains(idCardImg)||file.Contains(captureImg))
                {
                    fileList.Add(file);
                }
            }
            return fileList;
        }
    }
}
