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

        // GET: Archivos/Details/5
        public ActionResult Details(int? id)
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

        // GET: Archivos/Create
        public ActionResult Create()
        {
            ViewBag.IDTIPO = new SelectList(db.TIPO_PLANTILLA.Where(s=>s.IDTIPO>=6), "IDTIPO", "TIPOPLANTILLA");
            return View();
        }

        // POST: Archivos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDARCHIVO,IDTIPO,NOMBREARCHIVO,ARCHIVO,IMAGENARCHIVO")] ARCHIVOS aRCHIVOS)
        {
            if (ModelState.IsValid)
            {
                db.ARCHIVOS.Add(aRCHIVOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDTIPO = new SelectList(db.TIPO_PLANTILLA.Where(s => s.IDTIPO >= 6), "IDTIPO", "TIPOPLANTILLA");
            return View(aRCHIVOS);
        }

        // GET: Archivos/Edit/5
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

        // POST: Archivos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDARCHIVO,IDTIPO,NOMBREARCHIVO,ARCHIVO,IMAGENARCHIVO")] ARCHIVOS aRCHIVOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aRCHIVOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTIPO = new SelectList(db.TIPO_PLANTILLA.Where(s => s.IDTIPO >= 6), "IDTIPO", "TIPOPLANTILLA", aRCHIVOS.IDTIPO);
            return View(aRCHIVOS);
        }

        // GET: Archivos/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Archivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ARCHIVOS aRCHIVOS = db.ARCHIVOS.Find(id);
            db.ARCHIVOS.Remove(aRCHIVOS);
            db.SaveChanges();
            return RedirectToAction("Index");
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
