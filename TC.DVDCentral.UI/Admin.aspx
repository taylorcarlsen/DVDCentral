<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="TC.DVDCentral.UI.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Admin Page</h2>
    <asp:Table ID="AdminTable" runat="server"
        CellPadding="10"
        GridLines="Both"
        HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell>
                <!--<asp:Button ID="DirectorsButton" runat="server" Text="Directors" CssClass="btn btn-primary btn-large ml-3" href="~/ManageDirectors" />-->
                <a href="ManageDirectors.aspx" class="btn btn-primary btn-large ml-3" role="button">Directors</a>
            </asp:TableCell>
            <asp:TableCell>
                <!--<asp:Button ID="GenreButton" runat="server" Text="Genres" CssClass="btn btn-primary btn-large ml-3" />-->
                <a href="ManageGenres.aspx" class="btn btn-primary btn-large ml-3" role="button">Genres</a>
            </asp:TableCell>
            <asp:TableCell>
                <!--<asp:Button ID="MovieButton" runat="server" Text="Movies" CssClass="btn btn-primary btn-large ml-3" />-->
                <a href="ManageMovies.aspx" class="btn btn-primary btn-large ml-3" role="button">Movies</a>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <!--<asp:Button ID="OrderButton" runat="server" Text="Buttons" CssClass="btn btn-primary btn-large ml-3" />-->
                <a href="ManageOrders.aspx" class="btn btn-primary btn-large ml-3" role="button">Orders</a>
            </asp:TableCell>
            <asp:TableCell>
                <!--<asp:Button ID="RatingButton" runat="server" Text="Rating" CssClass="btn btn-primary btn-large ml-3" />-->
                <a href="ManageRatings.aspx" class="btn btn-primary btn-large ml-3" role="button">Ratings</a>
            </asp:TableCell>
            <asp:TableCell>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
