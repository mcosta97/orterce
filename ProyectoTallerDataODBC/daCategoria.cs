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
    public class daCategoria {
        private const string SQLSearch = "SELECT * FROM Categorias";
        private const string SQLSearchId = "SELECT * FROM Categorias WHERE IdCategoria = @IdCategoria";
        private const string SQLSearchNombre = "SELECT * FROM Categorias WHERE Nombre = @Nombre";
        private const string SQLInsert = "INSERT INTO Categorias (IdCategoria, Nombre) VALUES ((SELECT MAX(IdCategoria) + 1 FROM Categorias), @Nombre)";
        private const string SQLUpdate = "UPDATE Categorias SET Nombre = @Nombre WHERE IdCategoria = @IdCategoria";
        private const string SQLDelete = "DELETE FROM Categorias WHERE IdCategoria = @IdCategoria";

        private daConexion connectionDA = new daConexion();

        public daCategoria() { }

        public CategoriaEntity CrearEntidad(SqlDataReader dr) {
            CategoriaEntity entidad = new CategoriaEntity();
            entidad.IdCategoria = Convert.ToInt32(dr["idcategoria"]);
            entidad.Nombre = dr["nombre"].ToString();
            return entidad;
        }

        private void CrearParametros(SqlCommand command, CategoriaEntity entidad) {
            SqlParameter parameter = null;

            parameter = command.Parameters.Add("@Nombre", SqlDbType.VarChar);
            parameter.Value = entidad.Nombre;
        }

        private void EjecutarComando(TipoComando sqlCommandType, CategoriaEntity entidad) {
            SqlConnection connection = null;
            SqlCommand command = null;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                IDataParameter paramId = new SqlParameter("@IdCategoria", SqlDbType.Int);
                paramId.Value = entidad.IdCategoria;

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
                if (command != null) {command.Dispose();}
                if (connection != null) {connection.Dispose();}
            }
        }

        public DataTable ObtenerCategoriasTabla() {
            List<CategoriaEntity> categorias = ObtenerCategorias();
            DataTable dt = new DataTable();
            dt.Columns.Add("IdCategoria");
            dt.Columns.Add("Nombre");

            foreach(CategoriaEntity categoria in categorias) {
                dt.Rows.Add(categoria.IdCategoria, categoria.Nombre);
            }

            return dt;
        }

        public CategoriaEntity ObtenerCategoria(int id) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            CategoriaEntity categoria = null;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchId, connection);
                IDataParameter paramId = new SqlParameter("@IdCategoria", SqlDbType.Int);
                paramId.Value = id;
                command.Parameters.Add(paramId);
                dr = command.ExecuteReader();

                categoria = new CategoriaEntity();

                while (dr.Read()) {
                    categoria = CrearEntidad(dr);
                }

                dr.Close();
                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;
                if (command != null) { command.Dispose(); }
                if (connection != null) { connection.Dispose(); }
            }

            return categoria;
        }

        public CategoriaEntity ObtenerCategoriaNombre(string nombre) {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            CategoriaEntity categoria = null;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchNombre, connection);
                CrearParametros(command, categoria);
                dr = command.ExecuteReader();

                categoria = new CategoriaEntity();

                while (dr.Read()) {
                    categoria = CrearEntidad(dr);
                }

                dr.Close();
                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;
                if (command != null) { command.Dispose(); }
                if (connection != null) { connection.Dispose(); }
            }

            return categoria;
        }

        public List<CategoriaEntity> ObtenerCategorias() {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            List<CategoriaEntity> categorias = null;

            try {
                connection = (SqlConnection)connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearch, connection);
                dr = command.ExecuteReader();

                categorias = new List<CategoriaEntity>();

                while (dr.Read()) {
                    categorias.Add(CrearEntidad(dr));
                }

                dr.Close();
                connection.Close();
            } catch (Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;
                if(command != null) {command.Dispose();}
                if (connection != null) {connection.Dispose();}
            }

            return categorias;
        }

        public void Insertar(CategoriaEntity entidad) {
            EjecutarComando(TipoComando.Insertar, entidad);
        }

        public void Actualizar(CategoriaEntity entidad) {
            EjecutarComando(TipoComando.Actualizar, entidad);
        }

        public void Eliminar(int id) {
            CategoriaEntity entidad = new CategoriaEntity();
            entidad.IdCategoria = id;
            EjecutarComando(TipoComando.Eliminar, entidad);
        }
    }
}
