using System;
namespace HDBH.Log.Logger
{
    public interface ILogger
    {
        void Fatal(string message, Exception ex = null);
        void Error(string message, Exception ex = null);
        void Warn(string message, Exception ex = null);
        void Info(string message, Exception ex = null);
        void Debug(string message, Exception ex = null);
    }
}
