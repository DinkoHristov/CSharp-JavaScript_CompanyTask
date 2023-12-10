using Backend.Interfaces;
using log4net;
using System;

namespace Backend.Model
{
    // Logger class provides error logging
    public class Logger : ILogger
    {
        private readonly ILog _log;

        public Logger()
        {
            _log = LogManager.GetLogger(typeof(Logger));
        }

        public void Error(string message, Exception ex)
        {
            _log.Error(message, ex);
        }
    }
}
