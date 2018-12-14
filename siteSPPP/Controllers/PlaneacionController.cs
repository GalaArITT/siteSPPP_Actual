using siteSPPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace siteSPPP.Controllers
{
    public class PlaneacionController : Controller
    {
        private sitio_seplaEntities db = new sitio_seplaEntities();

        // GET: Planeacion
        public ActionResult Index()
        {
            return View(db.ARCHIVOS.ToList());
        }
        public ActionResult pdm()
        {
            return View();
        }

        public ActionResult SistemaDesempeño()
        {
            return View(db.PLANTILLA.Where(s=>s.IDPLANTILLA==6).ToList());
        }

        public ActionResult Evaluación_2012()
        {
            return View();
        }
        public ActionResult Evaluación_2013()
        {
            return View();
        }

        public ActionResult Evaluación_2014()
        {
            return View();
        }

        public ActionResult ASM_2016()
        {
            return View();
        }

        public ActionResult ASM_2017()
        {
            return View();
        }

        public ActionResult guia_MIR()
        {
            return View();
        }

        public ActionResult CarteraProyectos()
        {
            return View();
        }

    }
}