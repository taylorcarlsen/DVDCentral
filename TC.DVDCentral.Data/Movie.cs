using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.DVDCentral.Data
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal Cost { get; set; }
        public Rating Rating { get; set; }
        public Format Format { get; set; }
        public Director Director { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}
