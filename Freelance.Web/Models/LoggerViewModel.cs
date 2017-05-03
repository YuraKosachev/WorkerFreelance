using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.Web.Models
{
    public class LoggerViewModel
    {
        public LoggerViewModel() { }
        public LoggerViewModel(string exceptionType, string exceptionMessage, string exceptionStackTrace)
        {
            DateTimeOfCreate = DateTime.Now;
            ExceptionId = Guid.NewGuid();
            ExceptionMessage = exceptionMessage;
            ExceptionStackTrace = exceptionStackTrace;
            ExceptionType = exceptionType;

        }
        public static LoggerViewModel Instance(string exceptionType, string exceptionMessage, string exceptionStackTrace)
        {
            return new LoggerViewModel(exceptionType, exceptionMessage, exceptionStackTrace);
        }
        public Guid ExceptionId { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public DateTime DateTimeOfCreate { get; set; }
    }
}