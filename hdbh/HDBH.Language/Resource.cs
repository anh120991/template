// @yasinkuyu
// 05/08/2014

using OCB.Treasury.Language.Model;
using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace HDBH.Language
{
    public class Resource
    {
        /// <summary>
        /// GetXmlResource all items (ex: tr_TR.xml)
        /// 
        /// Very simple xml structure
        ///     <lang>
	    ///         <item id="homepage">Homepage</item>
        ///     </lang>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="applicationName"></param>
        public static void GetXmlResource(string path, string applicationName)
        {
            
            List<LanguageModel> ls = new List<LanguageModel>();
            using (XmlTextReader reader = new XmlTextReader(path)) {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(reader);

                var nodes = xmlDoc.DocumentElement.SelectSingleNode("//lang");

                for (int i = 0; i <= nodes.ChildNodes.Count - 1; i++) {
                    try
                    {
                        string itemId = nodes.ChildNodes.Item(i).Attributes.Item(0).InnerText;
                        string itemValue = nodes.ChildNodes.Item(i).InnerText;
                        ls.Add(new LanguageModel()
                        {
                            Id = itemId,
                            Value = itemValue
                        });

                        HttpContext.Current.Application[applicationName + itemId] = itemValue;
                    }
                   catch { }
                }
                HttpContext.Current.Session["sessionLang"] = ls;
            }
        }
    }
}
