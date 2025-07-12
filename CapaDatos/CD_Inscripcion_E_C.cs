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
    public class CD_Inscripcion_E_C
    {
        #region METODO PARA LISTAR ASIGNACION DE DOCENTES A CURSOS
        public List<Inscripciones_E_C> Listar(int estudianteId)
        {
            List<Inscripciones_E_C> lista_Inscripciones_E_C = new List<Inscripciones_E_C>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    oconexion.Open();

                    Console.WriteLine("Conexión exitosa a la base de datos.");
                    String query = @"SELECT 
    i.Id_Inscripcion,
    i.Estudiante_Id,
    e.Nombre_Estudiante,
    e.Apellido_Estudiante,
    a.Curso_Id,
    c.Nombre_Curso,
	c.Descripcion_Curso,
    a.Asistente_Id,
    u.Nombre_Usuario,
    u.Apellido_Usuario,
    i.Asignacion_Id,
    a.Dia_Asignacion,
    a.Horario_Inicio_Asignacion,
    a.Horario_Fin_Asignacion
FROM inscripciones_e_c i
INNER JOIN estudiantes e ON e.Id_Estudiante = i.Estudiante_Id
INNER JOIN asignacion_d_c a ON a.Id_Asignacion = i.Asignacion_Id
INNER JOIN cursos c ON c.Id_Curso = a.Curso_Id
INNER JOIN usuarios u ON u.Id_Usuario = a.Asistente_Id
WHERE i.Estudiante_Id = @EstudianteId;";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@EstudianteId", estudianteId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista_Inscripciones_E_C.Add(
                                new Inscripciones_E_C()
                                {
                                    Id_Inscripcion = Convert.ToInt32(reader["Id_Inscripcion"]),
                                    Estudiante_Id = new Estudiantes
                                    {
                                        Id_Estudiante = Convert.ToInt32(reader["Estudiante_Id"]),
                                        Nombre_Estudiante = reader["Nombre_Estudiante"].ToString(),
                                        Apellido_Estudiante = reader["Apellido_Estudiante"].ToString()
                                    },
                                    Asignacion_Id = new Asignacion_D_C
                                    {
                                        Id_Asignacion = Convert.ToInt32(reader["Asignacion_Id"]),
                                        Asistente_Id = new Usuarios
                                        {
                                            Id_Usuario = Convert.ToInt32(reader["Asistente_Id"]),
                                            Nombre_Usuario = reader["Nombre_Usuario"].ToString(),
                                            Apellido_Usuario = reader["Apellido_Usuario"].ToString(),

                                        },
                                        Curso_Id = new Cursos
                                        {
                                            Id_Curso = Convert.ToInt32(reader["Curso_Id"]),
                                            Nombre_Curso = reader["Nombre_Curso"].ToString(),
                                            Descripcion_Curso = reader["Descripcion_Curso"].ToString(),
                                        },
                                        Dia_Asignacion = reader["Dia_Asignacion"].ToString(),
                                        Horario_Inicio_Asignacion = reader["Horario_Inicio_Asignacion"].ToString(),
                                        Horario_Fin_Asignacion = reader["Horario_Fin_Asignacion"].ToString(),
                                    },
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista_Inscripciones_E_C = new List<Inscripciones_E_C> {
                    new Inscripciones_E_C {
                        Id_Inscripcion = -1,
                        Mensaje = ex.Message,
                    }
                };
                Console.WriteLine("Error en ListarCursos: " + ex.Message);
            }
            return lista_Inscripciones_E_C;
        }
        #endregion
    }
}
