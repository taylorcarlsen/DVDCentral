using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC.DVDCentral.BusinessLogic;
using TC.DVDCentral.Models;
using TC.DVDCentral.MVC.Models;
using TC.DVDCentral.MVC.Security;

namespace TC.DVDCentral.MVC.Controllers
{
    public class MoviesController : Controller
    {
        MovieManager manager;

        // GET: Movies
        [RequireAuthentication]
        public ActionResult Index()
        {
            List<DVDCentral.Models.Movie> movies;
            using (manager = new MovieManager())
            {
                movies = manager.GetAll();
            }

            return View(movies);
        }

        [RequireAuthentication]
        public ActionResult GetAllByGenreId(int id)
        {
            List<DVDCentral.Models.Movie> movies;
            using (manager = new MovieManager())
            {
                movies = manager.GetAllByGenreId(id);
            }

            if(movies.Any())
            {
                ViewBag.Message = movies[0].Title;
            }

            return View("Index", movies);
        }

        // GET: Movies/Details/5
        [RequireAuthentication]
        public ActionResult Details(int id)
        {
            Movie movie;
            using (manager = new MovieManager())
            {
                movie = manager.GetById(id);
            }
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(movie);
            }

        }

        // GET: Movies/Create
        [RequireAuthentication]
        public ActionResult Create()
        {
            MovieViewModel viewModel = new MovieViewModel();
            viewModel.Movie = new Movie();
            using (GenreManager genreManager = new GenreManager())
            {
                viewModel.PossibleGenres = genreManager.GetAll();
            }
            using (RatingManager ratingManager = new RatingManager())
            {
                viewModel.PossibleRatings = ratingManager.GetAll();
            }
            using (DirectorManager directorManager = new DirectorManager())
            {
                viewModel.PossibleDirectors = directorManager.GetAll();
            }
            using (FormatManager formatManager = new FormatManager())
            {
                viewModel.PossibleFormats = formatManager.GetAll();
            }

            return View(viewModel);
        }

        // POST: Movies/Create
        [HttpPost]
        public ActionResult Create(MovieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (manager = new MovieManager())
                {
                    Movie movie = new Movie();
                    movie.Cost = viewModel.Movie.Cost;
                    movie.Description = viewModel.Movie.Description;
                    movie.Title = viewModel.Movie.Title;
                    movie.Director = new Director { Id = viewModel.SelectedDirectorId };
                    movie.Format = new Format { Id = viewModel.SelectedFormatId };  
                    movie.Rating = new Rating { Id = viewModel.SelectedRatingId };

                    if (viewModel.File != null)
                    {
                        movie.ImagePath = viewModel.File.FileName;
                        string saveTarget = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(viewModel.File.FileName));
                        if (!System.IO.File.Exists(saveTarget))
                        {
                            viewModel.File.SaveAs(saveTarget);
                        }
                    }
                    int id = manager.Add(movie);
                    manager.AddGenres(id, viewModel.CurrentGenres);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(viewModel);
            }
        }

        // GET: Movies/Edit/5
        [RequireAuthentication]
        public ActionResult Edit(int id)
        {
            Movie movie = new Movie();
            using (manager = new MovieManager())
            {
                movie = manager.GetById(id);
            }
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                MovieViewModel vm = new MovieViewModel();
                using (GenreManager genreManager = new GenreManager()) { vm.PossibleGenres = genreManager.GetAll(); }
                using (RatingManager ratingManager = new RatingManager()) { vm.PossibleRatings = ratingManager.GetAll(); }
                using (DirectorManager directorManager = new DirectorManager()) { vm.PossibleDirectors = directorManager.GetAll(); }
                using (FormatManager formatManager = new FormatManager()) { vm.PossibleFormats = formatManager.GetAll(); }
                //vm.MovieId = movie.Id;
                vm.Movie = movie;
                vm.MovieDescription = movie.Description;
                vm.MovieCost = movie.Cost;
                vm.MovieTitle = movie.Title;
                vm.ImagePath = movie.ImagePath;

                vm.CurrentGenres = new List<int>();
                foreach (var a in movie.Genres)
                {
                    vm.CurrentGenres.Add(a.Id);
                }
                return View(vm);
            }
        }

        // POST: Movies/Edit/5
        [HttpPost]
        public ActionResult Edit(MovieViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (manager = new MovieManager())
                {
                    Movie movie = manager.GetById(vm.Movie.Id);
                    movie.Cost = vm.Movie.Cost;
                    movie.Description = vm.Movie.Description;
                    movie.Title = vm.Movie.Title;
                    movie.Format = new Format { Id = vm.SelectedFormatId };
                    movie.Director = new Director { Id = vm.SelectedDirectorId };
                    movie.Rating = new Rating { Id = vm.SelectedRatingId };
                    manager.Update(movie);
                    manager.AddGenres(vm.Movie.Id, vm.CurrentGenres);

                    if (vm.File != null)
                    {
                        movie.ImagePath = vm.File.FileName;
                        string saveTarget = Path.Combine(Server.MapPath("/images"), Path.GetFileName(vm.File.FileName));
                        if (!System.IO.File.Exists(saveTarget))
                        {
                            vm.File.SaveAs(saveTarget);
                        }
                    }

                    return RedirectToAction("Index");
                }
            }
            return View(vm);
        }

        // GET: Movies/Delete/5
        [RequireAuthentication]
        public ActionResult Delete(int id)
        {
            Movie movie;
            using (manager = new MovieManager())
            {
                movie = manager.GetById(id);

            }
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost]
        public ActionResult Delete(Movie movie)
        {
            using (manager = new MovieManager())
            {
                manager.Delete(movie.Id);
            }
            return RedirectToAction("Index");
        }
    }
}
