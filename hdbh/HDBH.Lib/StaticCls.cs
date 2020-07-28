using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace HDBH.Lib
{
    public static class StaticCls
    {
        public static string Xtrim(this string me)
        {
            if (me != null)
            {
                return me.Trim();
            }
            return null;
        }
        public static string FilterSpecial(this string txt)
        {
            string ret = string.Empty;
            txt = txt.Xtrim();
            if (!string.IsNullOrWhiteSpace(txt))
            {
                //Regex regex = new Regex(@"(?:union|drop|table|select|delete|update|create|from|where|truncate|xp_|dual|<|>)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                //ret = regex.Replace(txt.Trim(), "");
                Regex regex = new Regex(@"(?:')", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                ret = regex.Replace(txt.Trim(), "''");

                regex = new Regex(@"(?:--|<|>|"")", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                ret = regex.Replace(ret.Trim(), "");
            }
            return ret;
        }
    }
}
