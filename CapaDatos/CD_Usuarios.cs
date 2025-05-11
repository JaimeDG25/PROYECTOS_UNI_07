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
                    //String query1 = "SELECT Id_Usuario, Nombre_Usuario, Apellido_Usuario, Correo_electronico_Usuario, Clave, Rol_Id FROM usuarios";
                    //Aqui estamos creando una consulta para acceder tanto a los atributos de usuarios como a los de roles
                    String query = @"SELECT u.Id_Usuario, u.Nombre_Usuario, u.Apellido_Usuario, 
                                            u.Correo_electronico_Usuario, u.Clave, 
                                            r.Id_Rol, r.Roles_Descripcion
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
                                    Rol_Id = new Roles //aqui estamos trallendo lo que esta dentro de roles por eso hicimos la consulta con inner
                                    {
                                        Id_Rol = Convert.ToInt32(reader["Id_Rol"]),
                                        Rol_Descripcion = reader["Roles_Descripcion"].ToString()
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
    }
}
