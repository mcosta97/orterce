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
    public class daLocalidad {
        private const string SQLSearchByPrimaryKey = "SELECT * FROM Localidades WHERE IdLocalidad = ?";
        private const string SQLSearch = "SELECT * FROM Localidades WHERE Nombre=";
        private const string SQLSearchProv = "SELECT * FROM Localidades L INNER JOIN Provincias P ON P.IdProvincia = L.IdProvincia WHERE p.nombre=?";
        private const string SQLInsert = "INSERT INTO Localidades (IdProvincia, Nombre) VALUES (?,?)";
        private const string SQLUpdate = "UPDATE Localidades SET IdProvincia = ?, Nombre = ? WHERE idLocalidad = ?";
        private const string SQLDelete = "DELETE FROM Localidades WHERE IdLocalidad = ?";

        private daConexion connectionDA = new daConexion();

        public daLocalidad() {
        }

        private LocalidadEntity CrearEntidad(OdbcDataReader dr) {
            LocalidadEntity entidad = new LocalidadEntity();
            entidad.IdLocalidad = Convert.ToInt32(dr["IdLocalidad"]);
            entidad.Nombre = dr["Nombre"].ToString();
            entidad.Provincia = new daProvincia().ObtenerProvinciaPorId(Convert.ToInt32(dr["IdProvincia"]));
            return entidad;
        }

        private void CrearParametros(OdbcCommand command, LocalidadEntity entidad) {
            OdbcParameter parameter = null;

            parameter = command.Parameters.Add("?", OdbcType.Int);
            parameter.Value = entidad.Provincia.IdProvincia;

            parameter = command.Parameters.Add("?", OdbcType.VarChar);
            parameter.Value = entidad.Nombre;

            parameter = command.Parameters.Add("?", OdbcType.Int);
            parameter.Value = entidad.IdLocalidad;
        }

        private void EjecutarComando(daComun.TipoComandoEnum sqlCommandType, LocalidadEntity entidad) {
            OdbcConnection connection = null;
            OdbcCommand command = null;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                IDataParameter paramId = new OdbcParameter("?", OdbcType.Int);
                paramId.Value = entidad.IdLocalidad;

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
                if(command != null) {command.Dispose();}
                if(connection != null) {connection.Dispose();}
            }
        }

        public LocalidadEntity ObtenerLocalidadPorId(int idlocalidad) {
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            LocalidadEntity localidad;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLSearchByPrimaryKey, connection);
                command.Parameters.Add("?", OdbcType.Int);
                command.Parameters[0].Value = idlocalidad;
                dr = command.ExecuteReader();

                localidad = new LocalidadEntity();

                while(dr.Read()) {
                    localidad = CrearEntidad(dr);
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

            return localidad;
        }

        public LocalidadEntity ObtenerLocalidadPorNombre(string loca) {
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            LocalidadEntity localidad;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLSearch, connection);
                command.Parameters.Add("?", OdbcType.VarChar);
                command.Parameters[0].Value = loca;
                dr = command.ExecuteReader();

                localidad = new LocalidadEntity();

                while(dr.Read()) {
                    localidad = CrearEntidad(dr);
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

            return localidad;
        }

        public List<LocalidadEntity> ObtenerLocalidades(string provincia) {
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            List<LocalidadEntity> localidades = null;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLSearchProv, connection);
                command.Parameters.Add("?", OdbcType.VarChar);
                command.Parameters[0].Value = provincia;
                dr = command.ExecuteReader();

                localidades = new List<LocalidadEntity>();

                while(dr.Read()) {
                    localidades.Add(CrearEntidad(dr));
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

            return localidades;
        }

        public void Insertar(LocalidadEntity entidad) {
            EjecutarComando(daComun.TipoComandoEnum.Insertar, entidad);
        }

        public void Actualizar(LocalidadEntity entidad) {
            EjecutarComando(daComun.TipoComandoEnum.Actualizar, entidad);
        }

        public void Eliminar(int id) {
            LocalidadEntity entidad = new LocalidadEntity();
            entidad.IdLocalidad = id;
            EjecutarComando(daComun.TipoComandoEnum.Eliminar, entidad);
        }
    }
}