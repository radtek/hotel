using System;
using System.Xml;
using log4net;

namespace CommonLibrary
{
    public class XmlHelper
    {
        private static string xmlUrl = AppDomain.CurrentDomain.BaseDirectory + "setting.xml";
        private readonly static ILog _log = LogManager.GetLogger("XmlHelper");
        readonly static XmlDocument _xmlDoc = new XmlDocument();
        static XmlHelper()
        {
            try
            {
                _xmlDoc.Load(xmlUrl);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }
        public static string ReadNode(string key)
        {
            XmlNodeList nodeList = _xmlDoc.SelectSingleNode("settings").ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType != XmlNodeType.Comment)
                {
                    XmlElement xe = (XmlElement)node;
                    if (xe.GetAttribute("key") == key)
                    {
                        return xe.GetAttribute("value");
                    }
                }
            }
            return "";
        }
        public static void WriteNode(string key, string value)
        {
            XmlNodeList nodeList = _xmlDoc.SelectSingleNode("settings").ChildNodes;
            var Flag = false;
            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType != XmlNodeType.Comment)
                {
                    XmlElement xe = (XmlElement)node;
                    if (xe.GetAttribute("key") == key)//如果没有该节点则添加
                    {
                        xe.SetAttribute("value", value);
                        Flag = true;
                        break;
                    }
                }
            }
            if (!Flag)
            {
                XmlNode root = _xmlDoc.SelectSingleNode("settings");
                XmlNode xn = _xmlDoc.CreateNode(XmlNodeType.Element, "node", null);
                XmlAttribute xa = _xmlDoc.CreateAttribute("key");
                xa.Value = key;

                XmlAttribute xa2 = _xmlDoc.CreateAttribute("value");
                xa2.Value = value;
                xn.Attributes.Append(xa);
                xn.Attributes.Append(xa2);
                root.AppendChild(xn);
            }
            _xmlDoc.Save(xmlUrl);
        }
    }
}
