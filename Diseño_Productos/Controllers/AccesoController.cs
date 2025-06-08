using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security;


using System.Web.Security;
using CapaEntidades;
using CapaNegocio;
namespace Diseño_Productos.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Adpoint()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string correo, string clave)
        {
            Usuarios usuario = new Usuarios();
            usuario = new CN_Usuarios().Listar().Where(u => u.Correo_electronico_Usuario == correo && u.Clave == CN_Recurso.ConvertirHash(clave)).FirstOrDefault();
            if (usuario == null)
            {
                ViewBag.Error = "El correo o contraseña no son correctas";
                return View();
            }
            else
            {
                Session["IdUsuario"] = usuario.Id_Usuario;
                Session["NombreUsuario"] = usuario.Nombre_Usuario;
                Session["ApellidoUsuario"] = usuario.Apellido_Usuario;
                Session["CorreoUsuario"] = usuario.Correo_electronico_Usuario;
                FormsAuthentication.SetAuthCookie(usuario.Correo_electronico_Usuario, false);
                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult CerrarSession()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Acceso");
        }
    }
}