<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Gwit.Web.Models.RepositoriesViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Index</h2>
    <% foreach (var item in Model.List)
       { %>
    <ul class="repo">
        <li class="name">
            <a href="<%= Url.Action("Details", "Repository", new { repositoryName = item.Name }) %>">
                <%: item.Name %></a>
        </li>
        <li class="desc">
            <%: item.Description %></li>
        <li class="stats">
            <%: item.Description %></li>
    </ul>
    <% } %>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
    <script type="text/javascript">
        $(function () {
            $(".repo").corner(10);
        });
    </script>
</asp:Content>
