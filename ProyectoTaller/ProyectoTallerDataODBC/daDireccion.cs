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
    public class daDireccion {
        private const string SQLSearchByPrimaryKey = "SELECT * FROM Direcciones WHERE IdDireccion = ?";
        private const string SQLSearchAll = "SELECT * FROM Direcciones WHERE IdCliente LIKE ?";
        private const string SQLInsert = "INSERT INTO Direcciones (IdCliente, Direccion, IdProvincia, IdLocalidad, Altura, Piso, Dpto) VALUES (?,?,?,?,?,?,?)";
        private const string SQLUpdate = "UPDATE Direcciones SET IdCliente = ?, Direccion = ?, IdProvincia = ?, IdLocalidad = ?, Altura = ?, Piso = ?, Dpto = ? WHERE IdDireccion = ?";
        private const string SQLDelete = "DELETE FROM Direcciones WHERE IdDireccion = ?";

        private daConexion connectionDA = new daConexion();

        public daDireccion() {}

        private DireccionEntity CrearEntidad(OdbcDataReader dr) {
            DireccionEntity entidad = new DireccionEntity();
            entidad.IdDireccion = Convert.ToInt32(dr["IdDireccion"]);
            entidad.Cliente = new ClienteEntity();
            entidad.Direccion = dr["Direccion"].ToString();
            entidad.Localidad = new daLocalidad().ObtenerLocalidadPorId(Convert.ToInt32(dr["IdLocalidad"]));
            entidad.Provincia = entidad.Localidad.Provincia;
            entidad.Altura = Convert.ToInt32(dr["Altura"]);
            entidad.Piso = dr["Piso"].ToString();
            entidad.Dpto = dr["Dpto"].ToString();
            return entidad;
        }

        private void CrearParametros(OdbcCommand command, DireccionEntity entidad) {
            OdbcParameter parameter = null;

            parameter = command.Parameters.Add("?", OdbcType.Int);
            parameter.Value = entidad.Cliente.IdCliente;

            parameter = command.Parameters.Add("?", OdbcType.VarChar);
            parameter.Value = entidad.Localidad.IdLocalidad;

            parameter = command.Parameters.Add("?", OdbcType.Int);
            parameter.Value = entidad.Provincia.IdProvincia;

            parameter = command.Parameters.Add("?", OdbcType.Int);
            parameter.Value = entidad.Altura;

            parameter = command.Parameters.Add("?", OdbcType.VarChar);
            parameter.Value = entidad.Piso;

            parameter = command.Parameters.Add("?", OdbcType.VarChar);
            parameter.Value = entidad.Dpto;
        }

        private void EjecutarComando(daComun.TipoComandoEnum sqlCommandType, DireccionEntity entidad) {
            OdbcConnection connection = null;
            OdbcCommand command = null;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                IDataParameter paramId = new OdbcParameter("?", OdbcType.Int);
                paramId.Value = entidad.IdDireccion;

                switch(sqlCommandType) {
                    case daComun.TipoComandoEnum.Insertar:
                        command = new OdbcCommand(SQLInsert, connection);
                        CrearParametros(command, entidad);
                        break;

                    case daComun.TipoComandoEnum.Actualizar:
                        command = new OdbcCommand(SQLUpdate, connection);
                        CrearParametros(command, entidad);
                        command.Parameters.Add(paramId);
                        break;

                    case daComun.TipoComandoEnum.Eliminar:
                        command = new OdbcCommand(SQLDelete, connection);
                        command.Parameters.Add(paramId);
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

        public DataTable DireccionesTabla(List<DireccionEntity> direcciones) {
            DataTable dt = new DataTable();
            dt.Columns.Add("IdDireccion");
            dt.Columns.Add("IdCliente");
            dt.Columns.Add("Direccion");
            dt.Columns.Add("Altura");
            dt.Columns.Add("Piso");
            dt.Columns.Add("Dpto");
            dt.Columns.Add("Localidad");
            dt.Columns.Add("Provincia");

            foreach(DireccionEntity direccion in direcciones) {
                dt.Rows.Add(direccion.IdDireccion, direccion.Cliente.IdCliente, direccion.Direccion, 
                            direccion.Altura, direccion.Piso, direccion.Dpto,
                            direccion.Localidad.Nombre, direccion.Localidad.Provincia.Nombre);
            }

            return dt;
        }

        public List<DireccionEntity> ObtenerDirecciones(int idcliente) {
            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcDataReader dr = null;
            List<DireccionEntity> direcciones = null;

            try {
                connection = (OdbcConnection) connectionDA.GetOpenedConnection();
                command = new OdbcCommand(SQLSearchAll, connection);
                command.Parameters.Add("?", OdbcType.VarChar);
                command.Parameters[0].Value = idcliente;

                dr = command.ExecuteReader();

                direcciones = new List<DireccionEntity>();

                while(dr.Read())
                {
                    direcciones.Add(CrearEntidad(dr));
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

            return direcciones;
        }

        public void Insertar(DireccionEntity entidad) {
            EjecutarComando(daComun.TipoComandoEnum.Insertar, entidad);
        }

        public void Actualizar(DireccionEntity entidad) {
            EjecutarComando(daComun.TipoComandoEnum.Actualizar, entidad);
        }

        public void Eliminar(int id) {
            DireccionEntity entidad = new DireccionEntity();
            entidad.IdDireccion = id;
            EjecutarComando(daComun.TipoComandoEnum.Eliminar, entidad);
        }
    }
}
