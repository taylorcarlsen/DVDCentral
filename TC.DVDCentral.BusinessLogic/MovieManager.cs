using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.DVDCentral.Data;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.BusinessLogic
{
    public class MovieManager : IDisposable
    {
        private readonly DVDCentralContext db;

        public MovieManager()
        {
            db = new DVDCentralContext();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public List<Models.Movie> GetAll()
        {
            List<Models.Movie> movieModel = new List<Models.Movie>();
            foreach(var movie in db.Movies.Include("Rating").Include("Format").Include("Director").OrderBy(x=>x.Description))
            {
                movieModel.Add(new Models.Movie
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    ImagePath = movie.ImagePath,
                    Cost = movie.Cost,
                    Format = new Models.Format { Id = movie.Format.Id, Description = movie.Format.Description },
                    Rating = new Models.Rating { Id = movie.Rating.Id, Description = movie.Rating.Description },
                    Director = new Models.Director { Id = movie.Director.Id, FirstName = movie.Director.FirstName, LastName = movie.Director.LastName }

                });
            }
            var movie1 = movieModel;

            return movieModel;
        }

        public List<Models.Movie> GetAllByGenreId(int genreId)
        {
            List<Models.Movie> movies = new List<Models.Movie>();
            List<Models.Genre> genres = new List<Models.Genre>();
            foreach(var existing in db.Movies.Include("Rating").Include("Format").Include("Genres").Include("Director").Where(x => x.Genres.Any(g => g.Id == genreId)))
            {
                movies.Add(new Models.Movie
                {
                    Id = existing.Id,
                    Cost = existing.Cost,
                    Description = existing.Description,
                    Director = new Models.Director { Id = existing.Director.Id, FirstName = existing.Director.FirstName, LastName = existing.Director.LastName },
                    Format = new Models.Format { Id = existing.Format.Id, Description = existing.Format.Description },
                    Rating = new Models.Rating { Id = existing.Rating.Id, Description = existing.Rating.Description },
                    ImagePath = existing.ImagePath,
                    Title = existing.Title
                });
            }
            return movies;
        }

        

        public Models.Movie GetById(int id)
        {
            var existing = db.Movies.Include("Rating").Include("Format").Include("Genres").Include("Director").FirstOrDefault(x => x.Id == id);
            if(existing == null)
            {
                return null;
            }

            List<Models.Genre> existingGenres = new List<Models.Genre>();
            foreach(var g in existing.Genres)
            {
                existingGenres.Add(new Models.Genre { Id = g.Id, Description = g.Description });
            }

            Models.Movie model = new Models.Movie()
            {
                Id = existing.Id,
                Description = existing.Description,
                Cost = existing.Cost,
                ImagePath = existing.ImagePath,
                Title = existing.Title,
                Genres = existingGenres,
                Director = new Models.Director { Id = existing.Director.Id, FirstName = existing.Director.FirstName, LastName = existing.Director.LastName },
                Format = new Models.Format { Id = existing.Format.Id, Description = existing.Format.Description },
                Rating = new Models.Rating { Id = existing.Rating.Id, Description = existing.Rating.Description }
            };

            return model;
        }

        public int Add(Models.Movie movieModel)
        {
            if(movieModel.Format == null)
            {
                throw new ArgumentNullException("Format", "The Format property must not be null.");
            }
            if(movieModel.Director == null)
            {
                throw new ArgumentNullException("Director", "The Director property must not be null.");
            }

            Data.Movie newRow = new Data.Movie();
            newRow.Cost = movieModel.Cost;
            newRow.Description = movieModel.Description;
            newRow.Title = movieModel.Title;
            newRow.ImagePath = movieModel.ImagePath;

            Data.Rating existingRatings = db.Ratings.SingleOrDefault(x => x.Id == movieModel.Rating.Id);
            newRow.Rating = existingRatings ?? throw new ArgumentException("The associated rating cannot be found.");

            Data.Director existingDirectors = db.Directors.SingleOrDefault(x => x.Id == movieModel.Director.Id);
            newRow.Director = existingDirectors ?? throw new ArgumentException("The associated Director cannot be found.");

            Data.Format existingFormats = db.Formats.SingleOrDefault(x => x.Id == movieModel.Format.Id);
            newRow.Format = existingFormats ?? throw new ArgumentException("The associated format cannot be found.");

            db.Movies.Add(newRow);
            db.SaveChanges();
            return newRow.Id;
        }

        public Models.Movie Update(Models.Movie movieModel)
        {
            if (movieModel.Format == null)
            {
                throw new ArgumentNullException("Format", "The Format property must not be null.");
            }
            if (movieModel.Director == null)
            {
                throw new ArgumentNullException("Director", "The Director property must not be null.");
            }

            var existing = db.Movies.SingleOrDefault(x => x.Id == movieModel.Id);
            if(existing == null)
            {
                return null;
            }

            existing.Cost = movieModel.Cost;
            existing.Description = movieModel.Description;
            existing.Title = movieModel.Title;
            existing.ImagePath = movieModel.ImagePath;

            Data.Director existingDirectors = db.Directors.SingleOrDefault(x => x.Id == movieModel.Director.Id);
            existing.Director = existingDirectors ?? throw new ArgumentException("The associated Director cannot be found.");

            Data.Format existingFormats = db.Formats.SingleOrDefault(x => x.Id == movieModel.Format.Id);
            existing.Format = existingFormats ?? throw new ArgumentException("The associated format cannot be found.");

            Data.Rating existingRatings = db.Ratings.SingleOrDefault(x => x.Id == movieModel.Rating.Id);
            existing.Rating = existingRatings ?? throw new ArgumentException("The associated Rating cannot be found.");

            db.SaveChanges();
            return movieModel;
        }

        public bool Delete(int id)
        {
            var existing = db.Movies.SingleOrDefault(x => x.Id == id);
            if(existing != null)
            {
                db.Movies.Remove(existing);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public void AddGenres(int movieId, List<int> genreIds)
        {
            var existing = db.Movies.Include("Genres").FirstOrDefault(x => x.Id == movieId);
            if(existing != null)
            {
                var genresToRemove = existing.Genres.ToList();
                foreach(var g in genresToRemove)
                {
                    existing.Genres.Remove(g);
                }
                db.SaveChanges();
            }

            foreach(var id in genreIds)
            {
                Data.Genre genre = db.Genres.SingleOrDefault(x => x.Id == id);
                existing.Genres.Add(genre ?? throw new ArgumentException("The associated genre does not exist."));
            }
            db.SaveChanges();
        }
    }
}
