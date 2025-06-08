using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;
namespace CapaNegocio
{
    public class CN_Cursos
    {
        private CD_Cursos objeto_cn_cursos = new CD_Cursos();

        #region FUNCION PARA LISTAR CURSOS EN NEGOCIO
        //ESTA FUNCION ES PARA LISTA LOS USUARIOS (Y DEMOSTRAR QUE TENEMOS CONEXIN A LA BASE DE DATOS
        public List<Cursos> Listar()
        {
            return objeto_cn_cursos.Listar();
        }
        #endregion

        #region FUNCION PARA REGISTRAR CURSOS EN NEGOCIO
        public int Registrar(Cursos obj_curso_register, out string mensaje_registrar)
        {
            mensaje_registrar = string.Empty;

            if (string.IsNullOrEmpty(obj_curso_register.Nombre_Curso) || string.IsNullOrWhiteSpace(obj_curso_register.Nombre_Curso))
            {
                mensaje_registrar = "El nombre del curso no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj_curso_register.Descripcion_Curso) || string.IsNullOrWhiteSpace(obj_curso_register.Descripcion_Curso ))
            {
                mensaje_registrar = "La descripcion del curso no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj_curso_register.Codigo_Curso) || string.IsNullOrWhiteSpace(obj_curso_register.Codigo_Curso))
            {
                mensaje_registrar = "El codigo del cuso no puede ser vacio";
            }
            if (string.IsNullOrEmpty(mensaje_registrar))
            {
                return objeto_cn_cursos.Registrar(obj_curso_register, out mensaje_registrar);
                
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region FUNCION PARA ELIMINAR CURSOS EN NEGOCIO
        #endregion

        #region FUNCION PARA EDITAR CURSOS EN NEGOCIO
        #endregion

    }
}
