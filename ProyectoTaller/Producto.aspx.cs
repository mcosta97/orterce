﻿using ProyectoTallerBussines;
using ProyectoTallerEntity;
using System;

public partial class Productos : System.Web.UI.Page {

    private ProductoEntity producto = null;
    private obPedido bussinesPedido;

    protected void Page_Load(object sender, EventArgs e) {
        bussinesPedido = new obPedido();
        int productoCargado = 0;

        try {
            productoCargado = Convert.ToInt32(Request.QueryString["Id"]);
        } catch(Exception ex) {
            Response.Redirect("Principal.aspx");
        }

        if (productoCargado != 0) {
            TraerProducto(obProducto.CargarProducto(productoCargado));
            for (int i = 1; i < 101; i++) {
                ddCantidad.Items.Add(i.ToString());
            }
        } else {
            Response.Redirect("Principal.aspx");
        }
    }

    protected void TraerProducto(ProductoEntity producto) {
        this.producto = producto;
        imagen.ImageUrl = producto.Imagen;
        titulo.Text = producto.Nombre;
        descripcion.Text = "Descripcion: " + producto.Descripcion;
        stock.Text = "Stock: " + producto.Stock.ToString();
        precio.Text = "$" + obProducto.CalcularPrecioIva(producto);
        iva.Text = "IVA: " + producto.Iva.ToString() + "%";
        modelo.Text = producto.Modelo;
        medidas.Text = "Medidas: " + producto.Medida;
        color.Text = "Color: " + producto.Color;
        peso.Text = "Peso: " + producto.Peso.ToString();
    }

    protected void add_Click(object sender, EventArgs e) {
        DetalleEntity detalle = new DetalleEntity();
        PedidoEntity pedido = (PedidoEntity)Session["PedID"];
        if (Session["UserID"] != null) {
            if (pedido == null) {
                PedidoEntity ped = new PedidoEntity();
                ped.Estado = 4;
                ped.Fecha = DateTime.Now;
                ped.IdCliente = ((UsuarioEntity)Session["UserID"]).IdUsuario;
                Session["PedID"] = ped;
            }
            detalle.Cantidad = 1;
            detalle.IdPedido = 0;
            detalle.IdProducto = producto.IdProducto;
            bussinesPedido.AgregarProducto(detalle, pedido);
        } else {
            Response.Redirect("Login.aspx");
        }
    }
}