<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>
<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Powered by</h2>
    <p class="credits">
        <a href="http://git-scm.com/">
            Git
        </a><a href="http://eqqon.com/index.php/GitSharp">
            GitSharp
        </a>
        <a href="http://pygments.org/">
            Pygments
        </a>
        <a href="http://jquery.org/">
            jQuery
        </a>
    </p>
    <h2>
        Inspired by</h2>
    <p class="credits">
        <a href="http://github.com/">GitHub</a>
        <a href="http://gitorious.org/">Gitorious</a>
    </p>
    <h2>
        Bits and pieces taken from</h2>
    <p class="credits">
        <a href="http://github.com/devhawk/pygments.wlwriter">devhawk / pygments.wlwriter</a>
        <a href="http://github.com/henon/GitSharp.Demo"> henon / GitSharp.Demo  </a>
        
    </p>
</asp:Content>
