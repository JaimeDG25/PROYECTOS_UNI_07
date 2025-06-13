using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Estudiantes
    {
        public int Id_Estudiante {  get; set; }
        public string Nombre_Estudiante { get; set; }
        public string Apellido_Estudiante { get; set; }
        public string Correo_Electronico_Estudiante { get; set; }
        public int DNI_Estudiante { get; set; }
        public string Contraseña_Estudiante { get; set; }
        public Carreras Carrera_Id { get; set; }
    }
}
