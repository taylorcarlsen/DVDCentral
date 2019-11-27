using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.DVDCentral.Data;

namespace TC.DVDCentral.BusinessLogic
{
    public class DirectorManager : IDisposable
    {
        private readonly DVDCentralContext db;

        public DirectorManager()
        {
            db = new DVDCentralContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public List<Models.Director> GetAll()
        {
            var allDirectors = db.Directors.OrderBy(x => x.LastName);

            List<Models.Director> directors = new List<Models.Director>();
            foreach (var i in allDirectors)
            {
                directors.Add(new Models.Director { Id = i.Id, FirstName = i.FirstName, LastName = i.LastName });
            }

            return directors;
        }

        public Models.Director GetById(int id)
        {
            var existing = db.Directors.SingleOrDefault(x => x.Id == id);
            if(existing == null)
            {
                return null;
            }
            Models.Director d = new Models.Director { Id = existing.Id, FirstName = existing.FirstName, LastName = existing.LastName };

            return d;
        }
        public int Create(Models.Director director)
        {
            Data.Director newDirector = new Data.Director { FirstName = director.FirstName, LastName = director.LastName };
            db.Directors.Add(newDirector);

            db.SaveChanges();
            return newDirector.Id;
        }
        public Models.Director Update(Models.Director director)
        {
            var existing = db.Directors.SingleOrDefault(x => x.Id == director.Id);

            if (existing != null)
            {
                existing.FirstName = director.FirstName;
                existing.LastName = director.LastName;
                db.SaveChanges();
            }
            return null;
        }

        /// <summary>
        /// Delete a degree type giving the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if successful - false if the id is not found.</returns>
        public bool Delete(int id)
        {
            var exisitng = db.Directors.SingleOrDefault(x => x.Id == id);

            if (exisitng != null)
            {
                db.Directors.Remove(exisitng);
                db.SaveChanges();
                return true;
            }
            else { return false; }
        }
    }
}
