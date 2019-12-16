using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.DVDCentral.Data;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.BusinessLogic
{
    public class OrderManager : IDisposable
    {
        private readonly DVDCentralContext db;
        private UserManager userManager;

        public OrderManager()
        {
            db = new DVDCentralContext();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public int AddOrder(Models.User user, List<Models.Movie> movies)
        {
            MovieManager movieManager = new MovieManager();
            Data.User userFind = db.Users.FirstOrDefault(x => x.Id == user.Id);

            Data.Order newOrder = new Data.Order();
            newOrder.OrderDate = DateTime.UtcNow;
            newOrder.PaymentReceipt = true;
            newOrder.ShipDate = DateTime.UtcNow;
            newOrder.User = userFind;

            db.Orders.Add(newOrder);
            db.SaveChanges();
            int orderId = newOrder.Id;

            foreach(var item in movies)
            {
                Data.Movie movie = db.Movies.SingleOrDefault(d => d.Id == item.Id);

                Data.OrderItem newOrderItem = new Data.OrderItem();
                newOrderItem.Order = newOrder;
                newOrderItem.Movie = movie;
                newOrderItem.Quantity = 1;
                db.OrderItems.Add(newOrderItem);
            }

            db.SaveChanges();

            return orderId;
        }
    }
}
