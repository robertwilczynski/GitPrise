<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Gwit.Web.Models.RepositoriesViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Index</h2>
        <ul class="repolist">
        <li>
    <% foreach (var item in Model.List)
       { %>
    <ul class="repo">
        <li class="head">
            <img src="<%= Url.Image("repo.png") %>" alt="<%: item.Name %>" class="hico" />
            
            <a href="<%= Url.Action("Details", "Repository", new { repositoryName = item.Name }) %>">
                <%: item.Name %></a>
        </li>
        <li class="content">            
        </li>
        <li class="foot">
        <div>last change by <%: item.CurrentCommit.Author.Name %> <%: Html.DateTime(item.CurrentCommit.AuthorDate) %> </div>
        <div>commited by <%: item.CurrentCommit.Committer.Name %> <%: Html.DateTime(item.CurrentCommit.CommitDate) %> </div>
            </li>
    </ul>
    <% } %>
    </li>
    </ul>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
<%--    <script type="text/javascript">
        $(function () {
            $(".repo").corner(10);
        });
    </script>--%>
</asp:Content>
