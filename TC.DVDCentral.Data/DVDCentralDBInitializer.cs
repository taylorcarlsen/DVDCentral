using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TC.DVDCentral.Data
{
    public class DVDCentralDBInitializer : DropCreateDatabaseAlways<DVDCentralContext>
    {
        protected override void Seed(DVDCentralContext context)
        {
            List<Rating> defaultRatings = new List<Rating>();

            defaultRatings.Add(new Rating { Description = "G" });
            defaultRatings.Add(new Rating { Description = "PG" });
            defaultRatings.Add(new Rating { Description = "PG-13" });
            defaultRatings.Add(new Rating { Description = "R" });
            defaultRatings.Add(new Rating { Description = "NC-17" });
            context.Ratings.AddRange(defaultRatings);


            List<Genre> defaultGenres = new List<Genre>();

            defaultGenres.Add(new Genre { Description = "Horror" });
            defaultGenres.Add(new Genre { Description = "Drama" });
            defaultGenres.Add(new Genre { Description = "Thriller" });
            defaultGenres.Add(new Genre { Description = "Action" });
            defaultGenres.Add(new Genre { Description = "Romance" });
            defaultGenres.Add(new Genre { Description = "Adventure" });
            defaultGenres.Add(new Genre { Description = "Science Fiction" });
            context.Genres.AddRange(defaultGenres);

            List<Format> defaultFormats = new List<Format>();
            defaultFormats.Add(new Format { Description = "Blu Ray" });
            defaultFormats.Add(new Format { Description = "DVD" });
            context.Formats.AddRange(defaultFormats);

            List<Director> defaultDirectors = new List<Director>();

            defaultDirectors.Add(new Director
            {
                FirstName = "Steven",
                LastName = "Spielberg",
            });
            defaultDirectors.Add(new Director
            {
                FirstName = "George",
                LastName = "Lucas",
            });
            defaultDirectors.Add(new Director
            {
                FirstName = "James",
                LastName = "Cameron",
            });
            context.Directors.AddRange(defaultDirectors);

            base.Seed(context);
        }
    }
}
