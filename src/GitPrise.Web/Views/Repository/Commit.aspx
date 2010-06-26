<%@ Page Title="" Language="C#" MasterPageFile="Repository.Master" Inherits="System.Web.Mvc.ViewPage<CommitViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="changeset" class="separate">
        <tbody>
            <% foreach (var change in Model.CurrentCommit.Changes)
               { %>
            <tr>
                <td class="squash">
                    <% switch (change.ChangeType)
                       {
                           case GitSharp.ChangeType.Added:
                    %>
                    <%= Html.Image("diff-add.png", "added") %>
                    <%
                        break;
                           case GitSharp.ChangeType.Copied:
                    %>
                    <%= Html.Image("diff-add.png", "copied") %>
                    <%             
                        break;
                           case GitSharp.ChangeType.Deleted:
                    %>
                    <%= Html.Image("diff-remove.png", "deteled") %>
                    <%              
                        break;
                           case GitSharp.ChangeType.Modified:
                    %>
                    <%= Html.Image("diff-modify.png", "modified") %>
                    <%              
                        break;
                           case GitSharp.ChangeType.Renamed:
                    %>
                    <%= Html.Image("diff-modify.png", "renamed")%>
                    <%              
                        break;
                           case GitSharp.ChangeType.TypeChanged:
                    %>
                    <%= Html.Image("diff-modify.png", "type changed") %>
                    <%              
                        break;
                       } %>
                </td>
                <td>
                    <a href="#<%: change.Name %>">
                        <%: change.Path %>
                    </a>
                </td>
                <td class="squash">
                    summary
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <div id="changes">
        <% foreach (var change in Model.Changes)
           { %>
        <div class="change">
            <div id="<%: change.Name %>" class="bar">
                <%= Html.Image("file.txt.png", "", new { @class="ico" })%>
                <%: change.Name%>
                <a class="change-link" href="<%= Html.BlobUrl(change.Change.ComparedCommit.Hash, change.Change.Path) %>">
                    View file @
                    <%: change.Change.ComparedCommit.ShortHash%></a>
            </div>
            <% Html.RenderPartial("ChangeView", change); %>
        </div>
        <% } %>
    </div>
</asp:Content>
