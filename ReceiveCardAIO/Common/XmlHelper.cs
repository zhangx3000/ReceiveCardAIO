using System.Xml;

namespace ReceiveCardAIO
{
    public class XmlHelper
    {
        /// <summary>
        /// 读取XML资源中的指定节点内容
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>节点内容</returns>
        public static string GetNodeValue(string source, string nodeName)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(source);
            var documentElement = xmlDocument.DocumentElement;
            var selectSingleNode = documentElement?.SelectSingleNode("//" + nodeName);
            return selectSingleNode?.InnerText;
        }
    }
}