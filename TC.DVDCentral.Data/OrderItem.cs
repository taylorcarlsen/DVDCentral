using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.DVDCentral.Data
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Movie Movie { get; set; }
        public int Quantity { get; set; }
    }
}
