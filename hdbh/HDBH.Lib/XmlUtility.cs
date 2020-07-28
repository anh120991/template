using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace HDBH.Lib
{
    public class XmlUtility
    {
        /// <summary>
        /// Converts a none Xml string to an Xml string by encoding Xml special characters
        /// </summary>
        /// <param name="noneXMLString">the string to be converted</param>
        /// <returns></returns>
        public static string XmlSpecialchars(string noneXmlString)
        {
            if (string.IsNullOrEmpty(noneXmlString)) return string.Empty;
            string[][] exchanges = new string[][] {
                new string[] { "&", "&amp;" },
                new string[] { "\"", "&quot;" },
                new string[] { "'", "&#39;" },
                new string[] { "<", "&lt;" },
                new string[] { ">", "&gt;" }
            };
            foreach (string[] p in exchanges)
            {
                noneXmlString = noneXmlString.Replace(p[0], p[1]);
            }
            return noneXmlString;
        }
        /// <summary>
        /// Check if a name is a valid Xml Element Name
        /// </summary>
        /// <param name="name">the name to be checked</param>
        /// <returns></returns>
        public static bool IsValidNodeName(string name)
        {
            bool valid = false;
            try
            {
                string xml = string.Format("<root><{0}>1</{0}></root>", name);
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xml);
                valid = true;
            }
            catch (Exception)
            {
                valid = false;
            }
            return valid;
        }
        public static List<T> GetList<T>(XPathNodeIterator iterator)
        {
            var lstObj = new List<T>();
            while (iterator.MoveNext())
            {
                string xml = iterator.Current.InnerXml;
                if (!xml.StartsWith("<?xml version=\"1.0\" encoding=\"UTF-8\"?>"))
                {
                    string template = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><{0} xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">{1}</{2}>";
                    string name = typeof(T).Name;
                    xml = string.Format(template, name, xml, name);
                }
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using (TextReader rd = new StringReader(xml))
                {
                    lstObj.Add((T)ser.Deserialize(rd));
                }
            }
            return lstObj;
        }

        public static T Deserialize<T>(string xml)
        {
            if (!xml.StartsWith("<?xml version=\"1.0\" encoding=\"UTF-8\"?>"))
            {
                string template = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><{0} xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">{1}</{2}>";
                xml = string.Format(template, typeof(T).Name, xml, typeof(T).Name);
            }
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (TextReader rd = new StringReader(xml))
            {
                return (T)ser.Deserialize(rd);
            }
        }
        public static string Serialize(object ob)
        {
            string ret = "";
            try
            {
                if (ob != null)
                {
                    XmlSerializer x = new XmlSerializer(ob.GetType());
                    StringWriter sw = new StringWriter();
                    x.Serialize(sw, ob);
                    ret = sw.ToString();
                }
                else
                {
                    ret = "Object is null";
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }
    }
    public interface XmlParserExceptor
    {
        object ParseValue(string elementName, string val);
    }
}
