using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.DVDCentral.Data;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.BusinessLogic
{
    public class ErrorLogger : IDisposable
    {
        protected DVDCentralContext db;

        public ErrorLogger()
        {
            db = new DVDCentralContext();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        private Data.ErrorRecord ConvertToDataRecord(Models.ErrorRecord record)
        {
            if(record == null)
                return null;

            Data.ErrorRecord dataRow = new Data.ErrorRecord
            {
                ExceptionType = record.ExceptionType ?? String.Empty,
                Message = record.Message ?? String.Empty,
                StackTrace = record.StackTrace ?? String.Empty,
                Target = record.Target ?? String.Empty,
                TimeStamp = DateTime.UtcNow,
                Url = record.Url ?? String.Empty,
                UserIp = record.UserIp ?? String.Empty,
                InnerExceptionError = ConvertToDataRecord(record.InnerExceptionError)
            };

            return dataRow;
        }

        private Models.ErrorRecord ConvertFromDataRecord(Data.ErrorRecord record)
        {
            if (record == null)
                return null;
            Models.ErrorRecord model = new Models.ErrorRecord
            {
                ExceptionType = record.ExceptionType ?? string.Empty,
                Message = record.Message ?? string.Empty,
                StackTrace = record.StackTrace ?? string.Empty,
                Target = record.Target ?? string.Empty,
                TimeStamp = DateTime.UtcNow,
                Url = record.Url ?? string.Empty,
                UserIp = record.UserIp ?? string.Empty,
                InnerExceptionError = ConvertFromDataRecord(record.InnerExceptionError)
            };
            return model;
        }

        public List<Models.ErrorRecord> GetAll()
        {
            List<Models.ErrorRecord> errors = new List<Models.ErrorRecord>();
            foreach(var er in db.Errors.OrderByDescending(x => x.TimeStamp))
            {
                errors.Add(ConvertFromDataRecord(er));
            }
            return errors;
        }

        public Models.ErrorRecord GetError(int id)
        {
            var datarow = db.Errors.SingleOrDefault(x => x.Id == id);
            if (datarow == null)
                return null;
            else
                return ConvertFromDataRecord(datarow);
        }

        public void LogError(Models.ErrorRecord error)
        {
            if (error == null)
                return;

            try
            {
                var newError = ConvertToDataRecord(error);
                db.Errors.Add(newError);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
