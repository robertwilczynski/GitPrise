<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DiffSectionViewModel>" %>
<% foreach (var line in Model.Lines)
   { %>
<tr>
    <td>
        <%: line.LineA %>
    </td>
    <td>
        <%: line.LineB %>
    </td>
    <td>
        <%= Html.DiffLine(line) %></pre>
    </td>
</tr>
<% } %>