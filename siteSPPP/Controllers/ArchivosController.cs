using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using siteSPPP.Models;

namespace siteSPPP.Controllers
{
    public class ArchivosController : Controller
    {
        private sitio_seplaEntities db = new sitio_seplaEntities();

        // GET: Archivos
        public ActionResult ControlArchivos(string filtrarfech, string filtrado, string currentFilter, string busqueda, int? page)
        {
            var aRCHIVOS = db.ARCHIVOS.Include(a => a.TIPO_PLANTILLA);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(aRCHIVOS.OrderByDescending(s=>s.FECHA).ToPagedList(pageNumber,pageSize));
        }

        // GET: Archivos/Create
        public ActionResult Create()
        {
            ViewBag.IDTIPO = new SelectList(db.TIPO_PLANTILLA.Where(s=>s.IDTIPO>=6), "IDTIPO", "TIPOPLANTILLA");
            return View();
        }

        // POST: Archivos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDARCHIVO,IDTIPO,NOMBREARCHIVO,FECHA,ESTATUS")] ARCHIVOS aRCHIVOS, HttpPostedFileBase ARCHIVO, HttpPostedFileBase IMAGENARCHIVO)
        {
            int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
            //
            if (IMAGENARCHIVO != null)
            {
                aRCHIVOS.IMAGENARCHIVO = new byte[IMAGENARCHIVO.ContentLength];
                IMAGENARCHIVO.InputStream.Read(aRCHIVOS.IMAGENARCHIVO, 0, IMAGENARCHIVO.ContentLength);
            }
            if (ARCHIVO != null)
            {
                aRCHIVOS.ARCHIVO = new byte[ARCHIVO.ContentLength];
                ARCHIVO.InputStream.Read(aRCHIVOS.ARCHIVO, 0, ARCHIVO.ContentLength);
            }
            if (ModelState.IsValid)
            {
                db.ARCHIVOS.Add(aRCHIVOS);
                aRCHIVOS.FECHA = DateTime.Now;
                aRCHIVOS.ESTATUS = "A";
                db.SaveChanges();
                return RedirectToAction("ControlArchivos");
            }

            ViewBag.IDTIPO = new SelectList(db.TIPO_PLANTILLA.Where(s => s.IDTIPO >= 6), "IDTIPO", "TIPOPLANTILLA");
            return View(aRCHIVOS);
        }

        // GET: ARCHIVOS1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARCHIVOS aRCHIVOS = db.ARCHIVOS.Find(id);
            if (aRCHIVOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTIPO = new SelectList(db.TIPO_PLANTILLA.Where(s=>s.IDTIPO>=6), "IDTIPO", "TIPOPLANTILLA", aRCHIVOS.IDTIPO);
            return View(aRCHIVOS);
        }

        // POST: ARCHIVOS1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDARCHIVO,IDTIPO,NOMBREARCHIVO,FECHA,ESTATUS")] ARCHIVOS aRCHIVOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aRCHIVOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ControlArchivos");
            }
            ViewBag.IDTIPO = new SelectList(db.TIPO_PLANTILLA.Where(s => s.IDTIPO >= 6), "IDTIPO", "TIPOPLANTILLA", aRCHIVOS.IDTIPO);
            return View(aRCHIVOS);
        }
        //TRAER FOTOS Y PDF
        public ActionResult MostrarFoto(int id)
        {
            byte[] cover = TraerFoto(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        public byte[] TraerFoto(int id)
        {
            ARCHIVOS aRCHIVOS = db.ARCHIVOS.Find(id);
            byte[] cover = aRCHIVOS.IMAGENARCHIVO;
            return cover;
        }
        //PDFS
        public ActionResult MostrarPDF(int id)
        {
            byte[] cover = TraerPDF(id);
            if (cover != null)
            {
                return File(cover, "application/pdf");
            }
            else
            {
                return null;
            }
        }
        public byte[] TraerPDF(int id)
        {
            ARCHIVOS aRCHIVOS = db.ARCHIVOS.Find(id);
            byte[] cover = aRCHIVOS.ARCHIVO;
            return cover;
        }
        //CAMBIAR IMAGENES Y PDFS 
        // GET: Funcionarios/Edit/5
        public ActionResult CambiarArchivo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARCHIVOS aRCHIVOS = db.ARCHIVOS.Find(id);
            if (aRCHIVOS == null)
            {
                return HttpNotFound();
            }
            return View(aRCHIVOS);
        }
        //idarchivo, idtipo, nombrearchivo, archivo, imagen, fecha, estatus
        // POST: Funcionarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarArchivo([Bind(Include = "IDARCHIVO,IDTIPO,NOMBREARCHIVO,IMAGENARCHIVO,FECHA,ESTATUS")] ARCHIVOS aRCHIVOS, HttpPostedFileBase ARCHIVO)
        {
            if (ARCHIVO != null)
            {
                aRCHIVOS.ARCHIVO = new byte[ARCHIVO.ContentLength];
                ARCHIVO.InputStream.Read(aRCHIVOS.ARCHIVO, 0, ARCHIVO.ContentLength);
            }
            //
            if (ModelState.IsValid)
            {
                db.Entry(aRCHIVOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ControlArchivos");
            }
            return View(aRCHIVOS);
        }
        // GET: Funcionarios/Edit/5
        public ActionResult CambiarImagen(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARCHIVOS aRCHIVOS = db.ARCHIVOS.Find(id);
            if (aRCHIVOS == null)
            {
                return HttpNotFound();
            }
            return View(aRCHIVOS);
        }
        //idarchivo, idtipo, nombrearchivo, archivo, imagen, fecha, estatus
        // POST: Funcionarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarImagen([Bind(Include = "IDARCHIVO,IDTIPO,NOMBREARCHIVO,ARCHIVO,FECHA,ESTATUS")] ARCHIVOS aRCHIVOS, HttpPostedFileBase IMAGENARCHIVO)
        {
            //
            if (IMAGENARCHIVO != null)
            {
                aRCHIVOS.IMAGENARCHIVO = new byte[IMAGENARCHIVO.ContentLength];
                IMAGENARCHIVO.InputStream.Read(aRCHIVOS.IMAGENARCHIVO, 0, IMAGENARCHIVO.ContentLength);
            }
            if (ModelState.IsValid)
            {
                db.Entry(aRCHIVOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ControlArchivos");
            }
            return View(aRCHIVOS);
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
