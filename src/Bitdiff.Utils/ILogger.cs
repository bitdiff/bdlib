using System;

namespace Bitdiff.Utils
{
    public interface ILogger
    {
        void Error(string tag, Exception exception, string message, params object[] args);
        void Error(string tag, Exception exception);
        void Info(string tag, string message, params object[] args);
        void Debug(string tag, string message, params object[] args);
        void Warn(string tag, string message, params object[] args);
    }
}