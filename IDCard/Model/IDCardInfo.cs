using System;
using System.Drawing;

namespace IDCard.Model
{
    /// <summary>
    /// 存储从读卡器获得的个人信息
    /// </summary>
    public class IDCardInfo
    {
        public IDCardInfo()
        {
            //身份证信息的初始化
            name = "";    //姓名
            sex = "";     //性别
            nation = "";  //民族
            birthY = "";  //出生的年份
            birthM = ""; //出生的月份
            birthD = "";  //出生的日
            address = ""; //住址
            newAddress = ""; //新住址
            ID = "";      //身份证号码
            org = "";    //签发机关
            vaildity = "";  //有效期
            image = null;  //证件照
            capImage = null; //抓拍照
            imageFeature = IntPtr.Zero; //证件照的特征值
            isRight = false; //是否准备好
            isPass = 0; //通过闸机 0-未通过 1-人脸识别通过 2-指纹识别通过 
            similarity = 0.0f;  //人脸识别的相似度
        }

        public string name { get; set; }    //姓名
        public string sex { get; set; }     //性别
        public string nation { get; set; }  //民族
        public string birthY { get; set; }  //出生的年份
        public string birthM { get; set; } //出生的月份
        public string birthD { get; set; }  //出生的日
        public string address { get; set; } //住址
        public string newAddress { get; set; } //新住址
        public string ID { get; set; }      //身份证号码
        public string org { get; set; } //签发机关
        public string vaildity { get; set; } //身份证的有效期
        public Image image { get; set; } //身份证证件照
        public Bitmap capImage { get; set; } //捕捉到的抓拍照
        public IntPtr imageFeature { get; set; } //计算得到的人脸照的特征值
        public bool isRight { set; get; } //这个标志当前身份证是不是准备好
        public int isPass; //通过闸机 0-未通过 1-人脸识别通过 2-指纹识别通过 
        public float similarity { set; get; }  //存放人脸识别通过时候的相似度
    }
}
