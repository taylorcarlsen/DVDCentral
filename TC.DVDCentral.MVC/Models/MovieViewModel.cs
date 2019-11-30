using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.MVC.Models
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string MovieDescription { get; set; }
        public decimal MovieCost { get; set; }
        
             
        public string ImagePath { get; set; }
        public List<Rating> PossibleRatings { get; set; }
        public int SelectedRatingId { get; set; }
        public List<Format> PossibleFormats { get; set; }
        public int SelectedFormatId { get; set; }
        public List<Director> PossibleDirectors { get; set; }
        public int SelectedDirectorId { get; set; }
        public List<Genre> PossibleGenres { get; set; }
        public HttpPostedFileBase File { get; set; }
        public List<int> CurrentGenres { get; set; }
        public DVDCentral.Models.Genre Genre { get; set; }
        public Movie Movie { get; set; }
    }
}