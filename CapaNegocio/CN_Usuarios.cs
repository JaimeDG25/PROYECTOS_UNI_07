using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;
namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objeto_cn_usuarios = new CD_Usuarios();
        public List<Usuarios> Listar()
        {
            return objeto_cn_usuarios.Listar();
        }
    }
}
