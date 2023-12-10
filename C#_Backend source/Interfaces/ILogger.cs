using System;

namespace Backend.Interfaces
{
    public interface ILogger
    {
        void Error(string message, Exception ex);
    }
}
