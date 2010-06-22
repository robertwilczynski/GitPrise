<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GitSharp.Change>" %>

<div>
    <div><%: Model.Path %></div>
    <div> Diff will go here 
<%--    <% 
        try
        {
            var diff = new GitSharp.Diff(Model.ReferenceObject as GitSharp.Blob, Model.ComparedObject as GitSharp.Blob);
            foreach (var section in diff.Sections)
            { %>
       <%= section.EditWithRespectToA..ToString()%>
    <% }
        }
        catch (Exception) { }%>
--%>    </div>
</div>