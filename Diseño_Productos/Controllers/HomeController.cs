using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;
namespace Diseño_Productos.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string id = Session["IdUsuario"]?.ToString();
            string nombre = Session["NombreUsuario"]?.ToString();
            string apellido = Session["ApellidoUsuario"]?.ToString();
            string correo = Session["CorreoUsuario"]?.ToString();
            ViewBag.Id = id;
            ViewBag.Nombre = nombre;
            ViewBag.Apellido = apellido;
            ViewBag.Correo = correo;
            return View();
        }

        #region Aca tenemos todo lo que se debe hacer para la tabla Usuarios
        public ActionResult Usuarios()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuarios> olista = new List<Usuarios>();
            olista = new CN_Usuarios().Listar();
            string saludo = "Un saludo de agradecimiento";
            if (olista == null || !olista.Any())
            {
                return Json("Hola Mundoss", JsonRequestBehavior.AllowGet);
            }
            return Json(new { jeason_lista=olista,saludo=saludo }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Guardar(Usuarios registrado) //Ese "registrado" debe ser igual
        {
            object resultado;
            string mensaje = string.Empty;
            if (registrado.Id_Usuario == 0)
            {
                resultado = new CN_Usuarios().Registrar(registrado, out mensaje);
            }
            else
            {
                resultado = new CN_Usuarios().Editar(registrado, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar_usuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Usuarios().Eliminar(id, out mensaje);
            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Aca tenemos todo lo que se debe hacer para la tabla Cursos
        public ActionResult Cursos()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Listar_Cursos()
        {
            List<Cursos> olista = new List<Cursos>();
            olista = new CN_Cursos().Listar();
            string saludo = "Un saludo de agradecimiento";
            if (olista == null || !olista.Any())
            {
                return Json("Hola Mundoss", JsonRequestBehavior.AllowGet);
            }
            return Json(new { jeason_lista = olista, saludo = saludo }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Guardar_Curso(Cursos registrado) //Ese "registrado" debe ser igual
        {
            object resultado;
            string mensaje = string.Empty;
            if (registrado.Id_Curso == 0)
            {
                resultado = new CN_Cursos().Registrar(registrado, out mensaje);
            }
            else
            {
                resultado = null;
                //resultado = new CN_Cursos().Editar(registrado, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Aca tenemos todo lo que se debe hacer para la tabla Usuarios
        public ActionResult Carreras()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Listar_Carreras()
        {
            List<Carreras> olistacarreras = new List<Carreras>();
            olistacarreras = new CN_Carreras().Listar();
            string saludo = "Un saludo de agradecimiento";
            if (olistacarreras == null || !olistacarreras.Any())
            {
                return Json("Hola Mundoss", JsonRequestBehavior.AllowGet);
            }
            return Json(new { jeason_lista = olistacarreras, saludo = saludo }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}