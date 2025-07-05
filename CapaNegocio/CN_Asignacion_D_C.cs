using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class CN_Asignacion_D_C
    {
        private CD_Asignacion_D_C objeto_cn_asignacion_d_c = new CD_Asignacion_D_C();

        #region METODO PARA LSITAR ASIGNACION DE DOCENTES A CURSOS EN NEGOCIO
        public List<Asignacion_D_C> Listar()
        {
            return objeto_cn_asignacion_d_c.Listar();
        }
        #endregion

        #region METODO PARA REGISTRAR ASIGNACION DE DOCENTES A CURSOS EN NEGOCIO
        public int Registrar(Asignacion_D_C obj_asignar_register, out string mensaje_registrar)
        {
            mensaje_registrar = string.Empty;
            if (string.IsNullOrEmpty(mensaje_registrar))
            {
                return objeto_cn_asignacion_d_c.Registrar(obj_asignar_register, out mensaje_registrar);
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
