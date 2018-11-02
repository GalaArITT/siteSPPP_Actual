using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace siteSPPP.Controllers
{
    public class TransparenciaController : Controller
    {
        // GET: Transparencia
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ActasComite()
        {
            return View();
        }
        public ActionResult ArmonizacionContable()
        {
            return View();
        }
        public ActionResult InformesTrimestrales()
        {
            return View();
        }
        public ActionResult ProyectosInversion()
        {
            return View();
        }
    }
}