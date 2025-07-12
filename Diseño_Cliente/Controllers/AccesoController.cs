using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using CapaNegocio;
using System.Web.Security;

namespace Diseño_Cliente.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        #region ESTE METODO ES PARA EL LOGIN DEL CLIENTE
        public ActionResult Login_Cliente()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login_Cliente(string correo, string clave)
        {
            Estudiantes estudiante = new Estudiantes();
            estudiante = new CN_Estudiantes().Listar().Where(u => u.Correo_Electronico_Estudiante == correo && u.Contraseña_Estudiante == CN_Recurso.ConvertirHash(clave)).FirstOrDefault();
            if (estudiante == null)
            {
                ViewBag.Error = "El correo o contraseña no son correctas";
                return View();
            }
            else
            {
                Session["IdEstudiante"] = estudiante.Id_Estudiante;
                Session["NombreEstudiante"] = estudiante.Nombre_Estudiante;
                Session["ApellidoEstudiante"] = estudiante.Apellido_Estudiante;
                Session["CorreoEstudiante"] = estudiante.Correo_Electronico_Estudiante;
                FormsAuthentication.SetAuthCookie(estudiante.Correo_Electronico_Estudiante, false);
                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult CerrarSession()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login_Cliente", "Acceso");
        }
        #endregion 

        #region ESTE METODO ES PARA 
        #endregion
        #region
        #endregion
        #region
        #endregion
        #region
        #endregion
    }
}