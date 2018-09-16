using System;
using CactusGuru.Infrastructure.Logging;

namespace CactusGuru.Entry.Infrastructure
{
    public class LogForNet : ILogger
    {
        public LogForNet()
        {
            _log = log4net.LogManager.GetLogger("default");
        }

        private readonly log4net.ILog _log;

        public void Fatal(string message)
        {
            _log.Fatal(message);
        }

        public void Fatal(string message, Exception ex)
        {
            _log.Fatal(message, ex);
        }
    }
}
