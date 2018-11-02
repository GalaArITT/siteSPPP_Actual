using siteSPPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace siteSPPP.Controllers
{
    public class AdministradorController : Controller
    {
        private sitio_seplaEntities db = new sitio_seplaEntities();
        // GET: Administrador
        public ActionResult Bienvenido()
        {
            //Asegurar que a esta vista solo entren aquellos usuarios con rol 2=Administrador 
            int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
            var rol = db.USUARIO.Where(s => s.IDUSUARIO == idUsuario).FirstOrDefault().ROL;
            if (rol != 2)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View();
        }
    }
}