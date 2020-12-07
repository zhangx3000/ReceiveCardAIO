using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CRCXMODEM
{
    public class FunLib
    {
        public static string 初始化不移动卡 { get { return StrCommon.GetSerialCommand("C03"); } }
        public static string 查卡机状态 { get { return StrCommon.GetSerialCommand("C10"); } }
        public static string 将卡移动到RF卡位 { get { return StrCommon.GetSerialCommand("C22"); } }
        public static string 将卡移到出卡口持卡位 { get { return StrCommon.GetSerialCommand("C20"); } }
        public static string 使能前端进卡 { get { return StrCommon.GetSerialCommand("C30"); } }
        public static string 将卡移动到发卡栈 { get { return StrCommon.GetSerialCommand("C24"); } }
    }

    /// <summary>
    /// 命令列表，eg.C03->43 30 33
    /// 获取枚举描述文本
    /// </summary>
    public enum FunList
    {
        [DisplayText("C03")]
        初始化不移动卡,

        [DisplayText("C10")]
        查卡机状态,

        [DisplayText("C22")]
        将卡移动到RF卡位,

        [DisplayText("C20")]
        将卡移到出卡口持卡位,

        [DisplayText("C30")]
        使能前端进卡,

        [DisplayText("C24")]
        将卡移动到发卡栈,
    }
    /// <summary>
    /// 枚举描述文本特性
    /// </summary>
    public class DisplayText : Attribute
    {
        public DisplayText(string Text)
        {
            this.text = Text;
        }
        private string text;
        public string Text
        {
            get { return text; }
            //get { return StrCommon.GetSerialCommand(this.text); }
            set { text = value; }
        }
    }
}
