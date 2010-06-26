<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GitSharp.Commit>" %>
<div class="commit">
    <%if (Model != null)
      {%>
    <div class="main">
        <span class="message">
            <%= Model.Message %>
        </span>
        <div>
            <div class="person">
                <img src="<%= Url.Gravatar(Model.Author.EmailAddress) %>" alt="<%: Model.Author.Name %>" class="gravatar" />
                <span class="label">
                    <%: Model.Author.Name %></span> <span class="label">(author)</span>
                <div>
                    <%= Html.DateTime(Model.AuthorDate)%>
                </div>
            </div>

            <% if (Model.Committer.Name != Model.Author.Name)
               { %>
            <div class="person">            
                <img src="<%= Url.Gravatar(Model.Committer.EmailAddress) %>" alt="<%: Model.Committer.Name %>" class="gravatar" />
                <span class="label">
                    <%: Model.Committer.Name%></span> <span class="label">(committer)</span>
                <div>
                    <%= Html.DateTime(Model.CommitDate)%>
                </div>
            </div>
            <% } %>
        </div>
    </div>
    <div class="details">
        <ul>
            <li><span class="lbl">commit</span> <a href="<%= Html.CommitUrl(Model.Hash) %>" class="sha">
                <%= Model.ShortHash%></a> </li>
            <li><span class="lbl">tree</span> <a href="<%= Html.TreeUrl(Model.Tree.Hash) %>"
                class="sha">
                <%= Model.Tree.ShortHash %></a> </li>
            <% foreach (var parent in Model.Parents)
               { %>
            <li><span class="lbl">parent</span> <a href="<%= Html.CommitUrl(parent.Hash) %>"
                class="sha">
                <%= parent.ShortHash%></a> </li>
            <% } %>
        </ul>
    </div>
    <%}
      else
      {%>
    No data
    <%} %>
</div>
