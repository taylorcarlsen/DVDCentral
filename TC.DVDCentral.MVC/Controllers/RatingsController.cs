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
    public class RatingsController : Controller
    {
        RatingManager manager;

        // GET: Ratings
        [RequireAuthentication]
        public ActionResult Index()
        {
            List<Rating> ratings;
            using (manager = new RatingManager())
            {
                ratings = manager.GetAll();
            }
            return View(ratings);
        }

        [RequireAuthentication]
        public ActionResult Details(int Id)
        {
            Rating rating;
            using (manager = new RatingManager())
            {
                rating = manager.GetById(Id);
            }
            if (rating == null)
                return HttpNotFound();
            else
                return View(rating);
        }
        
        [HttpGet]
        [RequireAuthentication]
        public ActionResult Edit(int Id)
        {
            Rating rating;
            using (manager = new RatingManager())
            {
                rating = manager.GetById(Id);
                if (rating == null)
                    return HttpNotFound();
                else
                    return View(rating);
            }
        }

        [HttpPost]
        public ActionResult Edit(Rating rating)
        {
            if (ModelState.IsValid)
            {
                using (manager = new RatingManager())
                {
                    manager.Update(rating);
                }
                if (rating == null)
                    return HttpNotFound();
                else
                    return RedirectToAction("Index");
            }
            else
                return View(rating.Id);
        }

        [HttpGet]
        [RequireAuthentication]
        public ActionResult Create()
        {
            Rating rating = new Rating();
            return View(rating);
        }

        [HttpPost]
        public ActionResult Create(Rating rating)
        {
            if (ModelState.IsValid)
            {
                using (manager = new RatingManager())
                {
                    manager.Create(rating);
                }
                if (rating == null)
                    return HttpNotFound();
                else
                    return RedirectToAction("Index");
            }
            else
                return View(rating.Id);
        }

        [HttpGet]
        [RequireAuthentication]
        public ActionResult Delete(int Id)
        {
            Rating rating;
            using (manager = new RatingManager())
            {
                rating = manager.GetById(Id);
                if (rating == null)
                    return HttpNotFound();
                else
                    return View(rating);
            }
        }

        [HttpPost]
        public ActionResult Delete(Rating rating)
        {
            using (manager = new RatingManager())
            {
                manager.Delete(rating.Id);
            }
            if (rating == null)
                return HttpNotFound();
            else
                return RedirectToAction("Index");
        }


    }
}