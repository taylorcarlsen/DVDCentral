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
    public partial class ManageDirectors : System.Web.UI.Page
    {
        List<Director> AllDirectors;

        protected void Page_Load(object sender, EventArgs e)
        {
            DirectorManager manager = new DirectorManager();
            AllDirectors = manager.GetAll();
            if(!IsPostBack)
            {
                DoDataBind();
            }
            else
            {
                AllDirectors = manager.GetAll();
            }
            manager.Dispose();
        }

        private void DoDataBind()
        {
            DirectorManager manager = new DirectorManager();

            DirectorsDropdownList.DataValueField = "Id";
            DirectorsDropdownList.DataTextField = "FullName";
            DirectorsDropdownList.DataSource = manager.GetAll();
            DirectorsDropdownList.DataBind();
            UpdateTextBoxes();
            manager.Dispose();
        }

        private void UpdateTextBoxes()
        {
            DirectorManager manager = new DirectorManager();
            AllDirectors = manager.GetAll();
            if(AllDirectors.Count > 0)
            {
                int index = DirectorsDropdownList.SelectedIndex;
                var selectedDirector = AllDirectors[index];
                FirstNameTextBox.Text = selectedDirector.FirstName;
                LastNameTextBox.Text = selectedDirector.LastName;
            }
        }

        protected void DirectorsDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextBoxes();
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            DirectorManager manager = new DirectorManager();
            Director director = new Director {FirstName = FirstNameTextBox.Text, LastName = LastNameTextBox.Text };
            director.Id = Convert.ToInt32(DirectorsDropdownList.SelectedValue);
            manager.Update(director);
            DoDataBind();
            manager.Dispose();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            DirectorManager manager = new DirectorManager();
            int id = Convert.ToInt32(DirectorsDropdownList.SelectedValue);
            manager.Delete(id);
            DoDataBind();
            FirstNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            manager.Dispose();
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            DirectorManager manager = new DirectorManager();
            Director director = new Director { FirstName = FirstNameTextBox.Text, LastName = LastNameTextBox.Text};
            manager.Create(director);
            DoDataBind();
            manager.Dispose();
        }
    }
}