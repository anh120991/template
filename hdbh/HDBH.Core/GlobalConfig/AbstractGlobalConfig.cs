using System.Globalization;


namespace HDBH.Core.GlobalConfig
{
    public abstract class AbstractGlobalConfig
    {
        public abstract CultureInfo FormatInfo { get; }
        public abstract string DateSQLPattern { get; }

        protected static AbstractGlobalConfig _instance;

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static AbstractGlobalConfig Instance
        {
            get { return _instance; }
        }
    }
}
