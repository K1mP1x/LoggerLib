using System;
using Logger.Data.Enum;

namespace Logger.Data.Model
{
    public class Log
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public LogType LogType { get; set; }
        public string Message { get; set; }
    }
}