using ArcFaceSharp;
using ArcFaceSharp.ArcFace;
using ArcFaceSharp.Image;
using ArcFaceSharp.Model;
using IDCard;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using ReceiveCardAIO.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReceiveCardAIO
{
    // 第一步：声明一个委托。（根据自己的需求）
    public delegate void setTextValue(bool textValue);

    public partial class IdentityVerify : Form
    {
        private static bool bPlayFlag = false;
        private static VideoCapture m_vCapture;
        private static Thread ThreadCam;//摄像头视频播放线程
        private static Thread ThreadVerify;//人脸识别线程
        private static string PicSavePath = AppDomain.CurrentDomain.BaseDirectory + @"Avatar\";

        //人脸追踪引擎
        private static string APPID;
        private static string FT_SDKKEY;
        private ArcFaceCore arcFace;//人脸追踪
        private ArcFaceCore arcFaceImg;//人脸比对

        private Bitmap imgCaptureBmpTemp;
        private Bitmap imgIdCardBmpTemp;


        private IDCardHelper idCardHelper;

        //第二步：声明一个委托类型的事件
        public event setTextValue setFormTextValue;

        public IdentityVerify()
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            //读取配置文件
            AppSettingsReader reader = new AppSettingsReader();
            APPID = (string)reader.GetValue("APP_ID", typeof(string));
            FT_SDKKEY = (string)reader.GetValue("SDKKEY", typeof(string));
            if (string.IsNullOrWhiteSpace(APPID) || string.IsNullOrWhiteSpace(FT_SDKKEY))
            {
                MessageBox.Show("请在App.config配置文件中先配置APP_ID和SDKKEY64!");
                return;
            }
            try
            {
                //激活引擎    如出现错误，1.请先确认从官网下载的sdk库已放到对应的bin中，2.当前选择的CPU为x86或者x64
                // 创建 ArcFaceCore 对象，向构造函数传入相关参数进行 ArcFace 引擎的初始化
                arcFace = new ArcFaceCore(APPID, FT_SDKKEY, ArcFaceDetectMode.VIDEO,
                    ArcFaceFunction.FACE_DETECT | ArcFaceFunction.FACE_RECOGNITION | ArcFaceFunction.AGE | ArcFaceFunction.FACE_3DANGLE | ArcFaceFunction.GENDER, DetectionOrientPriority.ASF_OP_0_ONLY, 1, 16);

                if(string.IsNullOrWhiteSpace(arcFace.VersionInfo.Version))
                {
                    MessageBox.Show(string.Format("人脸追踪引擎初始化失败"), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                OpenCamera();//打开摄像头

                idCardHelper = new IDCardHelper();
                idCardHelper.GetAvatarInfo();//身份证识别,获取身份证头像
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("无法加载 DLL") > -1)
                {
                    MessageBox.Show("请将sdk相关DLL放入bin对应的x86或x64下的文件夹中!");
                }
                else
                {
                    MessageBox.Show("激活引擎失败!");
                }
                return;
            }
        }

        /// <summary>
        /// 打开摄像头
        /// </summary>
        private void OpenCamera()
        {
            if (!bPlayFlag)
            {
                m_vCapture = new VideoCapture(VideoCaptureAPIs.ANY.GetHashCode());
                if(!m_vCapture.IsOpened())
                {
                    MessageBox.Show("摄像头打不开", "摄像头故障", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                m_vCapture.Set(VideoCaptureProperties.FrameWidth, 550);//宽度
                m_vCapture.Set(VideoCaptureProperties.FrameHeight, 425);//高度

                bPlayFlag = true;
                ThreadCam = new Thread(PlayCamera);
                ThreadCam.Start();

                ThreadVerify = new Thread(VerifyAvatar);
                ThreadVerify.Start();
            }
            else
            {
                bPlayFlag = false;
                ThreadCam.Abort();
                ThreadVerify.Abort();
                m_vCapture.Release();
            }
        }
        /// <summary>
        /// 摄像头播放线程
        /// </summary>
        private void PlayCamera()
        {
            while(bPlayFlag)
            {
                Mat cFrame = new Mat();
                try
                {
                    m_vCapture.Read(cFrame);
                    int sleepTime = (int)Math.Round(1000 / m_vCapture.Fps);
                    Cv2.WaitKey(sleepTime);
                    if(cFrame.Empty())
                    {
                        continue;
                    }
                    Cv2.Flip(cFrame, cFrame, OpenCvSharp.FlipMode.Y);
                    #region 人脸追踪
                    //检测人脸，得到Rect框
                    MultiFaceModel multiFaceInfo = arcFace.FaceDetection(cFrame.ToBitmap());
                    if(multiFaceInfo.FaceInfoList.Count>0)
                    {
                        Mrect mrect = multiFaceInfo.FaceInfoList[0].faceRect;
                        Rect cMaxrect = new Rect(mrect.left, mrect.top, mrect.right - mrect.left, mrect.bottom - mrect.top);
                        //绘制指定区域(人脸框)
                        Scalar color = new Scalar(0, 0, 255);
                        Cv2.Rectangle(cFrame, cMaxrect, color, 1);
                        if(!string.IsNullOrWhiteSpace(IDCardHelper.AvatarPath))//拍照，截取追踪的人脸(如果获取到身份证照片)
                        {
                            this.pic_Capture.Image = null;
                            this.pic_IdCard.Image = null;

                            string picCap = PicSavePath + "CAPTURE.JPG";//被抓拍保存的图片名称
                            Mat cHead = new Mat(cFrame, cMaxrect);
                            Cv2.ImWrite(picCap, cHead);
                            SetPictureBoxImage(pic_Capture, cHead.ToBitmap());
                            cHead.Release();


                            Image imgCapture = Image.FromFile(picCap);
                            Bitmap imgCaptureBmp = new Bitmap(imgCapture);
                            this.pic_Capture.Image = imgCaptureBmp;
                            this.imgCaptureBmpTemp = imgCaptureBmp;
                            imgCapture.Dispose();

                            Image imgIdCard = Image.FromFile(IDCardHelper.AvatarPath);
                            Bitmap imgIdCardBmp = new Bitmap(imgIdCard);
                            this.imgIdCardBmpTemp = imgIdCardBmp;
                            this.pic_IdCard.Image = imgIdCardBmp;
                            imgIdCard.Dispose();

                            #region 注释
                            //bool ret = true;
                            //CompareAvatar(imgCaptureBmp, imgIdCardBmp, ref ret);
                            //if (ret)//如果匹配成功
                            //{
                            //    //第三步：准备相关数据。
                            //    setFormTextValue(ret);
                            //    this.Close();
                            //}
                            #endregion
                           
                            IDCardHelper.AvatarPath = "";//置空
                        }
                    }
                    multiFaceInfo.Dispose();
                    #endregion
                    SetPictureBoxImage(pic_Camera, cFrame.ToBitmap());
                    cFrame.Release();//释放   
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    cFrame.Release();//释放
                    IDCardHelper.AvatarPath = "";//置空
                }
            }
        }

        /// <summary>
        /// 人脸比对线程
        /// </summary>
        private void VerifyAvatar()
        {
            while(bPlayFlag)
            {
                bool ret = true;
                if(imgCaptureBmpTemp!=null&& imgIdCardBmpTemp != null)
                {
                    CompareAvatar(imgCaptureBmpTemp, imgIdCardBmpTemp, ref ret);
                   
                    if (ret)//如果匹配成功
                    {
                        //第三步：准备相关数据。
                        setFormTextValue(ret);
                        this.Close();
                        MemoryUtil.ClearMemory();
                    }
                    MemoryUtil.ClearMemory();
                }
            }
        }

        /// <summary>
        /// 填充每一帧画面到图片控件
        /// </summary>
        /// <param name="pic"></param>
        /// <param name="bitmap"></param>
        private void SetPictureBoxImage(PictureBox pic,Bitmap bitmap)
        {
            pic.Image = bitmap;
        }

        /// <summary>
        /// 窗口关闭时，关闭摄像头，并终止摄像头播放线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdentityVerify_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            ThreadCam.Abort();
            //上面调用Thread.Abort方法后线程thread不一定马上就被终止了，所以在这里写了个循环来做检查
            //看线程thread是否已经真正停止。其实也可以在这里使用Thread.Join方法来等待线程thread终止Thread.Join方法做的事情和我们在这里写的循环效果是一样的，都是阻塞主线程直到thread线程终止为止

            m_vCapture.Release();
            arcFace.Dispose();
            bPlayFlag = false;
            idCardHelper.CloseComm();
        }
        /// <summary>
        /// 比较人脸
        /// </summary>
        /// <param name="camImg"></param>
        /// <param name="idCardImg"></param>
        private void CompareAvatar(Bitmap camImg,Bitmap idCardImg,ref bool retCompare)
        {
            imgCaptureBmpTemp = null;
            imgIdCardBmpTemp = null;
            //图片比对
            arcFaceImg = new ArcFaceCore(APPID, FT_SDKKEY, ArcFaceDetectMode.IMAGE,
             ArcFaceFunction.FACE_DETECT | ArcFaceFunction.FACE_RECOGNITION | ArcFaceFunction.AGE | ArcFaceFunction.FACE_3DANGLE | ArcFaceFunction.GENDER, DetectionOrientPriority.ASF_OP_0_ONLY, 1, 16);
            //将第一张图片的 Bitmap 转换成 ImageData
            ImageData camImgData = ImageDataConverter.ConvertToImageData(camImg);
            ImageData idCardImgData = ImageDataConverter.ConvertToImageData(idCardImg);

            try
            {
                // 检测第一张图片中的人脸
                MultiFaceModel camImgMultiFace = arcFaceImg.FaceDetection(camImgData);
                // 取第一张图片中返回的第一个人脸信息
                AsfSingleFaceInfo camImgfaceInfo = camImgMultiFace.FaceInfoList.First();
                // 提第一张图片中返回的第一个人脸的特征
                AsfFaceFeature asfFaceFeatureCam = arcFaceImg.FaceFeatureExtract(camImgData, ref camImgfaceInfo);

                MultiFaceModel idCardImgMultiFace = arcFaceImg.FaceDetection(idCardImgData);
                AsfSingleFaceInfo idCardImgfaceInfo = idCardImgMultiFace.FaceInfoList.First();
                AsfFaceFeature asfFaceFeatureIdCard = arcFaceImg.FaceFeatureExtract(idCardImgData, ref idCardImgfaceInfo);
                float ret = arcFaceImg.FaceCompare(asfFaceFeatureCam, asfFaceFeatureIdCard);

                if (ret > 0.6)
                {
                    lbl_msg.ForeColor = Color.Green;
                    lbl_msg.Text = "人脸匹配成功--相似度：" + ret.ToString("P"); ;
                    FileHelper.DelectDir(PicSavePath);
                    retCompare= true;
                }
                else
                {
                    lbl_msg.ForeColor = Color.Red;
                    lbl_msg.Text = "人脸匹配失败--相似度：" + ret.ToString("P"); ;
                    retCompare= false;
                }
            }
            catch (Exception ex)
            {
                lbl_msg.ForeColor = Color.Red;
                lbl_msg.Text = "人脸匹配失败Ex：" + ex.Message;
                retCompare= false;
            }
            finally
            {
                //释放销毁引擎
                arcFaceImg.Dispose();
                // ImageData使用完之后记得要 Dispose 否则会导致内存溢出 
                camImgData.Dispose();
                idCardImgData.Dispose();
                // BItmap也要记得 Dispose
                camImg.Dispose();
                idCardImg.Dispose();
                //GC.Collect();
            }
        }
    }
}
