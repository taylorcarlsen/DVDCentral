using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC.DVDCentral.BusinessLogic;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.MVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            if (Security.Identity.IsAuthenticated())
            {
                return RedirectToAction("ManageProfile");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user, string returnUrl)
        {
            using (UserManager manager = new UserManager())
            {
                user = manager.Login(user);
                if (user != null)
                {
                    Security.Identity.Authenticate(user);
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(returnUrl); //redirect uses a url and not conroller like redirect to action does
                }
                else
                {
                    ViewBag.Message = "Login was not possible with the information supplied.";
                    return View(user);
                }
            }
        }

        public ActionResult LogOut()
        {
            Security.Identity.RemoveCurrentUserAuthentication();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                using (UserManager manager = new UserManager())
                {
                    try
                    {
                        int userId = manager.AddUser(user);
                        user.Id = userId;
                        Security.Identity.Authenticate(user);
                        return RedirectToAction("Index", "Home");
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        ViewBag.Message = "Please choose another username";
                        return View(user);
                    }
                }
            }
            else
            {
                return View(user);
            }
        }

        public ActionResult ManageProfile()
        {
            if (Security.Identity.IsAuthenticated())
            {
                return View(Security.Identity.GetCurrentUser());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult ManageProfile(User user)
        {
            if (Security.Identity.IsAuthenticated())
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }
                using (UserManager manager = new UserManager())
                {
                    manager.Update(user);
                    Security.Identity.Authenticate(user);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}