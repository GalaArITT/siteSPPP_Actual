using System.Linq;
using System.Web.Mvc;
using siteSPPP.Models;

namespace siteSPPP.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: V_top_noticias
        public ActionResult Index()
        {
            return View(/*db.V_top_noticias.Take(5).ToList()*/);
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Sitio de la Secretaría de Planeación, Programación y Presupuesto del Gobierno del Estado de Nayarit";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "sau@seplan.gob.mx";

            return View();
        }
    }
}