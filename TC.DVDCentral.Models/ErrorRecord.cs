using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.DVDCentral.Models
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
        public ErrorRecord InnerExceptionError { get;set; }

        public ErrorRecord()
        {

        }

        public ErrorRecord(Exception ex)
        {
            this.ExceptionType = ex.GetType().ToString();
            this.Message = ex.Message;
            this.StackTrace = ex.StackTrace;
            if(ex.StackTrace != null)
                this.Target = ex.TargetSite.ToString();
            if (ex.InnerException != null)
                this.InnerExceptionError = BuildErrorFromException(ex.InnerException);
        }

        private ErrorRecord BuildErrorFromException(Exception ex)
        {
            ErrorRecord error = new ErrorRecord();
            error.ExceptionType = ex.GetType().ToString();
            error.Message = ex.Message;
            error.StackTrace = ex.StackTrace;
            if (ex.TargetSite != null)
                error.Target = ex.TargetSite.ToString();
            if (ex.InnerException != null)
                error.InnerExceptionError = BuildErrorFromException(ex.InnerException);

            return error;
        }
    }
}
