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
    public class CD_Cursos
    {
        #region LISTANDO LOS CURSOS POR CARRERAS
        public List<Cursos> Listar() {
            List<Cursos> lista_cursos = new List<Cursos>();
            try {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) {
                    oconexion.Open();
                    
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                    String query = @"SELECT cu.Id_Curso, cu.Nombre_Curso, cu.Descripcion_Curso, cu.Codigo_Curso,cu.Creditos,
                                            ca.Id_Carrera, ca.Nombre_Carrera, cu.Carrera_Id, cu.Activo_Curso
                                     FROM cursos cu
                                     INNER JOIN carreras ca ON cu.Carrera_Id = ca.Id_Carrera"; 

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            lista_cursos.Add(
                                new Cursos() {
                                    Id_Curso = Convert.ToInt32(reader["Id_Curso"]),
                                    Nombre_Curso = reader["Nombre_Curso"].ToString(),
                                    Descripcion_Curso = reader["Descripcion_Curso"].ToString(),
                                    Codigo_Curso = reader["Codigo_Curso"].ToString(),
                                    Creditos = Convert.ToInt32(reader["Creditos"]),
                                    Activo_Curso = Convert.ToBoolean(reader["Activo_Curso"]),
                                    Carrera_Id = new Carreras {
                                        Id_Carrera = Convert.ToInt32(reader["id_carrera"]),
                                        Nombre_Carrera = reader["nombre_carrera"].ToString()
                                    }
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex) {
                lista_cursos = new List<Cursos> {
                    new Cursos {
                        Id_Curso = -1,
                        Nombre_Curso = ex.Message,
                        Descripcion_Curso = "Error",
                        Codigo_Curso = "de conexión",
                        Creditos  = -1,
                    }
                };
                Console.WriteLine("Error en ListarCursos: " + ex.Message);
            }
            return lista_cursos;
        }
        #endregion

        #region AGREGANDO NUEVOS CURSOS
        //ESTA FUNCION SERVIRA PARA AGREGAR USUARIOS (SIENDO EL ADMINISTRADOR)
        public int Registrar(Cursos obj, out string mensaje_registrar)
        {
            int idautogenerado = 0;
            mensaje_registrar = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCurso", oconexion);
                    cmd.Parameters.AddWithValue("Codigo_Curso", obj.Codigo_Curso);
                    cmd.Parameters.AddWithValue("Nombre_Curso", obj.Nombre_Curso);
                    cmd.Parameters.AddWithValue("Creditos", obj.Creditos);
                    cmd.Parameters.AddWithValue("Carrera_Id", obj.Carrera_Id.Id_Carrera);
                    cmd.Parameters.AddWithValue("Descripcion_Curso", obj.Descripcion_Curso);
                    cmd.Parameters.AddWithValue("Activo_Curso", obj.Activo_Curso);
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
    }
}
