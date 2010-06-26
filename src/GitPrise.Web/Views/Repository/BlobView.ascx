<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BlobViewModel>" %>
<%= Html.DisplayFor(m => m.PathModel) %>
<div class="filewrapper">
    <div class="file">
        <%= Model.FormattedData %>
    </div>
</div>
