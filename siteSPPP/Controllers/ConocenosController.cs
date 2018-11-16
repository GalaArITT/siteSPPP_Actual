using Microsoft.Reporting.WebForms;
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

        public ActionResult Organigrama()
        {
            return View(db.SERVIDORESPUBLICOS.Where(s=>s.ESTATUS.Equals("A")).ToList());
        }

        public ActionResult GenerarPdf()
        {

            SERVIDORESPUBLICOS salidaPersona = db.SERVIDORESPUBLICOS.FirstOrDefault();

            ReportViewer rv = new ReportViewer();
            rv.ProcessingMode = ProcessingMode.Local;
            rv.LocalReport.ReportPath = Server.MapPath("~/Organigrama/Organigrama.rdlc");
            rv.LocalReport.Refresh();

            DatosOrganigrama xsd = new DatosOrganigrama();
            DatosOrganigrama.OrganigramaRow fila = null;

            fila = xsd.Organigrama.NewOrganigramaRow();

            fila.Departamento = salidaPersona.DEPARTAMENTOS.NOMBREDEPTO;
            try
            {
                fila.Nombre = salidaPersona.NOMBREPERSONAL;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error" + ex);
            }
            xsd.Organigrama.Rows.Add(fila);

            rv.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", (DataTable)xsd.Organigrama));

            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;
            string deviceInfo = null;

            streamBytes = rv.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            return File(streamBytes, mimeType);
        }
        public ActionResult Directorio()
        {
            return View(db.SERVIDORESPUBLICOS.Where(s => s.ESTATUS.Equals("A")).ToList());
        }

        public ActionResult FuncionesPrincipales()
        {
            //valor 5 significa FUNCIONES GENERALES DE LA SPPP en la tabla TIPO_PLANTILLA
            var pLANTILLA = db.PLANTILLA.Where(s => s.IDTIPO == 5).ToList();
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