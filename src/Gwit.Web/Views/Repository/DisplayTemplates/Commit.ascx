<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GitSharp.Commit>" %>
<div class="commit">
    <%if (Model != null)
      {%>
    <div class="main">
        <span class="message">
            <%= Model.Message %>
        </span>
        <div>
            <div>
                <span class="label">
                    <%= Model.Author.Name %></span> <span class="label">(author)</span>
            </div>
            <div>
                <%= Html.DateTime(Model.AuthorDate)%>
            </div>
            <% if (Model.Committer.Name != Model.Author.Name)
               { %>
            <div>
                <span class="label">
                    <%= Model.Committer.Name%></span> <span class="label">(committer)</span>
            </div>
            <div>
                <%= Html.DateTime(Model.CommitDate)%>
            </div>
            <% } %>
        </div>
    </div>
    <div class="details">
        commit
        <%= Model.ShortHash %>
        <br />
        tree
        <%= Model.Tree.ShortHash %>
        <br />
        parent
        <%= Model.Parent.ShortHash %>
    </div>
    <%}
      else
      {%>
      No data
    <%} %>
</div>
