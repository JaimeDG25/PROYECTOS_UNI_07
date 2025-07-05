using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;

namespace Diseño_Cliente.Controllers
{
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
        
        #endregion

        #region ESTE METODO ES PARA LOS CURSOS DEL CLIENTE
        public ActionResult Cursos()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #endregion

        #region
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
            List<Cursos> lista = new CN_Cursos().Listar();
            return View(lista);
        }
        public ActionResult Listar_Solicitar_Cursos()
        {
            List<Cursos> olista = new List<Cursos>();
            olista = new CN_Cursos().Listar();
            string saludo = "Un saludo de agradecimiento para el CLIENTE";
            if (olista == null || !olista.Any())
            {
                return Json("Vaya vaya vaya, algo salio mal", JsonRequestBehavior.AllowGet);
            }
            return Json(new { jeason_lista = olista, saludo = saludo }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ESTE METODO ES PARA CHATS DEL CLIENTE
        #endregion
    }
}