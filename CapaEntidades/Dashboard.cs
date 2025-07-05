using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Dashboard
    {
        public int TotalDocentes { get; set; }
        public int TotalCursos { get; set; }
        public int TotalAlumnos { get; set; }
        public int Id_Carrera_id { get; set; }
        public string Nombre_Carrera { get; set; }
        public int TotalVeces { get; set; }
    }

}
