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
    }
}
