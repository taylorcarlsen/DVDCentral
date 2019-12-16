using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.DVDCentral.Data
{
    public class ErrorRecord
    {
        public int Id { get; set; }
        public string Url { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public string UserIp { get; set; }
        public string Target { get; set; }
        public string StackTrace { get; set; }
        public ErrorRecord InnerExceptionError { get; set; }
    }
}
