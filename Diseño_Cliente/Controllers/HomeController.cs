using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;

namespace Diseño_Cliente.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        #region ESTE METODO ES PARA EL INDEX DEL CLIENTE
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Listar_Estudiantes()
        {
            List<Estudiantes> olista = new List<Estudiantes>();
            olista = new CN_Estudiantes().Listar();
            string saludo = "Un saludo de agradecimiento para el estudiante";
            if (olista == null || !olista.Any())
            {
                return Json("Vaya vaya vaya, algo salio mal", JsonRequestBehavior.AllowGet);
            }
            return Json(new { jeason_lista = olista, saludo = saludo }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Enviar_Correo(Mensaje registrado)
        {
            string dashboard = "Mensaje de correo enviado a: " + registrado.correo
                       + ", asunto: " + registrado.asunto
                       + ", mensaje: " + registrado.mensaje;

            bool enviado = CN_Recurso.EnviarCorreo(registrado.correo, registrado.asunto, registrado.mensaje);
            if (!enviado)
            {
                dashboard = "Error al enviar el correo.";
            }
            return Json(new { resultado = dashboard }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ESTE METODO ES PARA LOS CURSOS DEL CLIENTE
        public ActionResult Cursos()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Listar_Inscripciones(int estudianteId)
        {
            int estudianteISd = 8;
            List<Inscripciones_E_C> olista = new List<Inscripciones_E_C>();
            olista = new CN_Inscripcion_E_C().Listar(estudianteId);
            string saludo = "Un saludo de agradecimiento";
            if (olista == null || !olista.Any())
            {
                return Json("Hola Mundoss", JsonRequestBehavior.AllowGet);
            }
            return Json(new { jeason_lista = olista, saludo = saludo }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ESTE METODO ES PARA VER INFORMACIN SOBRE EL CURSO DEL CLIENTE
        public ActionResult Curso_Clase()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        #endregion

        #region ESTE METODO ES PARA EL CALENCARIO DEL CLIENTE
        public ActionResult Calendario()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #endregion

        #region ESTE METODO ES PARA EL SOLICITAR CURSOS DEL CLIENTE
        public ActionResult Solicitar_Cursos()
        {
            ViewBag.Message = "Your contact page.";
            List<Usuarios> usuario = new CN_Usuarios().Listar();
            List<Estudiantes> estudiantes = new CN_Estudiantes().Listar();
            List<Cursos> lista = new CN_Cursos().Listar();
            List<Asignacion_D_C> asignacion= new CN_Asignacion_D_C().Listar();
            return View(asignacion);
        }
        public ActionResult Listar_Solicitar_Cursos()
        {
            List<Cursos> olista = new List<Cursos>();
            List<Asignacion_D_C> olistas = new List<Asignacion_D_C>();
            olista = new CN_Cursos().Listar();
            olistas = new CN_Asignacion_D_C().Listar();
            string saludo = "Un saludo de agradecimiento para el CLIENTE";
            if (olistas == null || !olistas.Any())
            {
                return Json("Vaya vaya vaya, algo salio mal", JsonRequestBehavior.AllowGet);
            }
            return Json(new { jeason_lista = olistas, saludo = saludo }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ESTE METODO ES PARA CHATS DEL CLIENTE
        public ActionResult Chats()
        {
            return View();
        }
        #endregion
    }
}