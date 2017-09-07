<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PerfilUsuario.aspx.cs" Inherits="Login" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="ctPerfilUsuario" ContentPlaceHolderID="titulo" Runat="Server">
    Nombre:<br>
    <input type="text" name="nombre">
    <br>
    Apellido:<br>
    <input type="text" name="apellido">
    <br><br>
    <input type="submit" value="Modificar">
</asp:Content>
