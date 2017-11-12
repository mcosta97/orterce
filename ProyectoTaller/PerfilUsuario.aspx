<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PerfilUsuario.aspx.cs" Inherits="PerfilUsuario" %>

<asp:Content ID="ctPerfilUsuario" ContentPlaceHolderID="generico" Runat="Server">
    <div class="form-group" style="width:15%; margin-left:auto; margin-right:auto; padding-top:50px">    
        <form runat="server">
            <div class="row">
                <h4>Modificacion de usuario</h4>
                <div id="basico" class="col-md-12 divisor-perfil">
                    <asp:Label runat="server">Nombre</asp:Label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre"></asp:TextBox>
                    <br>
                    <asp:Label runat="server">Apellido</asp:Label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido"></asp:TextBox>
                </div>

                <h4>Cambio de clave</h4>
                <div id="clave" class="col-md-12 divisor-perfil"">
                    <asp:Label runat="server">Clave anterior</asp:Label>
                    <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtAnterior"></asp:TextBox>
                    <br>
                    <asp:Label runat="server">Clave nueva</asp:Label>
                    <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtNueva"></asp:TextBox>
                    <br>
                    <asp:Label runat="server">Reingresar clave nueva</asp:Label>
                    <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtReNueva"></asp:TextBox>
                </div>
                
                <h4>Configuracion de Domicilio</h4>
                <div id="newdom" runat="server" class="col-md-12 divisor-perfil">
                    <asp:Label runat="server">Provincia</asp:Label>
                    <br>
                    <asp:DropDownList ID="ddProvincia" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProvincia_SelectedIndexChanged"></asp:DropDownList>
                    <br>
                    <asp:Label runat="server">Localidad</asp:Label>
                    <br>
                    <asp:DropDownList ID="ddLocalidad" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                    <br>
                    <asp:Label runat="server">Direccion</asp:Label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtDireccion"></asp:TextBox>
                    <br>
                    <asp:Label runat="server">Altura</asp:Label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtAltura"></asp:TextBox>
                    <br>
                    <asp:Label runat="server">Piso</asp:Label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtPiso"></asp:TextBox>
                    <br>
                    <asp:Label runat="server">Departamento</asp:Label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtDpto"></asp:TextBox>
                </div>

                <div id="domicilios" runat="server" class="col-md-12 divisor-perfil">
                    <asp:Repeater ID="dom" runat="server">
                        <HeaderTemplate>
                            <div class="row">
                        </HeaderTemplate>

                        <ItemTemplate>
                            <h4>Direccion: <%# Eval("direccion")%> <%# Eval("altura")%></h4>
                            <h4>Piso: <%# Eval("piso")%> | Departamento: <%# Eval("dpto")%></h4>
                            <h4><%# Eval("provincia")%>, <%# Eval("localidad")%></h4>
                        </ItemTemplate>

                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>

                <h4>Configuracion de Telefono</h4>
                <div id="newtel" runat="server" class="col-md-12 divisor-perfil">
                    <asp:Label runat="server">Telefono</asp:Label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtTelefono"></asp:TextBox>
                </div>

                <div id="telefonos" runat="server" class="col-md-12 divisor-perfil">
                    <asp:Repeater ID="tel" runat="server">
                        <HeaderTemplate>
                            <div class="row">
                        </HeaderTemplate>

                        <ItemTemplate>
                            <h4>Telefono: <%# Eval("telefono")%></h4>
                        </ItemTemplate>

                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>

                <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_Click"></asp:Button>
            </div>
        </form>
    </div>
</asp:Content>
