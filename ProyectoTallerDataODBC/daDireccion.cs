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
    public class daDireccion {
        private const string SQLSearchByPrimaryKey = "SELECT * FROM Direcciones WHERE IdDireccion = @IdDireccion";
        private const string SQLSearchAll = "SELECT * FROM Direcciones WHERE IdCliente LIKE @IdCliente";
        private const string SQLInsert = "INSERT INTO Direcciones (IdDireccion, IdCliente, Direccion, IdProvincia, IdLocalidad, Altura, Piso, Dpto) VALUES ((SELECT MAX(IdDireccion) + 1 FROM Direcciones), @IdCliente, @Direccion, @IdProvincia, @IdLocalidad, @Altura, @Piso, @Dpto)";
        private const string SQLUpdate = "UPDATE Direcciones SET IdCliente = @IdCliente, Direccion = @Direccion, IdProvincia = @IdProvincia, IdLocalidad = @IdLocalidad, Altura = @Altura, Piso = @Piso, Dpto = @Dpto WHERE IdDireccion = @IdDireccion";
        private const string SQLDelete = "DELETE FROM Direcciones WHERE IdDireccion = ?";

        private daConexion connectionDA = new daConexion();

        public daDireccion() {}

        private DireccionEntity CrearEntidad(SqlDataReader dr) {
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

        private void CrearParametros(SqlCommand command, DireccionEntity entidad) {
            SqlParameter parameter = null;

            parameter = command.Parameters.Add("@IdCliente", SqlDbType.Int);
            parameter.Value = entidad.Cliente.IdCliente;

            parameter = command.Parameters.Add("@IdLocalidad", SqlDbType.VarChar);
            parameter.Value = entidad.Localidad.IdLocalidad;

            parameter = command.Parameters.Add("@IdProvincia", SqlDbType.Int);
            parameter.Value = entidad.Provincia.IdProvincia;

            parameter = command.Parameters.Add("@Altura", SqlDbType.Int);
            parameter.Value = entidad.Altura;

            parameter = command.Parameters.Add("@Piso", SqlDbType.VarChar);
            parameter.Value = entidad.Piso;

            parameter = command.Parameters.Add("@Dpto", SqlDbType.VarChar);
            parameter.Value = entidad.Dpto;
        }

        private void EjecutarComando(TipoComando sqlCommandType, DireccionEntity entidad) {
            SqlConnection connection = null;
            SqlCommand command = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                IDataParameter paramId = new SqlParameter("@IdDireccion", SqlDbType.Int);
                paramId.Value = entidad.IdDireccion;

                switch(sqlCommandType) {
                    case TipoComando.Insertar:
                        command = new SqlCommand(SQLInsert, connection);
                        CrearParametros(command, entidad);
                        break;

                    case TipoComando.Actualizar:
                        command = new SqlCommand(SQLUpdate, connection);
                        CrearParametros(command, entidad);
                        command.Parameters.Add(paramId);
                        break;

                    case TipoComando.Eliminar:
                        command = new SqlCommand(SQLDelete, connection);
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
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dr = null;
            List<DireccionEntity> direcciones = null;

            try {
                connection = (SqlConnection) connectionDA.GetOpenedConnection();
                command = new SqlCommand(SQLSearchAll, connection);
                command.Parameters.Add("@IdCliente", SqlDbType.VarChar);
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
            EjecutarComando(TipoComando.Insertar, entidad);
        }

        public void Actualizar(DireccionEntity entidad) {
            EjecutarComando(TipoComando.Actualizar, entidad);
        }

        public void Eliminar(int id) {
            DireccionEntity entidad = new DireccionEntity();
            entidad.IdDireccion = id;
            EjecutarComando(TipoComando.Eliminar, entidad);
        }
    }
}
