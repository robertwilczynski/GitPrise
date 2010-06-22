<%@ Page Title="" Language="C#" MasterPageFile="Repository.Master" Inherits="System.Web.Mvc.ViewPage<CommitViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table>
<tbody>
<% foreach (var change in Model.CurrentCommit.Changes)
   { %>
   <tr>
   <td>icon / new/ mod / del</td>
   <td><%: change.Path %></td>   
   <td>summary</td>
   </tr>
<% } %>
</tbody>
</table>
<% foreach (var change in Model.CurrentCommit.Changes)
   { %>
   <% Html.RenderPartial("ChangeView", change); %>
<% } %>
</asp:Content>
