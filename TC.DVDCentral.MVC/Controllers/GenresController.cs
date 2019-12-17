using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC.DVDCentral.BusinessLogic;
using TC.DVDCentral.Models;
using TC.DVDCentral.MVC.Security;

namespace TC.DVDCentral.MVC.Controllers
{
    public class GenresController : Controller
    {
        GenreManager manager;

        // GET: Genres
        [RequireAuthentication]
        public ActionResult Index()
        {
            List<Genre> genres;
            using (manager = new GenreManager())
            {
                genres = manager.GetAll();
            }
                return View(genres);
        }

        [ChildActionOnly]
        public ActionResult SideBar()
        {
            List<Genre> genres;
            using (manager = new GenreManager())
            {
                genres = manager.GetAll();
            }
            return PartialView(genres);
        }

        [RequireAuthentication]
        public ActionResult Details(int Id)
        {
            Genre genre;
            using (manager = new GenreManager())
            {
                genre = manager.GetById(Id);
            }
            if (genre == null)
                return HttpNotFound();
            else
                return View(genre);
        }

        [RequireAuthentication]
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Genre genre;
            using (manager = new GenreManager())
            {
                genre = manager.GetById(Id);
                if (genre == null)
                    return HttpNotFound();
                else
                    return View(genre);
            }
        }
        [HttpPost]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                using (manager = new GenreManager())
                {
                    manager.Update(genre);
                }
                if (genre == null)
                    return HttpNotFound();
                else
                    return RedirectToAction("Index");
            }
            else
                return View(genre.Id);
        }


        [RequireAuthentication]
        [HttpGet]
        public ActionResult Create()
        {
            Genre genre = new Genre();
            return View(genre);
        }

        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                using (manager = new GenreManager())
                {
                    manager.Create(genre);
                }
                if (genre == null)
                    return HttpNotFound();
                else
                    return RedirectToAction("Index");
            }
            else
                return View(genre.Id);
        }

        [RequireAuthentication]
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            Genre genre;
            using (manager = new GenreManager())
            {
                genre = manager.GetById(Id);
                if (genre == null)
                    return HttpNotFound();
                else
                    return View(genre);
            }
        }

        [HttpPost]
        public ActionResult Delete(Genre genre)
        {
            using (manager = new GenreManager())
            {
                manager.Delete(genre.Id);
            }
            if (genre == null)
                return HttpNotFound();
            else
                return RedirectToAction("index");
        }
    }
}