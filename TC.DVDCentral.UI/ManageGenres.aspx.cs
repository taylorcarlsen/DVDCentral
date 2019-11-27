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
    public partial class ManageGenres : System.Web.UI.Page
    {
        List<Genre> AllGenres;

        protected void Page_Load(object sender, EventArgs e)
        {
            GenreManager manager = new GenreManager();
            AllGenres = manager.GetAll();
            if(!IsPostBack)
            {
                DoDataBind();
            }
            else
            {
                AllGenres = manager.GetAll();
            }
            manager.Dispose();
        }

        private void DoDataBind()
        {
            GenreManager manager = new GenreManager();

            GenreDropDownList.DataValueField = "Id";
            GenreDropDownList.DataTextField = "Description";
            GenreDropDownList.DataSource = manager.GetAll();
            GenreDropDownList.DataBind();
            UpdateTextBoxes();
            manager.Dispose();
        }

        private void UpdateTextBoxes()
        {
            GenreManager manager = new GenreManager();
            AllGenres = manager.GetAll();
            if(AllGenres.Count > 0)
            {
                int index = GenreDropDownList.SelectedIndex;
                var selectedGenre = AllGenres[index];
                GenreTextBox.Text = selectedGenre.Description;
            }
        }

        protected void GenreDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextBoxes();
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            GenreManager manager = new GenreManager();
            Genre genre = new Genre { Description = GenreTextBox.Text };
            genre.Id = Convert.ToInt32(GenreDropDownList.SelectedValue);
            manager.Update(genre);
            DoDataBind();
            manager.Dispose();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            GenreManager manager = new GenreManager();
            int id = Convert.ToInt32(GenreDropDownList.SelectedValue);
            manager.Delete(id);
            DoDataBind();
            GenreTextBox.Text = String.Empty;
            manager.Dispose();
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            GenreManager manager = new GenreManager();
            Genre genre = new Genre { Description = GenreTextBox.Text };
            manager.Create(genre);
            DoDataBind();
            manager.Dispose();
        }
    }
}