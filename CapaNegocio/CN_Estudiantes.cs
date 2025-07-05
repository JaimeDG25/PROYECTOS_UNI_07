using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaDatos;
namespace CapaNegocio
{
    public class CN_Estudiantes
    {
        private CD_Estudiantes objeto_cn_estudiantes = new CD_Estudiantes();

        #region METODO PARA LISTAR ESTUDIANTES EN NEGOCIO
        public List<Estudiantes> Listar()
        {
            return objeto_cn_estudiantes.Listar();
        }
        #endregion

        #region METODO PARA REGISTRAR ESTUDIANTES EN NEGOCIO
        public int Registrar(Estudiantes obj_estudiante_register, out string mensaje_registrar)
        {
            mensaje_registrar = string.Empty;

            if (string.IsNullOrEmpty(obj_estudiante_register.Nombre_Estudiante) || string.IsNullOrWhiteSpace(obj_estudiante_register.Nombre_Estudiante))
            {
                mensaje_registrar = "El nombre del Estudiante no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj_estudiante_register.Apellido_Estudiante) || string.IsNullOrWhiteSpace(obj_estudiante_register.Apellido_Estudiante))
            {
                mensaje_registrar = "La descripcion del Estudiante no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj_estudiante_register.Correo_Electronico_Estudiante) || string.IsNullOrWhiteSpace(obj_estudiante_register.Correo_Electronico_Estudiante))
            {
                mensaje_registrar = "El correo del Estudiante no puede ser vacio";
            }
            if (string.IsNullOrEmpty(mensaje_registrar))
            {
                string clave = CN_Recurso.GenerarClave();

                string asunto_c = "Felicidades has sido inscrito a la Universidad Sideral Carrion con exito";
                string mensaje_c = "<h3>Su inscripcion ha sido generada y su clave que para ingresar al portal es la siguiente !clave! </h3>";
                mensaje_c = mensaje_c.Replace("!clave!", clave);
                bool respuesta = CN_Recurso.EnviarCorreo(obj_estudiante_register.Correo_Electronico_Estudiante, asunto_c, mensaje_c);

                if (respuesta)
                {
                    obj_estudiante_register.Contraseña_Estudiante = CN_Recurso.ConvertirHash(clave);
                    obj_estudiante_register.Contraseña_Estudiante = CN_Recurso.ConvertirHash(clave);
                    return objeto_cn_estudiantes.Registrar(obj_estudiante_register, out mensaje_registrar);
                }
                else
                {
                    mensaje_registrar = "No se puede enviar el correo";
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region METODO PARA ELIMINAR ESTUDIANTES EN NEGOCIO
        //ESTA FUNCION SERVIRA PARA ELIMINAR EstudianteS
        public bool Eliminar(int id, out string mensaje_eliminar)
        {
            return objeto_cn_estudiantes.Eliminar(id, out mensaje_eliminar);
        }
        #endregion

        #region METODO PARA EDITAR ESTUDIANTES EN NEGOCIO
        public bool Editar(Estudiantes obj_estudiante_register, out string mensaje_editar)
        {
            mensaje_editar = string.Empty;

            if (string.IsNullOrEmpty(obj_estudiante_register.Nombre_Estudiante) || string.IsNullOrWhiteSpace(obj_estudiante_register.Nombre_Estudiante))
            {
                mensaje_editar = "El nombre del Estudiante no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj_estudiante_register.Apellido_Estudiante) || string.IsNullOrWhiteSpace(obj_estudiante_register.Apellido_Estudiante))
            {
                mensaje_editar = "El apellido del Estudiante no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj_estudiante_register.Correo_Electronico_Estudiante) || string.IsNullOrWhiteSpace(obj_estudiante_register.Correo_Electronico_Estudiante))
            {
                mensaje_editar = "El correo del Estudiante no puede ser vacío";
            }

            if (string.IsNullOrEmpty(mensaje_editar))
            {
                return objeto_cn_estudiantes.Editar(obj_estudiante_register, out mensaje_editar);
            }
            else
            {
                return false;
            }
            #endregion
        }
    }
}
