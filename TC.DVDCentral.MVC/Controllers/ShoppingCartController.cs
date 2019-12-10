using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC.DVDCentral.BusinessLogic;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoppingCart cart;
        ShoppingCartManager manager;

        [ChildActionOnly]
        public ActionResult CartWidget()
        {
            RetrieveCart();
            return PartialView(cart);
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            RetrieveCart();
            return View(cart);
        }

        public ActionResult Checkout()
        {
            RetrieveCart();
            using (manager = new ShoppingCartManager())
            {
                manager.Cart = cart;
                manager.Checkout();
                PersistCart(manager.Cart);
            }
            return View();
        }

        public ActionResult AddToCart(int id)
        {
            RetrieveCart();
            using (manager = new ShoppingCartManager())
            {
                manager.Cart = cart;
                manager.Add(id);
                PersistCart(manager.Cart);
            }
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult RemoveFromCart(int id)
        {
            RetrieveCart();
            using (manager = new ShoppingCartManager())
            {
                manager.Cart = cart;
                manager.Remove(id);
                PersistCart(manager.Cart);
            }
            return RedirectToAction("Index");
        }

        public ActionResult ClearCart()
        {
            RetrieveCart();
            using (manager = new ShoppingCartManager())
            {
                manager.Cart = cart;
                manager.EmptyCart();
                PersistCart(manager.Cart);
            }
            return RedirectToAction("Index");
        }

        private void PersistCart(ShoppingCart cart)
        {
            Session["Cart"] = cart; //persiting the cart in the session memory
        }

        private void RetrieveCart()
        {
            if (Session["Cart"] == null)
            {
                cart = new ShoppingCart();
                PersistCart(cart);
            }
            else
                using (manager = new ShoppingCartManager())
                {
                    cart = (ShoppingCart)Session["Cart"];
                    if(cart != null)
                    {
                        cart.ITEM_PRICE = manager.CalculateTotal(cart.Items);
                        cart.SalesTax = Math.Round(manager.CalculateSalesTax(cart.ITEM_PRICE),2);
                        cart.Cost = Math.Round(cart.ITEM_PRICE + cart.SalesTax,2);
                    }
                }
        }
    }
}