using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Cursos
    {
        public int Id_Curso { get; set; }
        public string Codigo_Curso { get; set; }
        public string Nombre_Curso { get; set; } 
        public int Creditos { get; set; }
        public Carreras Carrera_Id { get; set; }
    }
}
