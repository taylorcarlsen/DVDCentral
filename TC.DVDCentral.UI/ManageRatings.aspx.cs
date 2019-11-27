using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TC.DVDCentral.BusinessLogic;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.UI
{
    public partial class ManageRatings : System.Web.UI.Page
    {
        List<Rating> AllRatings;

        protected void Page_Load(object sender, EventArgs e)
        {
            RatingManager manager = new RatingManager();
            AllRatings = manager.GetAll();
            if(!IsPostBack)
            {
                DoDataBind();
            }
            else
            {
                AllRatings = manager.GetAll();
            }
            manager.Dispose();
        }

        private void DoDataBind()
        {
            RatingManager manager = new RatingManager();

            RatingDropDownList.DataValueField = "Id";
            RatingDropDownList.DataTextField = "Description";
            RatingDropDownList.DataSource = manager.GetAll();
            RatingDropDownList.DataBind();
            UpdateTextBoxes();
            manager.Dispose();
        }

        private void UpdateTextBoxes()
        {
            RatingManager manager = new RatingManager();
            AllRatings = manager.GetAll();
            if (AllRatings.Count > 0)
            {
                int index = RatingDropDownList.SelectedIndex;
                var selectedRating = AllRatings[index];
                RatingTextBox.Text = selectedRating.Description;
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            RatingManager manager = new RatingManager();
            Rating rating = new Rating { Description = RatingTextBox.Text };
            rating.Id = Convert.ToInt32(RatingDropDownList.SelectedValue);
            manager.Update(rating);
            DoDataBind();
            manager.Dispose();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            RatingManager manager = new RatingManager();
            int id = Convert.ToInt32(RatingDropDownList.SelectedValue);
            manager.Delete(id);
            DoDataBind();
            RatingTextBox.Text = string.Empty;
            manager.Dispose();
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            RatingManager manager = new RatingManager();
            Rating rating = new Rating { Description = RatingTextBox.Text };
            manager.Create(rating);
            DoDataBind();
            manager.Dispose();
        }

        protected void RatingDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextBoxes();
        }
    }
}