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
    public class NoticiasController : Controller
    {
        private ModeloNoticias db = new ModeloNoticias();

        // GET: Noticias
        public ActionResult Index()
        {
            return View(db.Noticias.ToList());
        }

        // GET: Noticias/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
            {
                return HttpNotFound();
            }
            return View(noticias);
        }

        // GET: Noticias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Noticias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_NOTICIA,FECHA,EVENTO,TITULO,ENCABEZADO,FECHA_PUBLICACION,RESPONSABLE,EVENTO2,BALAZO1,ID_FOTO,VER_COPLADENAY,VER_SEPLAN,VER_COCYTEN,VER_FOMIX,VER_INTRANET,BALAZO2,BALAZO3,LUGAR,PRIORIDAD,EVENTO3,SOLO_MEDIOS,PARTICIPANTES,RESENIA,VER_DIF,USUARIO,EVENTO4")] Noticias noticias)
        {
            if (ModelState.IsValid)
            {
                db.Noticias.Add(noticias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(noticias);
        }

        // GET: Noticias/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
            {
                return HttpNotFound();
            }
            return View(noticias);
        }

        // POST: Noticias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_NOTICIA,FECHA,EVENTO,TITULO,ENCABEZADO,FECHA_PUBLICACION,RESPONSABLE,EVENTO2,BALAZO1,ID_FOTO,VER_COPLADENAY,VER_SEPLAN,VER_COCYTEN,VER_FOMIX,VER_INTRANET,BALAZO2,BALAZO3,LUGAR,PRIORIDAD,EVENTO3,SOLO_MEDIOS,PARTICIPANTES,RESENIA,VER_DIF,USUARIO,EVENTO4")] Noticias noticias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noticias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noticias);
        }

        // GET: Noticias/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
            {
                return HttpNotFound();
            }
            return View(noticias);
        }

        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Noticias noticias = db.Noticias.Find(id);
            db.Noticias.Remove(noticias);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ObtenerFoto(int id)

        {
            byte[] cover = GetImageFromDataBase(id);

            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public ActionResult ObtenerFotos(int id, int num)

        {
            byte[] cover = GetImageFromDataBase(id, num);

            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public byte[] GetImageFromDataBase(int id)
        {
            Noticias noticias = db.Noticias.Find(id);
            byte[] cover = noticias.FOTO_PRINCIPAL;
            return cover;
        }


        public byte[] GetImageFromDataBase(int id, int num)
        {

            Noticias noticias = db.Noticias.Find(id);
            byte[] cover = null;
            if (num == 1) { cover = noticias.FOTO_PRINCIPAL; }
            else
                if (num == 2) { cover = noticias.FOTO_2 ; }
                else
                    if (num == 3) { cover = noticias.FOTO_3; }
                    else
                        if (num == 4) { cover = noticias.FOTO_4; }
                        else
                            if (num == 5) { cover = noticias.FOTO_5; }
            return cover;
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
