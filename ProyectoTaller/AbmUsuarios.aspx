<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AbmUsuarios.aspx.cs" Inherits="AbmUsuarios" EnableEventValidation="false" %>

<asp:Content ContentPlaceHolderID="generico" runat="Server">
    <form id="form1" runat="server">
        <div class="contenedor-50">
            <asp:Label runat="server"><h3>Clientes</h3></asp:Label>
            <asp:GridView ID="clientes" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" OnRowCommand="ObtenerClienteSeleccionado" OnRowDataBound="OnRowDataBoundClientes">
                <Columns>
                    <asp:BoundField DataField="idusuario" HeaderText="ID U" SortExpression="idusuario" ReadOnly="True" />
                    <asp:BoundField DataField="idcliente" HeaderText="ID C" SortExpression="idcliente" ReadOnly="True" />
                    <asp:BoundField DataField="usuario" HeaderText="Usuario" SortExpression="usuario" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                    <asp:BoundField DataField="apellido" HeaderText="Apellido" SortExpression="apellido" />
                    <asp:BoundField DataField="mail" HeaderText="Mail" SortExpression="mail" />
                    <asp:BoundField DataField="dni" HeaderText="Dni" SortExpression="dni" />
                </Columns>
            </asp:GridView>

            <br>

            <asp:Label runat="server"><h3>Administrativos</h3></asp:Label>
            <asp:GridView ID="administrativos" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" OnRowCommand="ObtenerAdminSeleccionado" OnRowDataBound="OnRowDataBoundAdmin">
                <Columns>
                    <asp:BoundField DataField="idusuario" HeaderText="ID U" ReadOnly="True" SortExpression="idusuario" />
                    <asp:BoundField DataField="idadministrativo" HeaderText="ID A" SortExpression="idadministrativo" ReadOnly="True" />
                    <asp:BoundField DataField="usuario" HeaderText="Usuario" SortExpression="usuario" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                    <asp:BoundField DataField="apellido" HeaderText="Apellido" SortExpression="apellido" />
                    <asp:BoundField DataField="mail" HeaderText="Mail" SortExpression="mail" />
                    <asp:BoundField DataField="acceso" HeaderText="Acceso" SortExpression="acceso" />
                </Columns>
            </asp:GridView>

            <div class="row" style="margin-bottom: 20px; border:groove; border-radius:10px; padding-top:10px; padding-bottom:10px; margin-top:30px">
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="ID" ID="txtIdE" Enabled="false"></asp:TextBox>
                    <br>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="ID" ID="txtIdU" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Usuario" ID="txtUsuario"></asp:TextBox>
                    <br>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Clave" ID="txtClave" TextMode="Password"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Nombre" ID="txtNombre"></asp:TextBox>
                    <br>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Apellido" ID="txtApellido"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Mail" ID="txtMail"></asp:TextBox>
                    <br>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Dni" ID="txtAdic"></asp:TextBox>
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