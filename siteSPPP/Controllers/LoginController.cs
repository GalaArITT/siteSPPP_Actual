using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using siteSPPP.Models;

namespace siteSPPP.Controllers
{
    public class LoginController : Controller
    {
        private sitio_seplaEntities db = new sitio_seplaEntities();

        // GET: Login
        public ActionResult Iniciar(siteSPPP.Models.USUARIO userModel)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(siteSPPP.Models.USUARIO userModel)
        {
            using (sitio_seplaEntities db = new sitio_seplaEntities())
            {
                //instanciar la tabla usuario
                //USUARIO userModel = new USUARIO();
                //validar que no se cierre la sesión con el usuario autenticado
                //consultar el usuario capturista
                var userDetails = db.USUARIO.Where(x => x.USUARIOINICIA == userModel.USUARIOINICIA && x.CONTRASENA == userModel.CONTRASENA)
                    .Where(s => s.ROL == 1 && s.ESTATUS == "A")
                    .FirstOrDefault();
                //consultar usuario administrador
                var userDetailsAdmin = db.USUARIO.Where(x => x.USUARIOINICIA == userModel.USUARIOINICIA && x.CONTRASENA == userModel.CONTRASENA)
                    .Where(s => s.ROL == 2 && s.ESTATUS == "A")
                    .FirstOrDefault();

                if (userDetails == null && userDetailsAdmin == null)
                {
                    ViewBag.Message = "Datos incorrectos, verifique su información";
                    return View("Iniciar", userModel);
                }

                //para usuario capturista
                if (userDetails != null)
                {
                    //Datos del usuario
                    Session["IDUSUARIO"] = userDetails.IDUSUARIO;
                    Session["NOMBREUSUARIO"] = userDetails.NOMBREUSUARIO;
                    Session["ROL"] = userDetails.ROL;
                    //short idUsuario = short.Parse("1");
                    int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
                    FormsAuthentication.SetAuthCookie(idUsuario.ToString(), false);
                    //ViewBag.rol = db.USUARIO.Where(s => s.IDUSUARIO == idUsuario).FirstOrDefault().ROL;
                    return RedirectToAction("Bienvenido", "Capturista");//("Vista","Controlador")
                }
                if (userDetailsAdmin != null)
                {
                    //Datos del usuario
                    Session["IDUSUARIO"] = userDetailsAdmin.IDUSUARIO;
                    Session["NOMBREUSUARIO"] = userDetailsAdmin.NOMBREUSUARIO;
                    Session["ROL"] = userDetailsAdmin.ROL;
                    int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
                    FormsAuthentication.SetAuthCookie(idUsuario.ToString(), false);
                    //ViewBag.rol = db.USUARIO.Where(s => s.IDUSUARIO == idUsuario).FirstOrDefault().ROL;
                    return RedirectToAction("Bienvenido", "Administrador");//("Vista","Controlador")
                }
                return null;
            }//using
        }
        public ActionResult VerDatos()
        {
            return View(db.USUARIO.ToList().Where(x => x.ROL == 1));
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Iniciar", "Login");
    
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
