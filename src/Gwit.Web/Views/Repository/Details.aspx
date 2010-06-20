<%@ Page Title="" Language="C#" MasterPageFile="Repository.Master" Inherits="System.Web.Mvc.ViewPage<Gwit.Web.Models.RepositoryDetailsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <div id="repo">
        <% Html.RenderPartial("TreeView", Model.DefaultTree); %>
    </div>
</asp:Content>
