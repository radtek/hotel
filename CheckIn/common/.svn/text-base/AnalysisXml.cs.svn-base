using System.Xml;

namespace CheckIn.common
{
    class AnalysisXml
    {
        public static void getResponse(ref bool result, ref string err_code, ref string msg, string xmlStr)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            XmlNode root_node = doc.SelectSingleNode("root");
            XmlNode success_node = root_node.SelectSingleNode("success");
            if ("1".Equals(success_node.InnerText))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            XmlNode errorcode_node = root_node.SelectSingleNode("errorcode");
            err_code = errorcode_node.InnerText;
            XmlNode msg_node = root_node.SelectSingleNode("msg");
            msg = msg_node.InnerText;
        }
    }
}
