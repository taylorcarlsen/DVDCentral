using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TC.DVDCentral.BusinessLogic;

namespace TC.DVDCentral.UI
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RatingManager ratingManager = new RatingManager();

            RatingDropDownList.DataValueField = "Id";
            RatingDropDownList.DataTextField = "Description";
            RatingDropDownList.DataSource = ratingManager.GetAll();
            RatingDropDownList.DataBind();

        }
    }
}