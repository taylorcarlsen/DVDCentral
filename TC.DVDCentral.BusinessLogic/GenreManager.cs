using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.DVDCentral.Data;

namespace TC.DVDCentral.BusinessLogic
{
    public class GenreManager : IDisposable
    {
        private readonly DVDCentralContext db;

        public GenreManager()
        {
            db = new DVDCentralContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }
        public List<Models.Genre> GetAll()
        {
            var allGenres = db.Genres.OrderBy(x => x.Description);

            List<Models.Genre> genres = new List<Models.Genre>();
            foreach(var i in allGenres)
            {
                genres.Add(new Models.Genre { Id = i.Id, Description = i.Description });
            }
            return genres;
        }
        public Models.Genre GetById(int id)
        {
            var existing = db.Genres.SingleOrDefault(x => x.Id == id);

            if(existing == null)
            {
                return null;
            }
            Models.Genre g = new Models.Genre { Id = existing.Id, Description = existing.Description };

            return g;
        }
        public int Create(Models.Genre genre)
        {
            Data.Genre newGenre = new Data.Genre { Description = genre.Description };
            db.Genres.Add(newGenre);

            db.SaveChanges();
            return newGenre.Id;
        }
        public Models.Genre Update(Models.Genre genre)
        {
            var existing = db.Genres.SingleOrDefault(x => x.Id == genre.Id);

            if(existing != null)
            {
                existing.Description = genre.Description;
                db.SaveChanges();
            }
            return null;
        }
        public bool Delete(int id)
        {
            var existing = db.Genres.SingleOrDefault(x => x.Id == id);
            if(existing != null)
            {
                db.Genres.Remove(existing);
                db.SaveChanges();
                return true;
            }
            else { return false; }
        }
    }
}
