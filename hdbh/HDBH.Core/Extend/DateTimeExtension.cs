using HDBH.Core.GlobalConfig;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class DateTimeExtension
{

    public static string ToPattern(this DateTime self)
    {
        return self.ToString("dd/MM/yyyy HH:mm:ss");
    }

    public static string ToShortDatePattern(this DateTime self)
    {
        return self.ToString("dd/MM/yyyy");
    }


    public static string ToLongDatePattern(this DateTime self)
    {
        return self.ToString("dd/MM/yyyy");
    }


    public static string ToFullDateTimePattern(this DateTime self)
    {
        return self.ToString("dd/MM/yyyy HH:mm:ss");
    }


    public static string ToShortTimePattern(this DateTime self)
    {
        return self.ToString("HH:mm");
    }


    public static string ToLongTimePattern(this DateTime self)
    {
        return self.ToString("HH:mm:ss");
    }
    public static string ToYYYYMMDD(this string dateString)
    {
        DateTime dt = DateTime.Parse(dateString);
        return dt.ToString("yyyyMMdd");
    }
}