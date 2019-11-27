using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.DVDCentral.Data;

namespace TC.DVDCentral.BusinessLogic
{
    public class RatingManager : IDisposable
    {
        private readonly DVDCentralContext db;

        public RatingManager()
        {
            db = new DVDCentralContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }
        public List<Models.Rating>GetAll()
        {
            var allRatings = db.Ratings.OrderBy(x => x.Description);

            List<Models.Rating> ratings = new List<Models.Rating>();
            foreach(var i in allRatings)
            {
                ratings.Add(new Models.Rating { Description = i.Description, Id = i.Id });
            }
            return ratings;
        }
        public Models.Rating GetById(int id)
        {
            var existing = db.Ratings.SingleOrDefault(x => x.Id == id);
            if(existing == null)
            {
                return null;
            }
            Models.Rating r = new Models.Rating { Id = existing.Id, Description = existing.Description };
            return r;
        }
        public int Create(Models.Rating rating)
        {
            Data.Rating newRating = new Data.Rating { Description = rating.Description };
            db.Ratings.Add(newRating);

            db.SaveChanges();
            return newRating.Id;
        }
        public Models.Rating Update(Models.Rating rating)
        {
            var exsiting = db.Ratings.SingleOrDefault(x => x.Id == rating.Id);
            if(exsiting != null)
            {
                exsiting.Description = rating.Description;
                db.SaveChanges();
            }
            return null;
        }
        public bool Delete(int id)
        {
            var existing = db.Ratings.SingleOrDefault(x => x.Id == id);
            if (existing != null)
            {
                db.Ratings.Remove(existing);
                db.SaveChanges();
                return true;
            }
            else { return false; }
        }
    }
}
