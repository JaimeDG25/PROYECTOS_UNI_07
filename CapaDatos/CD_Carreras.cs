using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;
namespace CapaDatos
{
    public class CD_Carreras
    {
        public List<Carreras> Listar() {
            List<Carreras> lista_carreras = new List<Carreras>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) //Ejecutando la conexion 
                {
                    oconexion.Open(); //Abriendo la conexion
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                    //Aqui estamos creando una consulta para acceder tanto a los atributos de usuarios como a los de roles
                    String query = @"SELECT * FROM Carreras";

                    //Integrando la consulta y abrinedo la conexion 
                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    //Ejecutando esa consulta
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista_carreras.Add(
                                new Carreras() //Leera todos estos parametros para poder guardarlos en una lista dependiendo de lo que pidas
                                {
                                    Id_Carrera = Convert.ToInt32(reader["Id_Carrera"]),
                                    Nombre_Carrera = reader["Nombre_Carrera"].ToString(),
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex) //La excepcion a nuetro codigo
            {
                lista_carreras = new List<Carreras> //Hubo un error en la consulta o en la agregacion a la lista y saltara esto en su lugar
                {
                    new Carreras
                    {
                        Id_Carrera = -1,
                        Nombre_Carrera = ex.Message,
                    }
                };
                // Mostrar el error de conexión
                Console.WriteLine("Error en ListarUsuarios: " + ex.Message);
            }
            return lista_carreras; 
        }
    }
}
