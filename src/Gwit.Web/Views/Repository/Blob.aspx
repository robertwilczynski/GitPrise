<%@ Page Title="" Language="C#" MasterPageFile="Repository.Master" Inherits="System.Web.Mvc.ViewPage<BlobViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%--<%: Model.Title %>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("BlobView", Model); %>    
</asp:Content>
