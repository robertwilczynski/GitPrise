<%@ Page Title="" Language="C#" MasterPageFile="Repository.Master" Inherits="System.Web.Mvc.ViewPage<TreeViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Tree
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("TreeView", Model); %>    
</asp:Content>
