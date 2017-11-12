using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoTallerEntity;
using System.Data.Odbc;
using ProyectoTallerData;
using System.Data;

namespace ProyectoTallerDataODBC {
    public class daProvincia {
        private const string SQLSearchByPrimaryKey = "SELECT * FROM Provincias WHERE IdProvincia = ?";
        private const string SQLSearch = "SELECT * FROM Provincias";
        private const string SQLInsert = "INSERT INTO Provincias (Nombre) VALUES (?)";
        private const string SQLUpdate = "UPDATE Provincias SET Nombre = ? WHERE IdProvincia = ?";
        private const string SQLDelete = "DELETE FROM Provincias WHERE IdProvincia = ?";

        private daConexion connectionDA = new daConexion();

        public daProvincia() {
        }

        private ProvinciaEntity CrearEntidad(OdbcDataReader dr) {
            ProvinciaEntity entidad = new ProvinciaEntity();
            entidad.IdProvincia = Convert.ToInt32(dr["IdProvincia"]);
            entidad.Nombre = dr["Nombre"].ToString();
            return entidad;
        }

        private void CrearParametros(OdbcCommand command, ProvinciaEntity entidad) {
            OdbcParameter parameter = null;

            parameter = command.Parameters.Add("?", OdbcType.VarChar);
            parameter.Value = entidad.Nombre;

            parameter = command.Parameters.Add("?", OdbcType.Int);
            parameter.Value = entidad.IdProvincia;
        }

        private void EjecutarComando(daComun.TipoComandoEnum sqlCommandType, ProvinciaEntity entidad) {
            OdbcConnection connection = null;
            OdbcCommand command = null;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                IDataParameter paramId = new OdbcParameter("?", OdbcType.Int);
                paramId.Value = entidad.IdProvincia;

                switch(sqlCommandType) {
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
            } catch(Exception ex) {
                throw new daException(ex);
            } finally {
                if(command != null) {
                    command.Dispose();
                }

                if(connection != null) {
                    connection.Dispose();
                }
            }
        }

        public ProvinciaEntity ObtenerProvinciaPorId(int idprovincia) {
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            ProvinciaEntity provincia;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLSearchByPrimaryKey, connection);
                command.Parameters.Add("?", OdbcType.Int);
                command.Parameters[0].Value = idprovincia;
                dr = command.ExecuteReader();

                provincia = new ProvinciaEntity();

                while(dr.Read()) {
                    provincia = CrearEntidad(dr);
                }

                dr.Close();
                connection.Close();
            } catch(Exception ex) {
                throw new daException(ex);
            } finally {
                dr = null;

                if(command != null) {
                    command.Dispose();
                }

                if(connection != null) {
                    connection.Dispose();
                }
            }

            return provincia;
        }

        public List<ProvinciaEntity> ObtenerProvincias() {
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            List<ProvinciaEntity> provincias = null;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLSearch, connection);
                dr = command.ExecuteReader();

                provincias = new List<ProvinciaEntity>();

                while(dr.Read()) {
                    provincias.Add(CrearEntidad(dr));
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

            return provincias;
        }

        public void Insertar(ProvinciaEntity entidad) {
            EjecutarComando(daComun.TipoComandoEnum.Insertar, entidad);
        }

        public void Actualizar(ProvinciaEntity entidad) {
            EjecutarComando(daComun.TipoComandoEnum.Actualizar, entidad);
        }

        public void Eliminar(int id) {
            ProvinciaEntity entidad = new ProvinciaEntity();
            entidad.IdProvincia = id;
            EjecutarComando(daComun.TipoComandoEnum.Eliminar, entidad);
        }
    }
}
