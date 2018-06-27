<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Producto.aspx.cs" Inherits="Productos" %>

<asp:Content ContentPlaceHolderID="generico" Runat="Server">
    <div class="form-group">
        <form id="prod" runat="server">
            <div class="row contenedor-50">
                <div class="col-md-5">
                    <asp:Image runat="server" CssClass="img-thumbnail" ID="imagen" />
                </div>

                <div class="col-md-6 titulo-producto">
                    <asp:Label runat="server" Font-Size="XX-Large" ID="titulo"></asp:Label><br>
                    <asp:Label runat="server" Font-Size="Large" ID="modelo"></asp:Label><br>
                    <asp:Label runat="server" ID="stock"></asp:Label><br><br>
                    <asp:Label runat="server" Font-Size="Large" ID="precio"></asp:Label><br>
                    <asp:Label runat="server" Font-Size="Small" ID="iva"></asp:Label>

                    <div class="row" style="margin-top:30px">
                        <div class="col-md-4"><asp:DropDownList ID="ddCantidad" CssClass="form-control" runat="server"></asp:DropDownList></div>
                        <div class="col-md-8"><asp:Button runat="server" CssClass="btn btn-block btn-info btn-sm" ID="add" Text="Agregar al carrito" OnClick="add_Click" /></div>
                    </div>
                </div>
            </div>
            <div class="container descripcion-producto">
                <asp:Label runat="server" ID="descripcion"></asp:Label><br>
                <asp:Label runat="server" ID="color"></asp:Label><br>
                <asp:Label runat="server" ID="medidas"></asp:Label><br>
                <asp:Label runat="server" ID="peso"></asp:Label>
            </div>
        </form>
    </div>
</asp:Content>