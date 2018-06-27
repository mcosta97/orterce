<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Principal.aspx.cs" Inherits="Principal" %>

<asp:Content ContentPlaceHolderID="generico" Runat="Server">
    <div class="form-group contenedor-55">
        <asp:Repeater ID="d1" runat="server">
            <HeaderTemplate>
                <nav style="display: inline-block" class="navbar navbar-default">
            </HeaderTemplate>

            <ItemTemplate>
                <a class="navbar-brand" style="padding-left:50px; padding-right:50px" href="Principal.aspx?Id=<%# Eval("idcategoria")%>"><%# Eval("nombre")%></a>
            </ItemTemplate>

            <FooterTemplate>
                </nav>
            </FooterTemplate>
        </asp:Repeater>
        <br>
        <br>
        <asp:Repeater ID="p1" runat="server">
            <HeaderTemplate>
                <div class="row">
            </HeaderTemplate>

            <ItemTemplate>
                <div class="col-md-3 presentacion-producto">
                    <a href="Producto.aspx?Id=<%# Eval("idproducto")%>"><img src="<%# Eval("imagen")%>" class="img img-thumbnail" /></a>
                    <div class="product-info">
                        <h3><%# Eval("nombre")%></h3>
                        <div class="product-desc">
                            <h4>Stock: <%# Eval("stock")%></h4>
                            <p><%# Eval("descripcion")%></p>
                            <strong class="price">$<%# Eval("precio")%></strong>
                        </div>
                    </div>
                </div>
            </ItemTemplate>

            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
