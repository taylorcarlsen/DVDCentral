﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.DVDCentral.Models
{
    public class Order
    {
        public int Id;
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public bool PaymentReceipt { get; set; }
        public DateTime ShipDate { get; set; }
    }
}
