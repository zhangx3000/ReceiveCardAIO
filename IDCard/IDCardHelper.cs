using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IDCard
{
    public class IDCardHelper
    {
        public static bool IsConnected = false;
        public static bool IsAuthenticate = false;
        public static bool IsRead_Content = false;
        public static int Port = 0;
        public static int ComPort = 0;
        public const int cbDataSize = 128;
        public const int GphotoSize = 256 * 1024;

        public static string AvatarPath { get; set; }

        private Thread serviceTread;
        public void DoService()
        {
            while (true)
            {
                AlwaysReader();
                //线程休眠10S
                Thread.Sleep(1000);
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

        public void GetAvatarInfo()
        {
            serviceTread = new Thread(new ThreadStart(DoService));
            serviceTread.Priority = ThreadPriority.Lowest;
            serviceTread.Start();
        }
        public void AlwaysReader()
        {
            //卡认证
            int FindCard = IDCardLib.Authenticate();
            if (FindCard != 1)
            {

            }
            else
            {
                //读卡
                int rs = IDCardLib.Read_Content(1);
                if (rs != 1 && rs != 2 && rs != 3)
                {
                    return;
                }
                //StringBuilder sb = new StringBuilder(cbDataSize);

                //号码 
                //IDCardLib.getIDNum(sb, cbDataSize);
                //string idNum = sb.ToString();

                //显示头像
                IDCardLib.GetBmpPhotoExt();
                int cbPhoto = 256 * 1024;
                StringBuilder sbPhoto = new StringBuilder(cbPhoto);
                int nRet = IDCardLib.getBMPPhotoBase64(sbPhoto, cbPhoto);
                byte[] byPhoto = Convert.FromBase64String(sbPhoto.ToString());

                if (nRet == 1)
                {
                    string pathImg = AppDomain.CurrentDomain.BaseDirectory + @"Avatar\IDCARDTMP.JPG";
                    using (var streamBitmap = new MemoryStream(byPhoto))
                    {
                        using (Image img = Image.FromStream(streamBitmap))
                        {
                            img.Save(pathImg);
                            img.Dispose();

                            AvatarPath = pathImg;
                        }

                    }
                }
            }
        }

        public void CloseComm()
        {
            IDCardLib.CloseComm();
        }
    }
}
