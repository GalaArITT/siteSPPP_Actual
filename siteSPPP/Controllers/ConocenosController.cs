using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using siteSPPP.DatosXSDS;
using siteSPPP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace siteSPPP.Controllers
{
    public class ConocenosController : Controller
    {
        private sitio_seplaEntities db = new sitio_seplaEntities();
        // GET: Conocenos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Antecedentes()
        {
            //valor 1 significa ANTECEDENTES en la tabla TIPO_PLANTILLA
            var pLANTILLA = db.PLANTILLA.Where(s => s.IDTIPO == 1).ToList();
            return View(pLANTILLA);
        }

        public ActionResult Mision()
        {
            //valor 2 significa MISION en la tabla TIPO_PLANTILLA
            var pLANTILLA = db.PLANTILLA.Where(s => s.IDTIPO == 2).ToList();
            return View(pLANTILLA);
        }
        public ActionResult Vision()
        {
            //valor 3 significa VISION en la tabla TIPO_PLANTILLA
            var pLANTILLA = db.PLANTILLA.Where(s => s.IDTIPO == 3).ToList();
            return View(pLANTILLA);
        }
        public ActionResult Objetivos()
        {
            //valor 4 significa OBJETIVOS en la tabla TIPO_PLANTILLA
            var pLANTILLA = db.PLANTILLA.Where(s => s.IDTIPO == 4).ToList();
            return View(pLANTILLA);
        }

        public JsonResult Organigrama_Json()
        {
            //deshabilitar la creación del proxy
            //db.Configuration.ProxyCreationEnabled = false;
            //IDSERVPUB, item.NOMBREPERSONAL, item.NOMBRAMIENTO, item.NIVEL
            var sERVIDORESPUBLICOs = db.SERVIDORESPUBLICOS.Select(
                p => new
                {
                    IDSERPUB = p.IDSERVPUB,
                    NOMBREPERSONAL = p.NOMBREPERSONAL,
                    NOMBRAMIENTO = p.NOMBRAMIENTO,
                    NIVEL = p.NIVEL,
                    ESTATUS = p.ESTATUS
                }).Where(p => p.ESTATUS == "A").FirstOrDefault();
            var json = Json(sERVIDORESPUBLICOs, JsonRequestBehavior.AllowGet);
            //json.MaxJsonLength = 500000000;
            return json;
        }


        public ActionResult Organigrama()
        {
            return View();
        }

        public ActionResult Directorio()
        {
            return View(db.SERVIDORESPUBLICOS.Where(s => s.ESTATUS.Equals("A")).ToList());
        }

        public ActionResult FuncionesPrincipales()
        {
            //valor 5 significa FUNCIONES GENERALES DE LA SPPP en la tabla TIPO_PLANTILLA fdsfsfsds
            var pLANTILLA = db.PLANTILLA.Where(s => s.IDTIPO == 5).ToList(              );
            return View(pLANTILLA);
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