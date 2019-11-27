using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.DVDCentral.Data;

namespace TC.DVDCentral.BusinessLogic
{
    public class FormatManager : IDisposable
    {
        private readonly DVDCentralContext db;

        public FormatManager()
        {
            db = new DVDCentralContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public List<Models.Format>GetAll()
        {
            var allFormats = db.Formats.OrderBy(x => x.Description);

            List<Models.Format> formats = new List<Models.Format>();
            foreach(var i in allFormats)
            {
                formats.Add(new Models.Format { Id = i.Id, Description = i.Description });
            }
            return formats;
        }
        public Models.Format GetById(int id)
        {
            var existing = db.Formats.SingleOrDefault(x => x.Id == id);
            if(existing == null)
            {
                return null;
            }
            Models.Format f = new Models.Format { Id = existing.Id, Description = existing.Description };

            return f;
        }
        public int Create(Models.Format format)
        {
            Data.Format newFormat = new Data.Format { Description = format.Description };
            db.Formats.Add(newFormat);
            db.SaveChanges();
            return newFormat.Id;
        }
        public Models.Format Update(Models.Format format)
        {
            var existing = db.Formats.SingleOrDefault(x => x.Id == format.Id);
            if(existing != null)
            {
                existing.Description = format.Description;
                db.SaveChanges();
            }
            return null;
        }
        public bool Delete(int id)
        {
            var existing = db.Formats.SingleOrDefault(x => x.Id == id);
            if(existing != null)
            {
                db.Formats.Remove(existing);
                db.SaveChanges();
                return true;
            }
            else { return false; }
        }
    }
}
