using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace HDBH.Core.GlobalConfig
{
    public class GlobalConfigHelper : AbstractGlobalConfig
    {
        private static CultureInfo _info;
        private static string _dateSQLPattern;

        /// <summary>
        ///     Gets the format information.
        /// </summary>
        /// <value>
        ///     The format information.
        /// </value>
        public override CultureInfo FormatInfo
        {
            get { return _info; }
        }

        /// <summary>
        ///     Gets the date SQL pattern.
        /// </summary>
        /// <value>
        ///     The date SQL pattern.
        /// </value>
        public override string DateSQLPattern
        {
            get { return _dateSQLPattern; }
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static GlobalConfigHelper GetInstance()
        {
            if (_instance == null) _instance = new GlobalConfigHelper();
            return (GlobalConfigHelper)_instance;
        }

        public void Init(string lang = "vi-vn")
        {

            _info = new CultureInfo("vi-vn")
            {
                DateTimeFormat =
                        {
                            FullDateTimePattern = "dd/MM/yyyy HH:mm:ss",
                            ShortTimePattern = "HH:mm",
                            ShortDatePattern = "dd/MM/yyyy"
                        },
                NumberFormat =
                {
                    NumberGroupSeparator = "."
                }
            };
            _dateSQLPattern = "yyyymmdd hh24:mi:ss.ff6";

        }
    }
}
