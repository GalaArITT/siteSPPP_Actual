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
using PagedList;
using System.Text;
using System.Globalization;
using Rotativa;

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
            db.Configuration.ProxyCreationEnabled = false;
            //IDSERVPUB, item.NOMBREPERSONAL, item.NOMBRAMIENTO, item.NIVEL
            var sERVIDORESPUBLICOs = db.SERVIDORESPUBLICOS.Where(s=>s.ESTATUS=="A").ToList();
            var json = Json(sERVIDORESPUBLICOs, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        public ActionResult Organigrama()
        {
            return View(db.SERVIDORESPUBLICOS.Where(s=>s.ESTATUS=="A").ToList());
        }

        public ActionResult Diagrama_PDF()
        {
            var report = new ActionAsPdf("Organigrama");
            return report;
        }
        public ActionResult Directorio(string busqueda)
        {
            var servidores = from s in db.SERVIDORESPUBLICOS
                             select s;
            
            //buscar por nombre de servidor 
            // busqueda = busqueda.ToString();
            if (!String.IsNullOrEmpty(busqueda))
            {
                servidores = servidores.Where(s => s.NOMBREPERSONAL.Contains(busqueda.Replace("ABCDEFGHIJKLMNÑOPQRSTUVWXYZaeiou", "abcdefghijklmnñopqrstuvwxyzáéíóú")) 
                || s.NOMBRAMIENTO.Contains(busqueda.Replace("ABCDEFGHIJKLMNÑOPQRSTUVWXYZaeiou", "abcdefghijklmnñopqrstuvwxyzáéíóú")));
                ViewBag.Currentfilter = busqueda;
            }
            else
            {
                ViewBag.Error = "No se encontraron resultados";
            }
            return View(servidores.Where(s => s.ESTATUS.Equals("A")).OrderBy(x => x.IDDEPARTAMENTO).ToList());
        }

        public ActionResult FuncionesPrincipales()
        {
            //valor 5 significa FUNCIONES GENERALES DE LA SPPP en la tabla TIPO_PLANTILLA fdsfsfsds
            var pLANTILLA = db.PLANTILLA.Where(s => s.IDTIPO == 5).ToList();
            return View(pLANTILLA);
        }
        public ActionResult VerNoticias(string currentFilter, string busqueda, int? page)
        {
            var noticias = from s in db.NOTICIAS
                           select s;
            if (busqueda != null)
            {
                page = 1;
            }
            else
            {
                busqueda = currentFilter;
            }
            //buscar por titulo
            // busqueda = busqueda.ToString();
            if (!String.IsNullOrEmpty(busqueda))
            {
                noticias = noticias.Where(s => s.TITULO.Contains(busqueda.Replace("ABCDEFGHIJKLMNÑOPQRSTUVWXYZaeiou", "abcdefghijklmnñopqrstuvwxyzáéíóú")));
                ViewBag.Currentfilter = busqueda;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(noticias.Where(s => s.ESTATUS.Equals("A")).OrderByDescending(x => x.FECHAPUBLIC).ToPagedList(pageNumber,pageSize));
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