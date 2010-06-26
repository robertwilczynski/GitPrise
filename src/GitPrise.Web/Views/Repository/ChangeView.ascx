<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ChangeViewModel>" %>

<div class="filewrapper">
    <div class="file">    
        <table class="code">
            <tbody>
                <% foreach (var section in Model.Diff.Sections)
                   {
                       %>
                <%foreach (var line in new DiffSectionViewModel(section).Lines)
                   { %>
                <tr>
                    <td class="line">
                        <%: line.LineA %>
                    </td>
                    <td class="line">
                        <%: line.LineB %>
                    </td>
                    <td>
                        <%= Html.DiffLine(line) %>
                    </td>
                </tr>
                <% } %>
                <% }%>
            </tbody>
        </table>
    </div>
</div>
