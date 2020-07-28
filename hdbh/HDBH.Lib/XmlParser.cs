using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace HDBH.Lib
{
    /// <summary>
    /// parses a specific Xml.
    /// </summary>
    public class XmlParser
    {
        private XPathNavigator xPathNav = null;
        /// <summary>
        /// Initilize an instance with a specific Xml data.
        /// </summary>
        /// <param name="xmlString">the xml data</param>
        public XmlParser(string xmlString)
        {
            if (string.IsNullOrWhiteSpace(xmlString))
                throw new ArgumentNullException("Could not initialize XmlParser instance with invalid parameter");
            XPathDocument xPathDoc = null;
            try
            {
                xPathDoc = new XPathDocument(new StringReader(xmlString));
            }
            catch (Exception)
            {
                xPathDoc = new XPathDocument(new StringReader("<root>" + xmlString + "</root>"));
            }
            xPathNav = xPathDoc.CreateNavigator();

        }

        public static string GetValue(string xpath, string xml, string _default)
        {
            XmlParser parser = new XmlParser(xml);
            return parser.GetValue(xpath, _default);
        }

        /// <summary>
        /// Wall throught nodes in XML Document, by selecting in a specified XPath Expression.
        /// </summary>
        /// <param name="xpath">representing an XPath expression to select</param>
        /// <param name="navigator">Interface class </param>
        /// <exception cref="ArgumentException">The XPath expression contains an error or its return type is not a node set</exception>
        /// <exception cref="XPathException">The XPath expression is not valid</exception>
        /// <exception cref="Exception">Catch errors occur during application execution</exception>
        public void WalkThrough(string xpath, IXpathNavigator navigator)
        {
            try
            {
                XPathNodeIterator nodes = xPathNav.Select(xpath);
                while (nodes.MoveNext())
                {
                    navigator.Navigate(nodes.Current);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Get a string represent child nodes of first matching node by selecting in a specified XPath Expression in XML Document. 
        /// </summary>
        /// <param name="xpath">representing an XPath expression to select</param>
        /// <param name="defaultValue">string represent nodes</param>
        /// <returns>a string represent child nodes of first matching node by selecting in a specified XPath Expression</returns>
        public string GetValue(string xpath, string defaultValue)
        {
            try
            {
                var node = xPathNav.SelectSingleNode(xpath);
                defaultValue = node.Value;
            }
            catch (Exception) { }
            return defaultValue;
        }
        public string GetNodeInnerXML(string xpath)
        {
            var node = xPathNav.SelectSingleNode(xpath);
            return node.InnerXml;
        }
        /// <summary>
        /// Try to get a string represent child nodes of first matching node by selecting in a specified XPath Expression in XML Document. 
        /// </summary>
        /// <param name="xpath">representing an XPath expression to select</param>
        /// <param name="value">string represent nodes</param>
        /// <returns><c>true</c> if there is a matching node by selecting in a specified Xpath Expression else return <c>false</c></returns>
        public bool TryGetValue(string xpath, out string value)
        {
            bool ret = false;
            value = null;
            try
            {
                var node = xPathNav.SelectSingleNode(xpath);
                if (node != null)
                {
                    value = node.Value;
                    ret = true;
                }
            }
            catch (Exception) { }
            return ret;
        }

        public List<T> GetList<T>(string xpath)
        {
            try
            {
                return XmlUtility.GetList<T>(xPathNav.Select(xpath));
            }
            catch (Exception) { }
            return new List<T>();
        }

    }
    /// <summary>
    /// <para>Interface IXpathNavigator is used in method WalkThrough of an XmlParser instance.</para>
    /// <para>The WalkThrough method walks through each node in an XPathNodeIterator and call the method Navigate of the interface</para>
    /// </summary>
    public interface IXpathNavigator
    {
        void Navigate(XPathNavigator nav);
    }
}
