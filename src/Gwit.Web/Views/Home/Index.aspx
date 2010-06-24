<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Gwit.Web.Models.RepositoriesViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Repositories</h2>
        Search <input id="filter" type="text" />
        <ul class="repolist">

    <% foreach (var item in Model.List)
       { %>
    <li id="<%: item.Name %>" class="repo">                
        <div class="head">
            <img src="<%= Url.Image("repo_big.png") %>" alt="<%: item.Name %>" class="hico" />
            <a href="<%= Url.Action("Details", "Repository", new { repositoryName = item.Name }) %>">
                <%: item.Name %></a>
            <div class="content">last change by <%: item.CurrentCommit.Author.Name %> <%: Html.DateTime(item.CurrentCommit.AuthorDate) %> 
            commited <% if (!item.CurrentCommit.IsCommittedByAuthor) { %>by <%: item.CurrentCommit.Committer.Name %> <% } %>
            
            <%: Html.DateTime(item.CurrentCommit.CommitDate) %> </div>
        </div>
        <div class="foot">
            
        </div>
    </li>
    <% } %>

    </ul>
    <script type="text/javascript">
        $(function () {

            $("#filter").keyup(function () {
                var pattern = $("#filter").val();
                $(".repo").each(function (idx, el) {
                    if (el.id.indexOf(pattern) > -1) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });

            });

        });
    </script>
</asp:Content>
