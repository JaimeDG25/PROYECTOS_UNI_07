using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;
namespace Diseño_Productos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
        #endregion

        public ActionResult Cursos()
        {
            return View();
        }

        public ActionResult Carreras()
        {
            return View();
        }
    }
}