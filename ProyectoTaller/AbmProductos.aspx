<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AbmProductos.aspx.cs" Inherits="AbmProductos" EnableEventValidation="false" %>

<asp:Content ContentPlaceHolderID="generico" Runat="Server">
    <form id="form1" runat="server">
        <div class="contenedor-50">
            <asp:Label runat="server"><h3>Productos</h3></asp:Label>
                <asp:GridView ID="productos" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" OnRowCommand="ObtenerProductoSeleccionado" OnRowDataBound="OnRowDataBoundProductos">
                <Columns>
                    <asp:BoundField DataField="idproducto" HeaderText="ID" SortExpression="idproducto" ReadOnly="True" />
                    <asp:BoundField DataField="idcategoria" HeaderText="Categoria" SortExpression="idcategoria" ReadOnly="True" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                    <asp:BoundField DataField="descripcion" HeaderText="Descripcion" SortExpression="descripcion" />
                    <asp:BoundField DataField="precio" HeaderText="Precio" SortExpression="precio" />
                    <asp:BoundField DataField="iva" HeaderText="Iva" SortExpression="iva" />
                    <asp:BoundField DataField="stock" HeaderText="Stock" SortExpression="stock" />
                    <asp:BoundField DataField="peso" HeaderText="Peso" SortExpression="peso" />
                    <asp:BoundField DataField="color" HeaderText="Color" SortExpression="color" />
                    <asp:BoundField DataField="modelo" HeaderText="Modelo" SortExpression="modelo" />
                    <asp:BoundField DataField="medida" HeaderText="Medida" SortExpression="medida" />
                    <asp:BoundField DataField="imagen" HeaderText="Imagen" SortExpression="imagen" />
                </Columns>
            </asp:GridView>

            <div class="row" style="margin-bottom:20px; border:groove; border-radius:10px; padding-top:10px; padding-bottom:10px; margin-top:30px">
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="ID" ID="txtId" Enabled="false"></asp:TextBox>
                    <br>
                    <asp:DropDownList runat="server" CssClass="form-control" placeholder="Categoria" ID="ddCategoria"></asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Nombre" ID="txtNombre"></asp:TextBox>
                    <br>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Descripcion" ID="txtDescripcion"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Precio" ID="txtPrecio"></asp:TextBox>
                    <br>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Iva" ID="txtIva"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Stock" ID="txtStock"></asp:TextBox>
                    <br>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Peso" ID="txtPeso"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Modelo" ID="txtModelo"></asp:TextBox>
                    <br>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Medida" ID="txtMedida"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Imagen" ID="txtImagen"></asp:TextBox>
                </div>
            </div>

            <div>
                <asp:Button runat="server" Width="32%" CssClass="btn btn-primary" Text="Alta" ID="btnAlta" OnClick="btnAlta_Click"></asp:Button>
                <asp:Button runat="server" Width="32%" CssClass="btn btn-danger" Text="Baja" ID="btnBaja" OnClick="btnBaja_Click"></asp:Button>
                <asp:Button runat="server" Width="32%" CssClass="btn btn-warning" Text="Modificar" ID="btnEdit" OnClick="btnEdit_Click"></asp:Button>
            </div>
        </div>
    </form>
</asp:Content>