<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Carrito.aspx.cs" Inherits="Carrito" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="generico" Runat="Server">
    <div class="contenedor-15">
        <asp:Repeater runat="server" ID="carrito">
            <ItemTemplate>
                <div class="media table-bordered">
                    <div class="media-left">
                        <img src="<%# Eval("imagen")%>" class="media-object" style="width:50px">
                    </div>
                    <div class="media-body">
                        <h4 class="media-heading"><%# Eval("nombre")%> <%# Eval("modelo")%><a href="Carrito.aspx?RemDet=<%# Eval("iddetalle")%>"><span class="glyphicon glyphicon-remove" style="padding-left:38px"></span></a></h4>
                        <p>Cantidad: <%# Eval("cantidad")%> | Precio: $<%# Eval("precio")%></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <br>
        <h3 runat="server" id="total">Total: $0</h3>
        <form runat="server">
            <asp:Button runat="server" Text="Procesar pedido" CssClass="btn btn-success"/>
        </form>
    </div>
</asp:Content>