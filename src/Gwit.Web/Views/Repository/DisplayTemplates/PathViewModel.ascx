<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PathViewModel>" %>

<ul id="path" class="breadcrumb">
    <li><a href="<%: Model.Root.Url %>"><%: Model.Root.Text %></a></li>
     
    <% foreach (var element in Model.Elements)
       { %>
    <li><a href="<%: element.Url %>"><%: element.Text%></a></li>
     
    <% } %>
    <% if (!Model.IsRootEqualToCurrentItem)
       { %>
    <li><span><%: Model.CurrentItem.Text%></span></li>
    <% } %>
</ul>
