using ProyectoTallerDataODBC;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProyectoTallerData.daComun;

namespace ProyectoTallerData {
    public class daProducto {

        private const string SQLSearchByPrimaryKey = "SELECT * FROM Productos WHERE IdProducto = @IdProducto";
        private const string SQLSearch = "SELECT * FROM Productos WHERE IdCategoria = @IdCategoria";
        private const string SQLSearchTabla = "SELECT * FROM Productos";
        private const string SQLSearchTablaId = "SELECT * FROM Productos WHERE IdProducto = @IdProducto";
        private const string SQLInsert = "INSERT INTO Productos (IdProducto, IdCategoria, Nombre, Descripcion, Precio, Iva, Stock, Peso, Color, Modelo, Medida, Imagen) VALUES ((SELECT MAX(IdProducto) + 1 FROM Productos), @Nombre, @Descripcion, @Precio, @Iva, @Stock, @Peso, @Color, @Modelo, @Medida, @Imagen)";
        private const string SQLUpdate = "UPDATE Productos SET IdCategoria = @IdCategoria, Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, Iva = @Iva, Stock = @Stock, Peso = @Peso, Color = @Color, Modelo = @Modelo, Medida = @Medida, Imagen = @Imagen WHERE IdProducto = @IdProducto";
        private const string SQLDelete = "DELETE FROM Productos WHERE IdProducto = @IdProducto";
        private const string SQLPromocionado = "SELECT * FROM Promocionados X INNER JOIN Productos P ON X.IdProducto = P.IdProducto";
        private const string SQLPublicitado = "SELECT * FROM Publicitados X INNER JOIN Productos P ON X.IdProducto = P.IdProducto";

        private daConexion connectionDA = new daConexion();

        public daProducto() { }

        public ProductoEntity CrearEntidad(SqlDataReader dr) {
            ProductoEntity entidad = new ProductoEntity();
            entidad.IdProducto = Convert.ToInt32(dr["IdProducto"]);
            entidad.IdCategoria = Convert.ToInt32(dr["IdCategoria"]);
            entidad.Nombre = dr["Nombre"].ToString();
            entidad.Descripcion = dr["Descripcion"].ToString();
            entidad.Precio = Convert.ToDouble(dr["Precio"]);
            entidad.Iva = Convert.ToInt32(dr["Iva"]);
            entidad.Stock = Convert.ToInt32(dr["Stock"]);
            entidad.Peso = Convert.ToInt32(dr["Peso"]);
            entidad.Color = dr["Color"].ToString();
            entidad.Modelo = dr["Modelo"].ToString();
            entidad.Medida = dr["Medida"].ToString();
            if (dr["Imagen"].ToString().Equals("")) {
                entidad.Imagen = "img/0.jpg";
            } else {
                entidad.Imagen = dr["Imagen"].ToString();
            }
            return entidad;
        }

        private void CrearParametros(SqlCommand command, ProductoEntity entidad) {
            SqlParameter parameter = null;

            parameter = command.Parameters.Add("@Nombre", SqlDbType.VarChar);
            parameter.Value = entidad.Nombre;

            parameter = command.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            parameter.Value = entidad.Descripcion;

            parameter = command.Parameters.Add("@Precio", SqlDbType.Decimal);
            parameter.Value = entidad.Precio;

            parameter = command.Parameters.Add("@Iva", SqlDbType.Int);
            parameter.Value = entidad.Iva;

            parameter = command.Parameters.Add("@Stock", SqlDbType.Int);
            parameter.Value = entidad.Stock;

            parameter = command.Parameters.Add("@Peso", SqlDbType.Int);
            parameter.Value = entidad.Peso;

            parameter = command.Parameters.Add("@Color", SqlDbType.VarChar);
            parameter.Value = entidad.Color;

            parameter = command.Parameters.Add("@Modelo", SqlDbType.VarChar);
            parameter.Value = entidad.Modelo;

            parameter = command.Parameters.Add("@Medida", SqlDbType.VarChar);
            parameter.Value = entidad.Medida;

            parameter = command.Parameters.Add("@Imagen", SqlDbType.VarChar);
            parameter.Value = entidad.Imagen;
        }

        private void EjecutarComando(TipoComando sqlCommandType, ProductoEntity entidad) {
            SqlConnection connection = null;
            SqlCommand command = null;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                IDataParameter paramId = new SqlParameter("@IdProducto", SqlDbType.Int);
                paramId.Value = entidad.IdProducto;

                switch (sqlCommandType) {
                    case TipoComando.Insertar:
                        command = new SqlCommand(SQLInsert, connection);
                        command.Parameters.Add(paramId);
                        CrearParametros(command, entidad);
                        break;

                    case TipoComando.Actualizar:
                        command = new SqlCommand(SQLUpdate, connection);
                        command.Parameters.Add(paramId);
                        CrearParametros(command, entidad);
                        break;

                    case TipoComando.Eliminar:
                        command = new SqlCommand(SQLDelete, connection);
                        command.Parameters.Add(paramId);
                        CrearParametros(command, entidad);
                        break;
                }

                command.ExecuteNonQuery();
                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                if (command != null) {
                    command.Dispose();
                }

                if (connection != null) {
                    connection.Dispose();
                }
            }
        }

        public DataTable ObtenerProductosPromocionadosTabla() {
            List<ProductoEntity> productos = ObtenerProductosPromocionados();
            DataTable dt = new DataTable();
            dt.Columns.Add("IdProducto"); dt.Columns.Add("IdCategoria"); dt.Columns.Add("Nombre");
            dt.Columns.Add("Descripcion"); dt.Columns.Add("Color"); dt.Columns.Add("Peso");
            dt.Columns.Add("Modelo"); dt.Columns.Add("Medida"); dt.Columns.Add("Imagen");
            dt.Columns.Add("Iva"); dt.Columns.Add("Precio"); dt.Columns.Add("Stock");


            foreach (ProductoEntity producto in productos) {
                dt.Rows.Add(producto.IdProducto, producto.IdCategoria, producto.Nombre, producto.Descripcion,
                            producto.Color, producto.Peso, producto.Modelo, producto.Medida, producto.Imagen,
                            producto.Iva, producto.Precio, producto.Stock);
            }

            return dt;
        }

        public DataTable ObtenerProductosPublicitadosTabla() {
            List<ProductoEntity> productos = ObtenerProductosPublicitados();
            DataTable dt = new DataTable();
            dt.Columns.Add("IdProducto"); dt.Columns.Add("IdCategoria"); dt.Columns.Add("Nombre");
            dt.Columns.Add("Descripcion"); dt.Columns.Add("Color"); dt.Columns.Add("Peso");
            dt.Columns.Add("Modelo"); dt.Columns.Add("Medida"); dt.Columns.Add("Imagen");
            dt.Columns.Add("Iva"); dt.Columns.Add("Precio"); dt.Columns.Add("Stock");

            foreach(ProductoEntity producto in productos) {
                dt.Rows.Add(producto.IdProducto, producto.IdCategoria, producto.Nombre, producto.Descripcion,
                            producto.Color, producto.Peso, producto.Modelo, producto.Medida, producto.Imagen,
                            producto.Iva, producto.Precio, producto.Stock);
            }

            return dt;
        }

        public DataTable ObtenerProductosTabla() {
            List<ProductoEntity> productos = ObtenerProductos();
            List<CategoriaEntity> categorias = new daCategoria().ObtenerCategorias();
            DataTable dt = new DataTable();
            dt.Columns.Add("IdProducto"); dt.Columns.Add("IdCategoria"); dt.Columns.Add("Nombre");
            dt.Columns.Add("Descripcion"); dt.Columns.Add("Color"); dt.Columns.Add("Peso");
            dt.Columns.Add("Modelo"); dt.Columns.Add("Medida"); dt.Columns.Add("Imagen");
            dt.Columns.Add("Iva"); dt.Columns.Add("Precio"); dt.Columns.Add("Stock");

            foreach (ProductoEntity producto in productos) {
                dt.Rows.Add(producto.IdProducto, categorias.Find(a => producto.IdCategoria == a.IdCategoria).Nombre, producto.Nombre, producto.Descripcion, 
                            producto.Color, producto.Peso, producto.Modelo, producto.Medida, producto.Imagen, 
                            producto.Iva, producto.Precio, producto.Stock);
            }

            return dt;
        }

        public DataTable ObtenerProductosPorCategoriaTabla(int idcategoria) {
            List<ProductoEntity> productos = ObtenerProductosPorCategoria(idcategoria);
            DataTable dt = new DataTable();
            dt.Columns.Add("IdProducto"); dt.Columns.Add("IdCategoria"); dt.Columns.Add("Nombre");
            dt.Columns.Add("Descripcion"); dt.Columns.Add("Color"); dt.Columns.Add("Peso");
            dt.Columns.Add("Modelo"); dt.Columns.Add("Medida"); dt.Columns.Add("Imagen");
            dt.Columns.Add("Iva"); dt.Columns.Add("Precio"); dt.Columns.Add("Stock");

            foreach(ProductoEntity producto in productos) {
                dt.Rows.Add(producto.IdProducto, producto.IdCategoria, producto.Nombre, producto.Descripcion,
                            producto.Color, producto.Peso, producto.Modelo, producto.Medida, producto.Imagen,
                            producto.Iva, producto.Precio, producto.Stock);
            }

            return dt;
        }

        public List<ProductoEntity> ObtenerProductos() {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            List<ProductoEntity> productos;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchTabla, connection);
                dr = command.ExecuteReader();
                productos = new List<ProductoEntity>();

                while (dr.Read()) {
                    productos.Add(CrearEntidad(dr));
                }

                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;
                if (command != null) {command.Dispose();}
                if (connection != null) {connection.Dispose();}
            }

            return productos;
        }

        public DataTable ObtenerProductosPorIdTabla(int id) {
            SqlConnection connection = null; // Conexión a la base de datos
            SqlCommand command = null; // Comando a ejecutar en la base de datos.
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection(); // Se obtiene una conexión abierta.
                command = new SqlCommand(SQLSearchTablaId, connection); // Se crea el comando con la sentencia Select.
                command.Parameters.Add("@IdProducto", SqlDbType.Int); // Se agrega el parámetro idcliente.
                command.Parameters[0].Value = id;
                da = new SqlDataAdapter(command);
                da.Fill(dt);

                connection.Close(); // Se cierra la conexión.
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                if(command != null) {command.Dispose();}
                if(connection != null) {connection.Dispose();}
            }

            return dt;
        }

        public ProductoEntity ObtenerProductosPorId(int id) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            ProductoEntity producto;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchTablaId, connection);
                command.Parameters.Add("@IdProducto", SqlDbType.Int);
                command.Parameters[0].Value = id;
                dr = command.ExecuteReader();
                producto = new ProductoEntity();

                while (dr.Read()) {
                    producto = CrearEntidad(dr);
                }

                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;
                if (command != null) {command.Dispose();}
                if (connection != null) {connection.Dispose();}
            }

            return producto;
        }

        public List<ProductoEntity> ObtenerProductosPromocionados() {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            List<ProductoEntity> productos = null;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLPromocionado, connection);
                dr = command.ExecuteReader();

                productos = new List<ProductoEntity>();

                while (dr.Read()) {
                    productos.Add(CrearEntidad(dr));
                }

                dr.Close();
                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;
                if (command != null) {command.Dispose();}
                if (connection != null) {connection.Dispose();}
            }

            return productos;
        }

        public List<ProductoEntity> ObtenerProductosPublicitados() {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            List<ProductoEntity> productos = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLPublicitado, connection);
                dr = command.ExecuteReader();

                productos = new List<ProductoEntity>();

                while(dr.Read()) {
                    productos.Add(CrearEntidad(dr));
                }

                dr.Close();
                connection.Close();
            } catch(Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;
                if(command != null) {command.Dispose();}
                if(connection != null) {connection.Dispose();}
            }

            return productos;
        }

        public List<ProductoEntity> ObtenerProductosPorCategoria(int idcategoria) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            List<ProductoEntity> productos = null;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearch, connection);
                command.Parameters.Add("@IdCategoria", SqlDbType.Int);
                command.Parameters[0].Value = idcategoria;
                dr = command.ExecuteReader();

                productos = new List<ProductoEntity>();

                while (dr.Read()) {
                    productos.Add(CrearEntidad(dr));
                }

                dr.Close();
                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;
                if (command != null) {command.Dispose();}
                if (connection != null) {connection.Dispose();}
            }

            return productos;
        }

        public void Insertar(ProductoEntity entidad) {
            EjecutarComando(TipoComando.Insertar, entidad);
        }

        public void Actualizar(ProductoEntity entidad) {
            EjecutarComando(TipoComando.Actualizar, entidad);
        }

        public void Eliminar(int id) {
            ProductoEntity entidad = new ProductoEntity();
            entidad.IdProducto = id;
            EjecutarComando(TipoComando.Eliminar, entidad);
        }

    }
}
