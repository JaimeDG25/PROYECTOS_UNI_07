using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class CN_Inscripcion_E_C
    {
        private CD_Inscripcion_E_C objeto_cn_inscripcion_e_c = new CD_Inscripcion_E_C();
        #region
        public List<Inscripciones_E_C> Listar(int estudianteId)
        {
            return objeto_cn_inscripcion_e_c.Listar(estudianteId);
        }
        #endregion
        #region METODO PARA REGISTRAR ASIGNACION DE DOCENTES A CURSOS EN NEGOCIO
        public int Registrar(Inscripciones_E_C obj_inscribir_register, out string mensaje_registrar)
        {
            mensaje_registrar = string.Empty;
            if (string.IsNullOrEmpty(mensaje_registrar))
            {
                return objeto_cn_inscripcion_e_c.Registrar(obj_inscribir_register, out mensaje_registrar);
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
