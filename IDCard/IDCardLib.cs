using System.Runtime.InteropServices;
using System.Text;

namespace IDCard
{
    /// <summary>
    /// 身份证识别
    /// </summary>
    public static class IDCardLib
    {
        /// <summary>
        /// SDK动态链接库路径
        /// </summary>
        public const string Dll_PATH = @"termb.dll";
        /// <summary>
        /// 连接身份证阅读器
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int InitComm(int port);//连接身份证阅读器 

        /// <summary>
        /// 自动搜索身份证阅读器并连接身份证阅读器
        /// </summary>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int InitCommExt();//自动搜索身份证阅读器并连接身份证阅读器 
        /// <summary>
        /// 断开与身份证阅读器连接
        /// </summary>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int CloseComm();//断开与身份证阅读器连接 
        /// <summary>
        /// 判断是否有放卡，且是否身份证
        /// </summary>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int Authenticate();//判断是否有放卡，且是否身份证 

        /// <summary>
        /// 读卡操作,信息文件存储在dll所在下
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int Read_Content(int index);//读卡操作,信息文件存储在dll所在下

        /// <summary>
        /// 读卡操作,信息文件存储在dll所在下
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int ReadContent(int index);//读卡操作,信息文件存储在dll所在下
        /// <summary>
        /// 获取SAM模块编号
        /// </summary>
        /// <param name="SAMID"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int GetSAMID(StringBuilder SAMID);//获取SAM模块编号

        /// <summary>
        /// 获取SAM模块编号（10位编号）
        /// </summary>
        /// <param name="SAMID"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int GetSAMIDEx(StringBuilder SAMID);//获取SAM模块编号（10位编号）
        /// <summary>
        /// 解析身份证照片
        /// </summary>
        /// <param name="PhotoPath"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int GetBmpPhoto(string PhotoPath);//解析身份证照片

        /// <summary>
        /// 解析身份证照片
        /// </summary>
        /// <param name="imageData"></param>
        /// <param name="cbImageData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int GetBmpPhotoToMem(byte[] imageData, int cbImageData);//解析身份证照片
        /// <summary>
        /// 解析身份证照片
        /// </summary>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int GetBmpPhotoExt();//解析身份证照片
        /// <summary>
        /// 重置Sam模块
        /// </summary>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int Reset_SAM();//重置Sam模块
        /// <summary>
        /// 获取SAM模块状态
        /// </summary>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int GetSAMStatus();//获取SAM模块状态 
        /// <summary>
        /// 解析身份证信息
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int GetCardInfo(int index, StringBuilder value);//解析身份证信息 
        /// <summary>
        /// 生成竖版身份证正反两面图片(输出目录：dll所在目录的cardv.jpg和SetCardJPGPathNameV指定路径)
        /// </summary>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int ExportCardImageV();//生成竖版身份证正反两面图片(输出目录：dll所在目录的cardv.jpg和SetCardJPGPathNameV指定路径)
        /// <summary>
        /// 生成横版身份证正反两面图片(输出目录：dll所在目录的cardh.jpg和SetCardJPGPathNameH指定路径) 
        /// </summary>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int ExportCardImageH();//生成横版身份证正反两面图片(输出目录：dll所在目录的cardh.jpg和SetCardJPGPathNameH指定路径) 
        /// <summary>
        /// 设置生成文件临时目录
        /// </summary>
        /// <param name="DirPath"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int SetTempDir(string DirPath);//设置生成文件临时目录
        /// <summary>
        /// 获取文件生成临时目录
        /// </summary>
        /// <param name="path"></param>
        /// <param name="cbPath"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int GetTempDir(StringBuilder path, int cbPath);//获取文件生成临时目录
        /// <summary>
        /// 获取jpg头像全路径名
        /// </summary>
        /// <param name="path"></param>
        /// <param name="cbPath"></param>
        [DllImport(Dll_PATH)]
        public static extern void GetPhotoJPGPathName(StringBuilder path, int cbPath);//获取jpg头像全路径名 

        /// <summary>
        /// 设置jpg头像全路径名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int SetPhotoJPGPathName(string path);//设置jpg头像全路径名
        /// <summary>
        /// 设置竖版身份证正反两面图片全路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int SetCardJPGPathNameV(string path);//设置竖版身份证正反两面图片全路径
        /// <summary>
        /// 获取竖版身份证正反两面图片全路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="cbPath"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int GetCardJPGPathNameV(StringBuilder path, int cbPath);//获取竖版身份证正反两面图片全路径
        /// <summary>
        /// 设置横版身份证正反两面图片全路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int SetCardJPGPathNameH(string path);//设置横版身份证正反两面图片全路径
        /// <summary>
        /// 获取横版身份证正反两面图片全路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="cbPath"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int GetCardJPGPathNameH(StringBuilder path, int cbPath);//获取横版身份证正反两面图片全路径
        /// <summary>
        /// 获取姓名
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getName(StringBuilder data, int cbData);//获取姓名
        /// <summary>
        /// 获取性别
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getSex(StringBuilder data, int cbData);//获取性别
        /// <summary>
        /// 获取民族
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getNation(StringBuilder data, int cbData);//获取民族
        /// <summary>
        /// 获取生日(YYYYMMDD)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getBirthdate(StringBuilder data, int cbData);//获取生日(YYYYMMDD)
        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getAddress(StringBuilder data, int cbData);//获取地址
        /// <summary>
        /// 获取身份证号
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getIDNum(StringBuilder data, int cbData);//获取身份证号
        /// <summary>
        /// 获取签发机关
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getIssue(StringBuilder data, int cbData);//获取签发机关
        /// <summary>
        /// 获取有效期起始日期(YYYYMMDD)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getEffectedDate(StringBuilder data, int cbData);//获取有效期起始日期(YYYYMMDD)
        /// <summary>
        /// 获取有效期截止日期(YYYYMMDD) 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getExpiredDate(StringBuilder data, int cbData);//获取有效期截止日期(YYYYMMDD) 
        /// <summary>
        /// 获取BMP头像Base64编码 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getBMPPhotoBase64(StringBuilder data, int cbData);//获取BMP头像Base64编码 
        /// <summary>
        /// 获取JPG头像Base64编码
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getJPGPhotoBase64(StringBuilder data, int cbData);//获取JPG头像Base64编码
        /// <summary>
        /// 获取竖版身份证正反两面JPG图像base64编码字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getJPGCardBase64V(StringBuilder data, int cbData);//获取竖版身份证正反两面JPG图像base64编码字符串
        /// <summary>
        /// 获取横版身份证正反两面JPG图像base64编码字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbData"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getJPGCardBase64H(StringBuilder data, int cbData);//获取横版身份证正反两面JPG图像base64编码字符串
        /// <summary>
        /// 语音提示。。仅适用于与带HID语音设备的身份证阅读器（如ID200）
        /// </summary>
        /// <param name="nVoice"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int HIDVoice(int nVoice);//语音提示。。仅适用于与带HID语音设备的身份证阅读器（如ID200）
        /// <summary>
        /// 设置发卡器序列号
        /// </summary>
        /// <param name="iPort"></param>
        /// <param name="data"></param>
        /// <param name="cbdata"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int IC_SetDevNum(int iPort, StringBuilder data, int cbdata);//设置发卡器序列号
        /// <summary>
        /// 获取发卡器序列号
        /// </summary>
        /// <param name="iPort"></param>
        /// <param name="data"></param>
        /// <param name="cbdata"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int IC_GetDevNum(int iPort, StringBuilder data, int cbdata);//获取发卡器序列号
        /// <summary>
        /// 设置发卡器序列号
        /// </summary>
        /// <param name="iPort"></param>
        /// <param name="data"></param>
        /// <param name="cbdata"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int IC_GetDevVersion(int iPort, StringBuilder data, int cbdata);//设置发卡器序列号 
        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="iPort"></param>
        /// <param name="keyMode"></param>
        /// <param name="sector"></param>
        /// <param name="idx"></param>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cbdata"></param>
        /// <param name="snr"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int IC_WriteData(int iPort, int keyMode, int sector, int idx, StringBuilder key, StringBuilder data, int cbdata, ref uint snr);//写数据
        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="iPort"></param>
        /// <param name="keyMode"></param>
        /// <param name="sector"></param>
        /// <param name="idx"></param>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cbdata"></param>
        /// <param name="snr"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int IC_ReadData(int iPort, int keyMode, int sector, int idx, StringBuilder key, StringBuilder data, int cbdata, ref uint snr);//du数据
        /// <summary>
        /// 读IC卡物理卡号
        /// </summary>
        /// <param name="iPort"></param>
        /// <param name="snr"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int IC_GetICSnr(int iPort, ref uint snr);//读IC卡物理卡号 
        /// <summary>
        /// 读身份证物理卡号
        /// </summary>
        /// <param name="iPort"></param>
        /// <param name="data"></param>
        /// <param name="cbdata"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int IC_GetIDSnr(int iPort, StringBuilder data, int cbdata);//读身份证物理卡号 
        /// <summary>
        /// 获取英文名
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbdata"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getEnName(StringBuilder data, int cbdata);//获取英文名
        /// <summary>
        /// 获取中文名
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbdata"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getCnName(StringBuilder data, int cbdata);//获取中文名 
        /// <summary>
        /// 获取港澳台居通行证号码
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cbdata"></param>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getPassNum(StringBuilder data, int cbdata);//获取港澳台居通行证号码
        /// <summary>
        /// 获取签发次数
        /// </summary>
        /// <returns></returns>
        [DllImport(Dll_PATH)]
        public static extern int getVisaTimes();//获取签发次数

        [DllImport(Dll_PATH)]
        public static extern int IC_ChangeSectorKey(int iPort, int keyMode, int nSector, StringBuilder oldKey, StringBuilder newKey);
    }
}
