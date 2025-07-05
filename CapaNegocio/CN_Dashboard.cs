using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class CN_Dashboard
    {
        private CD_Dashboard objeto_cn_dashboard = new CD_Dashboard();
        public Dashboard Total_Dashboard_CN()
        {
            return objeto_cn_dashboard.Total_Dashboard_CD();
        }

    }
}
