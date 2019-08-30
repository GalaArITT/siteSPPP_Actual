using siteSPPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace siteSPPP.Controllers
{
    public class TransparenciaController : Controller
    {
        private sitio_seplaEntities db = new sitio_seplaEntities();
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
            return View(db.ARCHIVOS.Where(s=>s.IDTIPO==13));
        }
        public ActionResult PortafolioInversiones()
        {
            return View(db.ARCHIVOS.Where(s => s.IDTIPO == 14 || s.IDTIPO == 16));
        }
        public ActionResult MacroindicadoresInversion()
        {
            return View(db.ARCHIVOS.Where(s => s.IDTIPO == 15));
        }
        public ActionResult FormatosGuiasFAIS()
        {
            return View();
        }


    }
}