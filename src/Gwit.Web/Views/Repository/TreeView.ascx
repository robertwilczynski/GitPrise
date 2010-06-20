<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TreeViewModel>" %>
<%= Html.DisplayFor(m => m.PathModel) %>
<div class="filewrapper">
    <div class="file">
        <table class="repo">
            <tr>
                <th>
                </th>
                <th>
                    name
                </th>
                <th>
                    age
                </th>
                <th>
                    message
                </th>
            </tr>
            <% if (!Model.IsRoot)
               { %>
            <tr>
                <td colspan="4">
                    ..
                </td>
            </tr>
            <% } %>
            <% foreach (var item in Model.Items)
               { %>
            <tr>
                <td class="ico">
                    <% if (item.Type == ListItemViewModel.ItemType.Tree)
                       { %>
                    <%= Html.Image("icons/folder.png", "File") %>
                    <% }
                       else
                       {%>
                    <%= Html.Image("icons/file.png", "Folder") %>
                    <% }%>
                </td>
                <td>
                    <a href="<%= Url.Action(item.Type == ListItemViewModel.ItemType.Tree ? "tree" : "blob", "Repository", new{ repositoryName = Model.RepositoryName, id = Model.CommitId, path = item.Path }) %>">
                        <%: item.Name %>
                    </a>
                </td>
                <td>
                    <%= Html.DateTime(item.CommitDate) %>
                </td>
                <td>
                    <%: item.Message %>
                </td>
            </tr>
            <% } %>
        </table>
    </div>
</div>
