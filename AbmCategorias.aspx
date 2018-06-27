<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AbmCategorias.aspx.cs" Inherits="AbmCategoria" EnableEventValidation="false" %>

<asp:Content ContentPlaceHolderID="generico" Runat="Server">
    <form id="form1" runat="server">
        <div class="contenedor-50">
            <asp:Label runat="server"><h3>Categorias</h3></asp:Label>
                <asp:GridView ID="categorias" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" OnRowCommand="ObtenerCategoriaSeleccionado" OnRowDataBound="OnRowDataBoundProductos">
                <Columns>
                    <asp:BoundField DataField="idcategoria" HeaderText="Categoria" SortExpression="idcategoria" ReadOnly="True" />
                    <asp:BoundField DataField="categoria" HeaderText="Nombre" SortExpression="categoria" />
                </Columns>
            </asp:GridView>

            <div class="row" style="margin-bottom:20px; border:groove; border-radius:10px; padding-top:10px; padding-bottom:10px; margin-top:30px">
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="ID" ID="txtId" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" CssClass="form-control" placeholder="Categoria" ID="txtCategoria"></asp:DropDownList>
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