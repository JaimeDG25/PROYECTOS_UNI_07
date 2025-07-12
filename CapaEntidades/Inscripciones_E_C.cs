using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Inscripciones_E_C
    {
        public int Id_Inscripcion {  get; set; }
        public Estudiantes Estudiante_Id { get; set; }
        public Asignacion_D_C Asignacion_Id { get; set; }
        public string Fecha_Inscripcion { get; set; }
        public string Mensaje { get; set; }
    }
}
