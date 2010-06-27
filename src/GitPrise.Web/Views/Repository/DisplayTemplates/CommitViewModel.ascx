<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CommitViewModel>" %>
<div class="commit">
    <%if (Model != null)
      {%>
    <div class="main">
        <span class="message">
            <%= Model.Commit.Message%>
        </span>
        <div>
            <div class="person">
                <img src="<%= Url.Gravatar(Model.Commit.Author.EmailAddress) %>" alt="<%: Model.Commit.Author.Name %>" class="gravatar" />
                <span class="label">
                    <%: Model.Commit.Author.Name%></span> <span class="label">(author)</span>
                <div>
                    <%= Html.DateTime(Model.Commit.AuthorDate)%>
                </div>
            </div>

            <% if (Model.Commit.Committer.Name != Model.Commit.Author.Name)
               { %>
            <div class="person">            
                <img src="<%= Url.Gravatar(Model.Commit.Committer.EmailAddress) %>" alt="<%: Model.Commit.Committer.Name %>" class="gravatar" />
                <span class="label">
                    <%: Model.Commit.Committer.Name%></span> <span class="label">(committer)</span>
                <div>
                    <%= Html.DateTime(Model.Commit.CommitDate)%>
                </div>
            </div>
            <% } %>
        </div>
    </div>
    <div class="details">
        <ul>
            <li><span class="lbl">commit</span> <a href="<%: Url.CommitUrl(Model) %>" class="sha">
                <%: Model.Commit.ShortHash %></a> </li>
            <li><span class="lbl">tree</span> <a href="<%: Url.TreeUrl(Model.Tree) %>"
                class="sha">
                <%: Model.Tree.Node.ShortHash %></a> </li>
            <% foreach (var parent in Model.Parents)
               { %>
            <li><span class="lbl">parent</span> <a href="<%: Url.CommitUrl(parent) %>"
                class="sha">
                <%: parent.Commit.ShortHash %></a> </li>
            <% } %>
        </ul>
    </div>
    <%}
      else
      {%>
    No data
    <%} %>
</div>
