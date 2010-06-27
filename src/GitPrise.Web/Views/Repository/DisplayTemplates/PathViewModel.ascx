<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PathViewModel>" %>
<ul id="path" class="breadcrumb">
    <li><a href="<%: Url.GitUrl("tree", Model.Root.Navigation) %>">
        <%: Model.Root.Text %></a></li>
    <% foreach (var element in Model.Elements)
       { %>
    <li><a href="<%: Url.GitUrl("tree", element.Navigation) %>">
        <%: element.Text%></a></li>
    <% } %>
    <% if (!Model.IsRootEqualToCurrentItem)
       { %>
    <li><span>
        <%: Model.CurrentItem.Text%></span></li>
    <% } %>
</ul>
