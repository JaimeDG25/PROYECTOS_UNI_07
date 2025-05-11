using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Asignaciones_Cursos
    {
        public int Id_Asignacion {  get; set; }
        public Cursos Curso_Id { get; set; }
        public Usuarios Profesor_Id { get; set; }
        public string Semestre {  get; set; }
    }
}
