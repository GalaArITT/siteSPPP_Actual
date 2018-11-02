using siteSPPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace siteSPPP.Controllers
{
    public class CapturistaController : Controller
    {
        private sitio_seplaEntities db = new sitio_seplaEntities();
        //metodo para dar seguridad al usuario
        public ActionResult Seguridad_Capturista()
        {
            //Asegurar que a esta vista solo entren aquellos usuarios con rol 1=Capturista 
            int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
            byte? rol = null;
            //linea para validar que no entre a los controladores hasta que detecte una autenticación
            if (idUsuario == 0)
            {
                Response.Redirect("~/Login/Iniciar");
                rol = null;
            }
            //en caso de que si detecte asignar el valor de rol y dar seguridad
            else
            {
                rol = db.USUARIO.Where(s => s.IDUSUARIO == idUsuario).FirstOrDefault().ROL;
                if (rol != 1) // 1 = Capturista
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                {
                    return View();
                }
            }
            return null;
        }
        public ActionResult Bienvenido()
        {
            return Seguridad_Capturista();
        }
    }
}