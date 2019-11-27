using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.DVDCentral.Models
{
    public class Order
    {
        public int Id;
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate;
        public bool PaymentReceipt { get; set; }
        public DateTime ShipDate { get; set; }
    }
}
