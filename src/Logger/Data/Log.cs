using System;

namespace Logger.Data
{
    public class Log
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public LogType LogType { get; set; }
        public string Message { get; set; }
    }
}