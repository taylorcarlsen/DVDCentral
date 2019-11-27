<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageRatings.aspx.cs" Inherits="TC.DVDCentral.UI.ManageRatings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Rating Management</h2>
        <div class="form-row ml-2 mt-2">
            <div class="control-label col-md-2">
                <asp:Label ID="DropdownLabel" runat="server" Text="Ratings: "></asp:Label>
            </div>
            <div class="control-label col-md-10">
                <asp:DropDownList ID="RatingDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RatingDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-row ml-2 mt-2">
            <div class="control-label col-md-2">
                <asp:Label ID="RatingLabel" runat="server" Text="Rating: "></asp:Label>
            </div>
            <div class="control-label col-md-10">
                <asp:TextBox ID="RatingTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-row ml-2 mt-2">
            <div class="form-group mt-2 ml-2">
                <asp:Button ID="EditButton" runat="server" Text="Update" CssClass="btn btn-primary btn-large ml-3" OnClick="EditButton_Click" />
                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CssClass="btn btn-primary btn-large ml-3" OnClick="DeleteButton_Click" />
                <asp:Button ID="AddButton" runat="server" Text="Add" CssClass="btn btn-primary btn-large ml-3" OnClick="AddButton_Click" />
            </div>
        </div>
        <div class="form-row ml-2 mt-2">
            <div class="form-group mt-2 ml-2">
                <a href="Admin.aspx" class="btn btn-primary btn-large ml-3" role="button">Back to Admin</a>
            </div>
        </div>
    </div>
</asp:Content>
