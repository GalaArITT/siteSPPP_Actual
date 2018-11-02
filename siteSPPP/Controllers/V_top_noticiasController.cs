using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using siteSPPP.Models;

namespace siteSPPP.Controllers
{
    public class V_top_noticiasController : Controller
    {
        private ModeloNoticias db = new ModeloNoticias();

        // GET: V_top_noticias
        public ActionResult Index()
        {
            return View(db.V_top_noticias.ToList());
        }

        // GET: V_top_noticias/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V_top_noticias v_top_noticias = db.V_top_noticias.Find(id);
            if (v_top_noticias == null)
            {
                return HttpNotFound();
            }
            return View(v_top_noticias);
        }

        // GET: V_top_noticias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: V_top_noticias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_NOTICIA,TITULO,FOTO,R")] V_top_noticias v_top_noticias)
        {
            if (ModelState.IsValid)
            {
                db.V_top_noticias.Add(v_top_noticias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(v_top_noticias);
        }

        // GET: V_top_noticias/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V_top_noticias v_top_noticias = db.V_top_noticias.Find(id);
            if (v_top_noticias == null)
            {
                return HttpNotFound();
            }
            return View(v_top_noticias);
        }

        // POST: V_top_noticias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_NOTICIA,TITULO,FOTO,R")] V_top_noticias v_top_noticias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(v_top_noticias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(v_top_noticias);
        }

        // GET: V_top_noticias/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V_top_noticias v_top_noticias = db.V_top_noticias.Find(id);
            if (v_top_noticias == null)
            {
                return HttpNotFound();
            }
            return View(v_top_noticias);
        }

        // POST: V_top_noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            V_top_noticias v_top_noticias = db.V_top_noticias.Find(id);
            db.V_top_noticias.Remove(v_top_noticias);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ObtenerFoto(int id)

        {
            byte[] cover = GetImageFromDataBase(id);
 
            if (cover != null)  {
                return File(cover, "image/jpg");
            }
            else {
                return null;
            }
        }

        public byte[] GetImageFromDataBase(int id) {
            V_top_noticias v_top_noticias = db.V_top_noticias.Find(id);
            byte[] cover = v_top_noticias.FOTO;
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
