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
    public class FuncionariosController : Controller
    {
        private sitio_seplaEntities db = new sitio_seplaEntities();

        // GET: Funcionarios
        public ActionResult Index()
        {
            var sERVIDORESPUBLICOS = db.SERVIDORESPUBLICOS.Include(s => s.DEPARTAMENTOS).Include(s => s.USUARIO);
            return View(sERVIDORESPUBLICOS.ToList());
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SERVIDORESPUBLICOS sERVIDORESPUBLICOS = db.SERVIDORESPUBLICOS.Find(id);
            if (sERVIDORESPUBLICOS == null)
            {
                return HttpNotFound();
            }
            return View(sERVIDORESPUBLICOS);
        }

        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            ViewBag.IDDEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "IDDEPARTAMENTO", "NOMBREDEPTO");
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA");
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSERVPUB,IDDEPARTAMENTO,IDUSUARIO,NOMBREPERSONAL,NOMBRAMIENTO,CONMUTADOR,EXTENSION,FOTOPERSONAL,CORREO,CURRICULUM,ESTATUS,FECHAREGISTRO,NIVEL")] SERVIDORESPUBLICOS sERVIDORESPUBLICOS)
        {
            if (ModelState.IsValid)
            {
                sERVIDORESPUBLICOS.NIVEL = 1;
                sERVIDORESPUBLICOS.FECHAREGISTRO = DateTime.Now;
                db.SERVIDORESPUBLICOS.Add(sERVIDORESPUBLICOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDDEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "IDDEPARTAMENTO", "NOMBREDEPTO", sERVIDORESPUBLICOS.IDDEPARTAMENTO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA", sERVIDORESPUBLICOS.IDUSUARIO);
            return View(sERVIDORESPUBLICOS);
        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SERVIDORESPUBLICOS sERVIDORESPUBLICOS = db.SERVIDORESPUBLICOS.Find(id);
            if (sERVIDORESPUBLICOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "IDDEPARTAMENTO", "NOMBREDEPTO", sERVIDORESPUBLICOS.IDDEPARTAMENTO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA", sERVIDORESPUBLICOS.IDUSUARIO);
            return View(sERVIDORESPUBLICOS);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSERVPUB,IDDEPARTAMENTO,IDUSUARIO,NOMBREPERSONAL,NOMBRAMIENTO,CONMUTADOR,EXTENSION,FOTOPERSONAL,CORREO,CURRICULUM,ESTATUS,FECHAREGISTRO,NIVEL")] SERVIDORESPUBLICOS sERVIDORESPUBLICOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sERVIDORESPUBLICOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "IDDEPARTAMENTO", "NOMBREDEPTO", sERVIDORESPUBLICOS.IDDEPARTAMENTO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA", sERVIDORESPUBLICOS.IDUSUARIO);
            return View(sERVIDORESPUBLICOS);
        }

        // GET: Funcionarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SERVIDORESPUBLICOS sERVIDORESPUBLICOS = db.SERVIDORESPUBLICOS.Find(id);
            if (sERVIDORESPUBLICOS == null)
            {
                return HttpNotFound();
            }
            return View(sERVIDORESPUBLICOS);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SERVIDORESPUBLICOS sERVIDORESPUBLICOS = db.SERVIDORESPUBLICOS.Find(id);
            db.SERVIDORESPUBLICOS.Remove(sERVIDORESPUBLICOS);
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
