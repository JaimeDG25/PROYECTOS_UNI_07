using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class CN_Carreras
    {
        private CD_Carreras objeto_cn_carreras = new CD_Carreras();

        public List<Carreras> Listar()
        {
            return objeto_cn_carreras.Listar();
        }
    }
}
