using siteSPPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace siteSPPP.Controllers
{
    public class InicioTestController : Controller
    {
        // GET: InicioTest
        private sitio_seplaEntities db = new sitio_seplaEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index1()
        {
            return View();
        }
    }
}