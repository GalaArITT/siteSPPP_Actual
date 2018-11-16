using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using siteSPPP.Models;

namespace siteSPPP.Controllers
{
    public class InformacionConocenosController : Controller
    {
        private sitio_seplaEntities db = new sitio_seplaEntities();

        // GET: InformacionConocenos
        public ActionResult Index()
        {
            var pLANTILLA = db.PLANTILLA.Include(p => p.TIPO_PLANTILLA).Include(p => p.USUARIO);
            return View(pLANTILLA.ToList());
        }

        // GET: InformacionConocenos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLANTILLA pLANTILLA = db.PLANTILLA.Find(id);
            if (pLANTILLA == null)
            {
                return HttpNotFound();
            }
            return View(pLANTILLA);
        }

        // GET: InformacionConocenos/Details/5
        public ActionResult Antecedentes()
        {
            var pLANTILLA = db.PLANTILLA.Where(s=>s.IDTIPO==1).ToList();
            return View(pLANTILLA);
        }

        // GET: InformacionConocenos/Create
        public ActionResult Create()
        {
            ViewBag.IDTIPO = new SelectList(db.TIPO_PLANTILLA, "IDTIPO", "TIPOPLANTILLA");
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA");
            return View();
        }

        // POST: InformacionConocenos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Create([Bind(Include = "IDPLANTILLA,IDUSUARIO,IDTIPO,TITULO,CONTENIDO")] PLANTILLA pLANTILLA)
        {
            if (ModelState.IsValid)
            {
                pLANTILLA.IDUSUARIO = 2;
                db.PLANTILLA.Add(pLANTILLA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDTIPO = new SelectList(db.TIPO_PLANTILLA, "IDTIPO", "TIPOPLANTILLA", pLANTILLA.IDTIPO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA", pLANTILLA.IDUSUARIO);
            return View(pLANTILLA);
        }

        // GET: InformacionConocenos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLANTILLA pLANTILLA = db.PLANTILLA.Find(id);
            if (pLANTILLA == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTIPO = new SelectList(db.TIPO_PLANTILLA, "IDTIPO", "TIPOPLANTILLA", pLANTILLA.IDTIPO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA", pLANTILLA.IDUSUARIO);
            return View(pLANTILLA);
        }

        // POST: InformacionConocenos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "IDPLANTILLA,IDUSUARIO,IDTIPO,TITULO,CONTENIDO")] PLANTILLA pLANTILLA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pLANTILLA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTIPO = new SelectList(db.TIPO_PLANTILLA, "IDTIPO", "TIPOPLANTILLA", pLANTILLA.IDTIPO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA", pLANTILLA.IDUSUARIO);
            return View(pLANTILLA);
        }

        // GET: InformacionConocenos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLANTILLA pLANTILLA = db.PLANTILLA.Find(id);
            if (pLANTILLA == null)
            {
                return HttpNotFound();
            }
            return View(pLANTILLA);
        }

        // POST: InformacionConocenos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PLANTILLA pLANTILLA = db.PLANTILLA.Find(id);
            db.PLANTILLA.Remove(pLANTILLA);
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
