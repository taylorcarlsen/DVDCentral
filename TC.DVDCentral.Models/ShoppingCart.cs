using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.DVDCentral.Models
{
    public class ShoppingCart
    {
        public List<Movie> Items { get; set; }
        public int Count { get { return Items.Count; } }
        public double ITEM_PRICE { get; set; }
        public double SalesTax { get; set; }
        public double Cost { get; set; }
        public ShoppingCart()
        {
            Items = new List<Movie>();
        }
    }
}
