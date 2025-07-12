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
    }
}
