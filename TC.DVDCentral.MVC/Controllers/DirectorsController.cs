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
    public class DirectorsController : Controller
    {
        DirectorManager manager;

        // GET: Directors
        [RequireAuthentication]
        public ActionResult Index()
        {
            List<Director> directors;
            using (manager = new DirectorManager())
            {
                directors = manager.GetAll();
            }

                return View(directors);
        }

        [RequireAuthentication]
        public ActionResult Details(int Id)
        {
            Director director;
            using (manager = new DirectorManager())
            {
                director = manager.GetById(Id);
            }
            if (director == null)
                return HttpNotFound();
            else
                return View(director);
        }

        [RequireAuthentication]
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Director director;
            using (manager = new DirectorManager())
            {
                director = manager.GetById(Id);
                if (director == null)
                    return HttpNotFound();
                else
                    return View(director);
            }
        }

        [HttpPost]
        public ActionResult Edit(Director director)
        {
            if (ModelState.IsValid)
            {
                using (manager = new DirectorManager())
                {
                    manager.Update(director);
                }
                if (director == null)
                    return HttpNotFound();
                else
                    return RedirectToAction("Index");
            }
            else
                return View(director.Id);
        }

        [RequireAuthentication]
        [HttpGet]
        public ActionResult Create()
        {
            Director director = new Director();
            return View(director);
        }

        [HttpPost]
        public ActionResult Create(Director director)
        {
            if (ModelState.IsValid)
            {
                using (manager = new DirectorManager())
                {
                    manager.Create(director);
                }
                if (director == null)
                    return HttpNotFound();
                else
                    return RedirectToAction("Index");
            }
            else
                return View(director.Id);
        }

        [RequireAuthentication]
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            Director director;
            using (manager = new DirectorManager())
            {
                director = manager.GetById(Id);
                if (director == null)
                    return HttpNotFound();
                else
                    return View(director);
            }
        }

        [HttpPost]
        public ActionResult Delete(Director director)
        {
            using (manager = new DirectorManager())
            {
                manager.Delete(director.Id);
            }
            if (director == null)
                return HttpNotFound();
            else
                return RedirectToAction("Index");
        }
    }
}