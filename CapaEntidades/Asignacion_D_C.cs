using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Asignacion_D_C
    {
        public int Id_Asignacion {  get; set; }
        public Cursos Curso_Id { get; set; }
        public Usuarios Asistente_Id { get; set; }
        public string Dia_Asignacion { get; set; }
        public string Horario_Inicio_Asignacion { get; set; }
        public string Horario_Fin_Asignacion { get; set; }
        public  string Mensaje { get; set; }
    }
}
