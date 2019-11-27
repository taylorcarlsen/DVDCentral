using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.DVDCentral.Data
{
    public class Genre
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
