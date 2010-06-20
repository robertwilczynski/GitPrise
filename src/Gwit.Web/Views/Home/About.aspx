<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Powered by</h2>
    <p>
    <a href="http://git-scm.com/">
        <img src="<%= Url.Image("git.png") %>" alt="Git" />
        </a>
    <a href="http://www.eqqon.com/index.php/GitSharp">
        <img src="<%= Url.Image("gitsharp.png") %>" alt="GitSharp" />
        </a>

    </p>
<%--    <p>
    <a href="">
        <img src="<%= Url.Image("gitorious.png") %>" alt="gitorious" />
        </a>
    </p>
--%>

    <h2>Inspired by</h2>
    
    <p>
    <a href="http://github.com/">
        <img src="<%= Url.Image("github.png") %>" alt="github" />
        </a>
    <a href="http://gitorious.org/">
        <img src="<%= Url.Image("gitorious.png") %>" alt="gitorious" />
        </a>

    </p>
</asp:Content>
