using System;

namespace CactusGuru.Infrastructure.Logging
{
    public interface ILogger
    {
        void Fatal(string message);
        void Fatal(string message, Exception ex);
    }
}
