using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.DVDCentral.Data
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool PaymentReceipt { get; set; }
        public DateTime ShipDate { get; set; }
    }
}
