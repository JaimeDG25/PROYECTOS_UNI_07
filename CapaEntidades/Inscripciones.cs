using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Inscripciones
    {
        public int Id_Inscripcion {  get; set; }
        public Estudiantes Estudiante_Id { get; set; }
        public Cursos Curso_id { get; set; }
        public string Fecha_Inscripcion { get; set; }
    }
}
