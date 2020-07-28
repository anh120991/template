using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HDBH.Lib
{
    public class TheSession
    {
        public static bool TryGet(string entryName, out object data, bool removeEntry = false)
        {
            data = HttpContext.Current != null && HttpContext.Current.Session != null ? HttpContext.Current.Session[entryName] : null;
            bool ret = data != null;
            if (ret && removeEntry)
            {
                Remove(entryName);
            }
            return ret;
        }
        public static bool TrySet(string entryName, object data)
        {
            bool ret = false;
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[entryName] = data;
                ret = true;
            }
            return ret;
        }
        public static bool RemoveAll()
        {
            bool ret = false;
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.RemoveAll();
                ret = true;
            }
            return ret;
        }

        public static bool Remove(params string[] keys)
        {
            bool ret = false;
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                foreach (string key in keys)
                {
                    HttpContext.Current.Session.Remove(key);
                }
                ret = true;
            }
            return ret;
        }
    }
}
