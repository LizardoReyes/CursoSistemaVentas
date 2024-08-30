using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Usuario
    {
        public List<Usuario> Listar() {
            List<Usuario> lista = new List<Usuario>();
            using(SqlConnection oConexion = new SqlConnection(Conexion.cadena)) {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT U.IdUsuario, U.Documento, U.NombreCompleto, U.Correo, U.Clave, U.Estado, R.IdRol, R.Descripcion FROM USUARIO U");
                    query.AppendLine("INNER JOIN ROL R ON U.IdRol = R.IdRol");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion)
                    {
                        CommandType = CommandType.Text
                    };
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario() {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oRol = new Rol()
                                {
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    Descripcion = dr["Descripcion"].ToString()
                                }
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<Usuario>();
                }
            }

            return lista;
        
        }

    }
}
