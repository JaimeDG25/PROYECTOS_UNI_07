using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public  class Usuarios
    {
        public int Id_Usuario { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Apellido_Usuario { get; set; }
        public string Correo_electronico_Usuario { get; set; }
        public Roles Rol_Id_D { get; set; }
        public int Rol_Id { get; set; }
        public int Rol_Id { get; set; }
        public string Clave {  get; set; }
        public bool Activo { get; set; }
    }
}
