using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.BusinessLogic
{
    public class ShoppingCartManager : IDisposable
    {
        public ShoppingCart Cart { get; set; }
        private MovieManager manager;

        public void Dispose()
        {
            if(manager != null)
            {
                manager.Dispose();
            }
        }

        public void Checkout()
        {
            RemoveAll();
        }
        private void RemoveAll()
        {
            if(Cart != null)
            {
                Cart.Items.RemoveAll(x => x.Id != 0);
            }
        }
        public void EmptyCart()
        {
            RemoveAll();
        }
        public void Add(int id)
        {
            using (manager = new MovieManager())
            {
                var movie = manager.GetById(id);
                if(movie != null)
                {
                    Cart.Items.Add(movie);
                }
            }
        }
        public void Remove(int id)
        {
            using (manager = new MovieManager())
            {
                var movie = manager.GetById(id);
                if(movie != null)
                {
                    Cart.Items.RemoveAll(x => x.Id == id);
                }
            }
        }

        public double CalculateTotal(List<Movie> movies)
        {
            double previousCost = 0;
            double currentTotal = 0;
            double currentCost = 0;

            foreach(var movieCost in movies)
            {
                previousCost = currentTotal;
                currentCost = movieCost.Cost;
                currentTotal = currentCost + previousCost;
            }
            //double calculateTax = currentTotal * .05;
            //double totalCost = currentTotal + calculateTax;

            return currentTotal;
        }

        public double CalculateSalesTax(double itemCost)
        {
            double calculate = itemCost * .05;
            return calculate;
        }
    }
}
