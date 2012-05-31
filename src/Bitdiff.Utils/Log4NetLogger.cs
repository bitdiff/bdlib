using System;
using log4net;

namespace Bitdiff.Utils
{
    public class Log4NetLogger : ILogger
    {
        public void Error(string tag, Exception exception)
        {
            LogManager.GetLogger(tag).Error(exception.Message, exception);
        }

        public void Error(string tag, Exception exception, string message, params object[] args)
        {
            LogManager.GetLogger(tag).Error(String.Format(message, args), exception);
        }

        public void Info(string tag, string message, params object[] args)
        {
            LogManager.GetLogger(tag).Info(String.Format(message, args));
        }
    }
}