using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public double MovieCost { get; set; }
        public string ImagePath { get; set; }
        [Display(Name = "Select a Rating")]
        public List<Rating> PossibleRatings { get; set; }
        public int SelectedRatingId { get; set; }
        [Display(Name = "Select a Format")]
        public List<Format> PossibleFormats { get; set; }
        public int SelectedFormatId { get; set; }
        [Display(Name = "Select a Director")]
        public List<Director> PossibleDirectors { get; set; }
        public int SelectedDirectorId { get; set; }
        [Display(Name = "Select the Genre(s)")]
        public List<Genre> PossibleGenres { get; set; }
        public HttpPostedFileBase File { get; set; }
        public List<int> CurrentGenres { get; set; }
        public DVDCentral.Models.Genre Genre { get; set; }
        public Movie Movie { get; set; }
    }
}