using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.MVC.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal Cost { get; set; }
        public List<Rating> PossibleRatings { get; set; }
        public int SelectedRatingId { get; set; }
        public List<Format> PossibleFormats { get; set; }
        public int SelectedFormatId { get; set; }
        public List<Director> PossibleDirectors { get; set; }
        public int SelectedDirectorId { get; set; }
        public List<Genre> PossibleGenres { get; set; }
        public HttpPostedFileBase File { get; set; }
        public List<int> CurrentGenres { get; set; }
        public DVDCentral.Models.Movie Movie { get; set; }
    }
}