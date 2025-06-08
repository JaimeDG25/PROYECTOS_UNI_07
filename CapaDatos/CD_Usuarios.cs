using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        #region LISTANDO LOS USUARIOS
        public List<Usuarios> Listar() {

            List<Usuarios> lista_usuarios = new List<Usuarios>(); 
            try {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) 
                {
                    oconexion.Open(); 
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                    String query = @"SELECT u.Id_Usuario, u.Nombre_Usuario, u.Apellido_Usuario, 
                                            u.Correo_electronico_Usuario, u.Clave, 
                                            r.Id_Rol, r.Roles_Descripcion, u.Activo
                                     FROM usuarios u
                                     INNER JOIN roles r ON u.Rol_Id = r.Id_Rol";
 
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            lista_usuarios.Add(
                                new Usuarios() 
                                {
                                    Id_Usuario = Convert.ToInt32(reader["Id_Usuario"]),
                                    Nombre_Usuario = reader["Nombre_Usuario"].ToString(),
                                    Apellido_Usuario = reader["Apellido_Usuario"].ToString(),
                                    Correo_electronico_Usuario = reader["Correo_electronico_Usuario"].ToString(),
                                    Clave = reader["Clave"].ToString(),
                                    Activo = Convert.ToBoolean(reader["Activo"]),
                                    Rol_Id_D = new Roles 
                                    {
                                        Id_Rol = Convert.ToInt32(reader["id_rol"]),
                                        Rol_Descripcion = reader["roles_descripcion"].ToString()
                                    }
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex) {
                lista_usuarios = new List<Usuarios> {
                    new Usuarios {
                        Id_Usuario = -1,
                        Nombre_Usuario = ex.Message,
                        Apellido_Usuario = "de conexión",
                        Correo_electronico_Usuario  = "vacios",
                        Clave = "N/A",
                    }
                };
                Console.WriteLine("Error en ListarUsuarios: " + ex.Message);
            }
            return lista_usuarios; 
        }
        #endregion

        #region FUNCION PARA REGISTRAR USUARIOS
        //ESTA FUNCION SERVIRA PARA AGREGAR USUARIOS (SIENDO EL ADMINISTRADOR)
        public int Registrar(Usuarios obj, out string mensaje_registrar)
        {
            int idautogenerado = 0;
            mensaje_registrar = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("Nombre_Usuario", obj.Nombre_Usuario);
                    cmd.Parameters.AddWithValue("Apellido_Usuario", obj.Apellido_Usuario);
                    cmd.Parameters.AddWithValue("Correo_electronico_Usuario", obj.Correo_electronico_Usuario);
                    cmd.Parameters.AddWithValue("Rol_Id", obj.Rol_Id);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    mensaje_registrar = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                mensaje_registrar = ex.Message;
            }
            return idautogenerado;
        }
        #endregion

        #region FUNCION PARA ELIMINAR USUARIOS
        //ESTA FUNCION SERVIRA PARA ELIMINAR USUARIOS
        public bool Eliminar(int id, out string mensaje_eliminar)
        {
            bool resultado = false;
            mensaje_eliminar = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("delete top (1) from usuarios where Id_Usuario = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje_eliminar = ex.Message;
            }
            return resultado;
        }
        #endregion

        #region FUNCION PARA EDITAR USUARIOS
        //ESTA FUNCION SERVIRA PARA EDITAR USUARIOS
        public bool Editar(Usuarios obj, out string mensaje_editar)
        {
            bool resultado = false;
            mensaje_editar = string.Empty;
            try {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("Id_Usuario", obj.Id_Usuario);
                    cmd.Parameters.AddWithValue("Nombre_Usuario", obj.Nombre_Usuario);
                    cmd.Parameters.AddWithValue("Apellido_Usuario", obj.Apellido_Usuario);
                    cmd.Parameters.AddWithValue("Correo_electronico_Usuario", obj.Correo_electronico_Usuario);
                    cmd.Parameters.AddWithValue("Rol_Id", obj.Rol_Id);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    mensaje_editar = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje_editar = ex.Message;
            }
            return resultado;
        }
        #endregion
    }
}
