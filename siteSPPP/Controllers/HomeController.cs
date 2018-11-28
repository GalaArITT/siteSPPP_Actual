using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using siteSPPP.Models;

namespace siteSPPP.Controllers
{
    public class HomeController : Controller
    {
        private sitio_seplaEntities db = new sitio_seplaEntities();
        // GET: V_top_noticias
        public ActionResult Index()
        {
            return View(db.NOTICIAS.OrderByDescending(s=>s.FECHAPUBLIC).Take(6).Where(s=>s.ESTATUS=="A").OrderBy(s=>s.PRIORIDAD).ToList());
        }

        public ActionResult MostrarFoto(int id)
        {
            byte[] cover = TraerFoto(id);
            if (cover != null)
            {
                return File(cover, "image/png");
            }
            else
            {
                return null;
            }
        }
        public byte[] TraerFoto(int id)
        {
            FOTOS fotos = db.FOTOS.Find(id);
            byte[] cover = fotos.FOTOGRAFIA;
            return cover;
        }
        // GET: NoticiasTesting/Details/5
        public ActionResult NoticiaCompleta(int? id)
        {
            //crear una lista de fotos con IList de la tabla de fotos donde el id de noticia sea igual al id de noticias de la tabla de fotos
            //Dicha lista agregarla a un objeto Viewbag para poder iterar desde la vista con un foreach
            IList<FOTOS> fotos = db.FOTOS.Where(s=>s.IDNOTICIA == id).ToList();
            ViewBag.fotos = fotos;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NOTICIAS_SEPLAN nOTICIAS_SEPLAN = db.NOTICIAS.Find(id);
            if (nOTICIAS_SEPLAN == null)
            {
                return HttpNotFound();
            }
            return View(nOTICIAS_SEPLAN);
        }
    }
}