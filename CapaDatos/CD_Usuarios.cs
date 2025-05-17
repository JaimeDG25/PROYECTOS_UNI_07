using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaEntidades;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        //ESTA FUNCION ES PARA LISTA LOS USUARIOS (Y DEMOSTRAR QUE TENEMOS CONEXIN A LA BASE DE DATOS
        public List<Usuarios> Listar()
        {
            List<Usuarios> lista_usuarios = new List<Usuarios>(); //Creando un alista
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) //Ejecutando la conexion 
                {
                    oconexion.Open(); //Abriendo la conexion
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                    //Aqui estamos creando una consulta para acceder tanto a los atributos de usuarios como a los de roles
                    String query = @"SELECT u.Id_Usuario, u.Nombre_Usuario, u.Apellido_Usuario, 
                                            u.Correo_electronico_Usuario, u.Clave, 
                                            r.Id_Rol, r.Roles_Descripcion, u.Activo
                                     FROM usuarios u
                                     INNER JOIN roles r ON u.Rol_Id = r.Id_Rol";
                    
                    //Integrando la consulta y abrinedo la conexion 
                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    //Ejecutando esa consulta
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista_usuarios.Add(
                                new Usuarios() //Leera todos estos parametros para poder guardarlos en una lista dependiendo de lo que pidas
                                {
                                    Id_Usuario = Convert.ToInt32(reader["Id_Usuario"]),
                                    Nombre_Usuario = reader["Nombre_Usuario"].ToString(),
                                    Apellido_Usuario = reader["Apellido_Usuario"].ToString(),
                                    Correo_electronico_Usuario = reader["Correo_electronico_Usuario"].ToString(),
                                    Clave = reader["Clave"].ToString(),
                                    Activo = Convert.ToBoolean(reader["Activo"]),
                                    //aqui estamos trallendo lo que esta dentro de roles por eso hicimos la consulta con inner
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
            catch (Exception ex) //La excepcion a nuetro codigo
            {
                lista_usuarios = new List<Usuarios> //Hubo un error en la consulta o en la agregacion a la lista y saltara esto en su lugar
                {
                    new Usuarios
                    {
                        Id_Usuario = -1,
                        Nombre_Usuario = ex.Message,
                        Apellido_Usuario = "de conexión",
                        Correo_electronico_Usuario  = "vacios",
                        Clave = "N/A",
                    }
                };
                // Mostrar el error de conexión
                Console.WriteLine("Error en ListarUsuarios: " + ex.Message);
            }
            return lista_usuarios; //Recuperemos el huascar
        }

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

        //ESTA FUNCION SERVIRA PARA EDITAR USUARIOS
        public bool Editar(Usuarios obj, out string mensaje_editar)
        {
            bool resultado = false;
            mensaje_editar = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
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
    }
}
