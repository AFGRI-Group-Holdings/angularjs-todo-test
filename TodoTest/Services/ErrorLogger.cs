using System;
using System.IO;

namespace TodoTest.Services
{
    public class ErrorLogger
    {
        private readonly string _logFilePath = "error.log";

        public void Log(Exception ex)
        {
            File.AppendAllText(_logFilePath, $"[{DateTime.Now}] {ex.Message}{Environment.NewLine}");
        }
    }
}
