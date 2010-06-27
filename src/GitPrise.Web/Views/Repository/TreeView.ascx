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
            <% for (int index = 0; index < Model.Items.Count; index++)                   
               {
                   var item = Model.Items[index];
                   %>
            <tr <%= (index % 2 == 0) ? @"class=""alt""" : String.Empty %>>
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
                    <a href="<%= Url.GitUrl(item.Type == ListItemViewModel.ItemType.Tree ? "tree" : "blob", Model) %>">
                        <%: item.Name %>
                    </a>
                </td>
                <td>
                    <%: Html.DateTime(item.CommitDate) %>
                </td>
                <td>
                    <%: item.Message %>
                </td>
            </tr>
            <% } %>
        </table>
    </div>
</div>
