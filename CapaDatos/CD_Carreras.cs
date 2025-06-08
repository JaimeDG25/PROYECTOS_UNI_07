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
        #region LISTANDO LAS CARRERAS
        public List<Carreras> Listar() {
            List<Carreras> lista_carreras = new List<Carreras>();
            try {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) {
                    oconexion.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                    String query = @"SELECT * FROM Carreras";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            lista_carreras.Add(
                                new Carreras() {
                                    Id_Carrera = Convert.ToInt32(reader["Id_Carrera"]),
                                    Nombre_Carrera = reader["Nombre_Carrera"].ToString(),
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex) {
                lista_carreras = new List<Carreras>
                {
                    new Carreras {
                        Id_Carrera = -1,
                        Nombre_Carrera = ex.Message,
                    }
                };
                Console.WriteLine("Error en ListarUsuarios: " + ex.Message);
            }
            return lista_carreras; 
        }
        #endregion
    }
}
