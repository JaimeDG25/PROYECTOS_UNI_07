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
    public class CN_Usuarios
    {
        private CD_Usuarios objeto_cn_usuarios = new CD_Usuarios();

        #region FUNCION PARA LISTAR USUARIOS EN NEGOCIO
        //ESTA FUNCION ES PARA LISTA LOS USUARIOS (Y DEMOSTRAR QUE TENEMOS CONEXIN A LA BASE DE DATOS
        public List<Usuarios> Listar()
        {
            return objeto_cn_usuarios.Listar();
        }
        #endregion

        #region FUNCION PARA REGISTRAR USUARIOS EN NEGOCIO
        //ESTA FUNCION SERVIRA PARA AGREGAR USUARIOS (SIENDO EL ADMINISTRADOR)
        public int Registrar(Usuarios obj_user_register, out string mensaje_registrar)
        {
            mensaje_registrar = string.Empty;

            if (string.IsNullOrEmpty(obj_user_register.Nombre_Usuario) || string.IsNullOrWhiteSpace(obj_user_register.Nombre_Usuario))
            {
                mensaje_registrar = "El nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj_user_register.Apellido_Usuario) || string.IsNullOrWhiteSpace(obj_user_register.Apellido_Usuario))
            {
                mensaje_registrar = "El apellido del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj_user_register.Correo_electronico_Usuario) || string.IsNullOrWhiteSpace(obj_user_register.Correo_electronico_Usuario))
            {
                mensaje_registrar = "El correo del usuario no puede ser vacio";
            }
            if (string.IsNullOrEmpty(mensaje_registrar))
            {
                string clave = CN_Recurso.GenerarClave();

                string asunto_c = "ATENTO QUE ESTAS CREANDO UNA CNUEVA CUENTA";
                string mensaje_c = "<h3>Su cuenta ha sido creada y su contraseña es la siguiente !clave! </h3>";
                mensaje_c = mensaje_c.Replace("!clave!", clave);
                bool respuesta = CN_Recurso.EnviarCorreo(obj_user_register.Correo_electronico_Usuario, asunto_c, mensaje_c);

                if (respuesta)
                {
                    obj_user_register.Clave = CN_Recurso.ConvertirHash(clave);
                    obj_user_register.Clave = CN_Recurso.ConvertirHash(clave);
                    return objeto_cn_usuarios.Registrar(obj_user_register, out mensaje_registrar);
                }
                else
                {
                    mensaje_registrar = "No se puede enviar el correo";
                    return 0;
                }
                //obj.Clave = CN_Recursos.ConvertirHash(clave);
                //return objeto.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region FUNCION PARA ELIMINAR USUARIOS EN NEGOCIO
        //ESTA FUNCION SERVIRA PARA ELIMINAR USUARIOS
        public bool Eliminar(int id, out string mensaje_eliminar)
        {
            return objeto_cn_usuarios.Eliminar(id, out mensaje_eliminar);
        }
        #endregion

        #region FUNCION PARA EDITAR USUARIOS EN NEGOCIO
        //ESTA FUNCION SERVIRA PARA EDITAR USUARIOS
        public bool Editar(Usuarios obj_user_edit, out string mensaje_editar)
        {
            mensaje_editar = string.Empty;

            if (string.IsNullOrEmpty(obj_user_edit.Nombre_Usuario) || string.IsNullOrWhiteSpace(obj_user_edit.Nombre_Usuario))
            {
                mensaje_editar = "El nombre del usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj_user_edit.Apellido_Usuario) || string.IsNullOrWhiteSpace(obj_user_edit.Apellido_Usuario))
            {
                mensaje_editar = "El apellido del usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj_user_edit.Correo_electronico_Usuario) || string.IsNullOrWhiteSpace(obj_user_edit.Correo_electronico_Usuario))
            {
                mensaje_editar = "El correo del usuario no puede ser vacío";
            }

            if (string.IsNullOrEmpty(mensaje_editar))
            {
                return objeto_cn_usuarios.Editar(obj_user_edit, out mensaje_editar);
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
