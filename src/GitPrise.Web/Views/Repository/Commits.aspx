<%@ Page Title="" Language="C#" MasterPageFile="RepositoryBase.Master" Inherits="System.Web.Mvc.ViewPage<CommitsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Commits
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Commits</h2>
    <% foreach (var group in Model.Grouping)
       { 
           
    %>
    <h3>
        <%= group.Key.ToShortDateString() %></h3>
    <% foreach (var commit in group)
       { %>
    <div class="commit-wrap separate">
        <%= Html.DisplayFor(m => commit) %>
    </div>
    <% } %>
    <% } %>
</asp:Content>
