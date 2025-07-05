using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using CapaEntidades;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Dashboard
    {
        //METODO PARA VER DATOS EN EL DASHBOARD
        public Dashboard Total_Dashboard_CD()
        {
            Dashboard reporte_dashboard = new Dashboard();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    oconexion.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                    SqlCommand cmd = new SqlCommand("sp_Total_Dashboard", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reporte_dashboard = new Dashboard()
                            {
                                TotalDocentes = Convert.ToInt32(reader["TotalUsuarios"]),
                                TotalCursos = Convert.ToInt32(reader["TotalCursos"]),
                                TotalAlumnos = Convert.ToInt32(reader["TotalEstudiantes"]),
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                reporte_dashboard = new Dashboard();
                // Mostrar el error de conexión
                Console.WriteLine("Error en ListarUsuarios: " + ex.Message);
            }
            return reporte_dashboard;
        }
        public List<Cursos> Listar_Mejores()
        {

            List<Cursos> listar_mejores = new List<Cursos>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    oconexion.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                    String query = @"SELECT TOP 1 
                                        cu.Carrera_Id, 
                                        ca.Nombre_Carrera, 
                                        COUNT(*) AS TotalCursos
                                    FROM cursos cu
                                    INNER JOIN carreras ca ON cu.Carrera_Id = ca.Id_Carrera
                                    GROUP BY cu.Carrera_Id, ca.Nombre_Carrera
                                    ORDER BY TotalCursos DESC";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listar_mejores.Add(
                                new Cursos()
                                {
                                    Carrera_Id = new Carreras
                                    {
                                        Id_Carrera = Convert.ToInt32(reader["id_carrera"]),
                                        Nombre_Carrera = reader["nombre_carrera"].ToString()
                                    },
                                    TotalVeces= Convert.ToInt32(reader["TotalCursos"]),
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listar_mejores = new List<Cursos> {
                    new Cursos {
                        Id_Curso = -1,
                        Nombre_Curso = ex.Message,
                    }
                };
                Console.WriteLine("Error en ListarMejores: " + ex.Message);
            }
            return listar_mejores;
        }
    }
}
