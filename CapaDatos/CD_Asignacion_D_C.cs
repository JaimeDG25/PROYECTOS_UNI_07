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
    public class CD_Asignacion_D_C
    {
        #region METODO PARA LISTAR ASIGNACION DE DOCENTES A CURSOS
        public List<Asignacion_D_C> Listar()
        {
            List<Asignacion_D_C> lista_asignacion_d_c = new List<Asignacion_D_C>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    oconexion.Open();

                    Console.WriteLine("Conexión exitosa a la base de datos.");
                    String query = @"select a.Id_Asignacion, a.Asistente_Id,u.Nombre_Usuario+' '+u.Apellido_Usuario as NombreCompleto, 
                                    a.Curso_Id, c.Nombre_Curso, c.Descripcion_Curso, c.Creditos,c.Codigo_Curso
                                    from asignacion_d_c a
                                    inner join cursos c ON c.Id_Curso=a.Curso_Id
                                    inner join usuarios u ON u.Id_Usuario=a.Asistente_Id ";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista_asignacion_d_c.Add(
                                new Asignacion_D_C()
                                {
                                    Id_Asignacion = Convert.ToInt32(reader["Id_Asignacion"]),
                                    Curso_Id = new Cursos
                                    {
                                        Id_Curso = Convert.ToInt32(reader["Curso_Id"]),
                                        Nombre_Curso = reader["Nombre_Curso"].ToString(),
                                        Descripcion_Curso = reader["Descripcion_Curso"].ToString(),
                                        Creditos = Convert.ToInt32(reader["Creditos"].ToString()),
                                        Codigo_Curso = reader["Codigo_Curso"].ToString(),
                                    },
                                    Asistente_Id = new Usuarios
                                    {
                                        Id_Usuario = Convert.ToInt32(reader["Asistente_Id"]),
                                        Nombre_Usuario = reader["NombreCompleto"].ToString()
                                    },
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista_asignacion_d_c = new List<Asignacion_D_C> {
                    new Asignacion_D_C {
                        Id_Asignacion = -1,
                        Mensaje = ex.Message,
                    }
                };
                Console.WriteLine("Error en ListarCursos: " + ex.Message);
            }
            return lista_asignacion_d_c;
        }
        #endregion

        #region METODO PARA REGISTRAR ASIGNACION DE DOCENTES A CURSOS
        public int Registrar(Asignacion_D_C obj, out string mensaje_registrar)
        {
            int idautogenerado = 0;
            mensaje_registrar = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarAsignacion_d_c", oconexion);
                    cmd.Parameters.AddWithValue("Curso_Id", obj.Curso_Id.Id_Curso);
                    cmd.Parameters.AddWithValue("Asistente_Id", obj.Asistente_Id.Id_Usuario);
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
        #region METODO PARA EDITAR ASIGNACION DE DOCENTES A CURSOS
        #endregion
        #region METODO PARA ELIMINAR ASIGNACION DE DOCENTES A CURSOS
        public bool Eliminar(int id, out string mensaje_eliminar)
        {
            bool resultado = false;
            mensaje_eliminar = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("delete top (1) from cursos where Id_Asignacion = @id", oconexion);
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
    }
}
