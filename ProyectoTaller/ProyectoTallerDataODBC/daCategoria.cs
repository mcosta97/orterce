using ProyectoTallerDataODBC;
using ProyectoTallerEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTallerData {
    public class daCategoria {
        private const string SQLSearch = "SELECT * FROM Categorias";
        private const string SQLInsert = "INSERT INTO Categorias (Nombre) VALUES (?)";
        private const string SQLUpdate = "UPDATE Categorias SET Nombre = ? WHERE IdCategoria = ?";
        private const string SQLDelete = "DELETE FROM Categorias WHERE IdCategoria = ?";

        private daConexion connectionDA = new daConexion();

        public daCategoria() { }

        public CategoriaEntity CrearEntidad(OdbcDataReader dr) {
            CategoriaEntity entidad = new CategoriaEntity();
            entidad.IdCategoria = Convert.ToInt32(dr["idcategoria"]);
            entidad.Nombre = dr["nombre"].ToString();
            return entidad;
        }

        private void CrearParametros(OdbcCommand command, CategoriaEntity entidad) {
            OdbcParameter parameter = null;

            parameter = command.Parameters.Add("?", OdbcType.Int);
            parameter.Value = entidad.IdCategoria;

            parameter = command.Parameters.Add("?", OdbcType.VarChar);
            parameter.Value = entidad.Nombre;
        }

        private void EjecutarComando(daComun.TipoComandoEnum sqlCommandType, CategoriaEntity entidad) {
            OdbcConnection connection = null;
            OdbcCommand command = null;

            try {
                connection = (OdbcConnection)connectionDA.GetOpenedConnection();
                IDataParameter paramId = new OdbcParameter("?", OdbcType.Int);
                paramId.Value = entidad.IdCategoria;

                switch (sqlCommandType) {
                    case daComun.TipoComandoEnum.Insertar:
                        command = new OdbcCommand(SQLInsert, connection);
                        command.Parameters.Add(paramId);
                        CrearParametros(command, entidad);
                        break;

                    case daComun.TipoComandoEnum.Actualizar:
                        command = new OdbcCommand(SQLUpdate, connection);
                        command.Parameters.Add(paramId);
                        CrearParametros(command, entidad);
                        break;

                    case daComun.TipoComandoEnum.Eliminar:
                        command = new OdbcCommand(SQLDelete, connection);
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

        public List<CategoriaEntity> ObtenerCategorias() {
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            List<CategoriaEntity> categorias = null;

            try {
                connection = (OdbcConnection)connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLSearch, connection);
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
            daContadores da = new daContadores();
            entidad.IdCategoria = da.TraerContador(daComun.Contador.Categoria) + 1;
            EjecutarComando(daComun.TipoComandoEnum.Insertar, entidad);
            da.Sumar(daComun.Contador.Categoria);
        }

        public void Actualizar(CategoriaEntity entidad) {
            EjecutarComando(daComun.TipoComandoEnum.Actualizar, entidad);
        }

        public void Eliminar(int id) {
            CategoriaEntity entidad = new CategoriaEntity();
            entidad.IdCategoria = id;
            EjecutarComando(daComun.TipoComandoEnum.Eliminar, entidad);
        }
    }
}
