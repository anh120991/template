using HDBH.Models.AbstractHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        ///     To the short pattern.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ToShortPattern(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return String.Empty;

            }
            return dateTime.Value.ToString(AbstractGlobalConfig.Instance.FormatInfo.DateTimeFormat.ShortDatePattern);
        }

        public static string ToShortPattern(this DateTime? dateTime, string format)
        {
            if (dateTime == null)
            {
                return String.Empty;

            }
            return dateTime.Value.ToString(format);
        }

        public static string ToHHMMTime(this TimeSpan? time)
        {
            if (time == null)
            {
                return String.Empty;

            }
            return time.Value.Hours + ":" + time.Value.Minutes;
        }

        /// <summary>
        ///  To the short pattern.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ToShortPattern(this DateTime dateTime)
        {
            if (dateTime.Year == 1)
            {
                return string.Empty;
            }
            return dateTime.ToString(AbstractGlobalConfig.Instance.FormatInfo.DateTimeFormat.ShortDatePattern);
        }

        /// <summary>
        ///  To FullDateTimePattern.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ToFullDateTimePattern(this DateTime dateTime)
        {
            if (dateTime.Year == 1)
            {
                return string.Empty;
            }
            return dateTime.ToString(AbstractGlobalConfig.Instance.FormatInfo.DateTimeFormat.FullDateTimePattern);
        }

        /// <summary>
        ///  To FullDateTimePattern.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ToFullDateTimePattern(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return string.Empty;
            }
            return dateTime.Value.ToString(AbstractGlobalConfig.Instance.FormatInfo.DateTimeFormat.FullDateTimePattern);
        }

        /// <summary>
        ///  To the long pattern.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ToLongPattern(this DateTime dateTime)
        {
            if (dateTime.Year == 1)
            {
                return string.Empty;
            }
            return dateTime.ToString(AbstractGlobalConfig.Instance.FormatInfo.DateTimeFormat.FullDateTimePattern);
        }

        /// <summary>
        ///     To the SQL pattern.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime ToSQLPattern(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return new DateTime();
            }
            return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day).Date;
        }

        /// <summary>
        ///     To the SQL pattern.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime ToSQLPattern(this DateTime dateTime)
        {
            if (dateTime.Year == 1)
            {
                return new DateTime();
            }
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).Date;
        }

        public static string ToOtherFormat(this string input, string fromFormat, string toFormat)
        {
            DateTime parsedDateTime;
            string formattedDate;
            if (DateTime.TryParseExact(input, fromFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out parsedDateTime))
            {
                formattedDate = parsedDateTime.ToString(toFormat);
            }
            else
            {
                formattedDate = "Parsing failed";
            }

            return formattedDate;
        }
        public static DateTime ToDMYPattern(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return new DateTime();
            }
            return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day).Date;
        }
    }
}
