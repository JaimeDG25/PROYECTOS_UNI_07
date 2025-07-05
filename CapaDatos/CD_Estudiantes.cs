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
    public class CD_Estudiantes
    {
        #region METODO PARA LISTAR ESTUDIANTES
        public List<Estudiantes> Listar()
        {
            List<Estudiantes> lista_estudiantes = new List<Estudiantes>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    oconexion.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                    String query = @"SELECT u.Id_Estudiante, u.Nombre_Estudiante, u.Apellido_Estudiante, 
                                            u.Correo_Electronico_Estudiante, u.Contraseña_Estudiante, u.DNI_Estudiante,
                                            c.Id_Carrera, c.Nombre_Carrera
                                     FROM estudiantes u
                                     INNER JOIN carreras c ON u.Carrera_Id = c.Id_Carrera";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista_estudiantes.Add(
                                new Estudiantes()
                                {
                                    Id_Estudiante = Convert.ToInt32(reader["Id_Estudiante"]),
                                    Nombre_Estudiante = reader["Nombre_Estudiante"].ToString(),
                                    Apellido_Estudiante = reader["Apellido_Estudiante"].ToString(),
                                    Correo_Electronico_Estudiante = reader["Correo_Electronico_Estudiante"].ToString(),
                                    Contraseña_Estudiante = reader["Contraseña_Estudiante"].ToString(),
                                    DNI_Estudiante = Convert.ToInt32(reader["DNI_Estudiante"]),
                                    Carrera_Id = new Carreras
                                    {
                                        Id_Carrera = Convert.ToInt32(reader["Id_Carrera"]),
                                        Nombre_Carrera = reader["Nombre_Carrera"].ToString()
                                    }
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista_estudiantes = new List<Estudiantes> {
                    new Estudiantes {
                        Id_Estudiante = -1,
                        Nombre_Estudiante = ex.Message,
                        Apellido_Estudiante = "de conexión",
                        Correo_Electronico_Estudiante  = "vacios",
                        Contraseña_Estudiante = "N/A",
                    }
                };
                Console.WriteLine("Error en ListarEstudiantes: " + ex.Message);
            }
            return lista_estudiantes;
        }
        #endregion

        #region METODO PARA REGISTRAR ESTUDIANTES
        //ESTA FUNCION SERVIRA PARA REGISTRAR ESTUDIANTES (SIENDO EL ADMINISTRADOR)
        public int Registrar(Estudiantes obj, out string mensaje_registrar)
        {
            int idautogenerado = 0;
            mensaje_registrar = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarEstudiante", oconexion);
                    cmd.Parameters.AddWithValue("Nombre_Estudiante", obj.Nombre_Estudiante);
                    cmd.Parameters.AddWithValue("Apellido_Estudiante", obj.Apellido_Estudiante);
                    cmd.Parameters.AddWithValue("Correo_Electronico_Estudiante", obj.Correo_Electronico_Estudiante);
                    cmd.Parameters.AddWithValue("Carrera_Id", obj.Carrera_Id.Id_Carrera);
                    cmd.Parameters.AddWithValue("DNI_Estudiante", obj.DNI_Estudiante);
                    cmd.Parameters.AddWithValue("Contraseña_Estudiante", obj.Contraseña_Estudiante);
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

        #region METODO PARA ELIMINAR ESTUDIANTES
        //ESTA FUNCION SERVIRA PARA ELIMINAR ESTUDIANTES
        public bool Eliminar(int id, out string mensaje_eliminar)
        {
            bool resultado = false;
            mensaje_eliminar = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("delete top (1) from estudiantes where Id_Estudiante = @id", oconexion);
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

        #region METODO PARA EDITAR ESTUDIANTES
        public bool Editar(Estudiantes obj, out string mensaje_editar)
        {
            bool resultado = false;
            mensaje_editar = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarEstudiante", oconexion);
                    cmd.Parameters.AddWithValue("Id_Estudiante", obj.Id_Estudiante);
                    cmd.Parameters.AddWithValue("Nombre_Estudiante", obj.Nombre_Estudiante);
                    cmd.Parameters.AddWithValue("Apellido_Estudiante", obj.Apellido_Estudiante);
                    cmd.Parameters.AddWithValue("Correo_Electronico_Estudiante", obj.Correo_Electronico_Estudiante);
                    cmd.Parameters.AddWithValue("Carrera_Id", obj.Carrera_Id.Id_Carrera);
                    cmd.Parameters.AddWithValue("DNI_Estudiante", obj.DNI_Estudiante);
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
