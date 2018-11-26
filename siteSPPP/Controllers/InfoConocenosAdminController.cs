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
    public class InfoConocenosAdminController : Controller
    {
        private sitio_seplaEntities db = new sitio_seplaEntities();

        // GET: InformacionConocenos
        public ActionResult Index()
        {
            //Asegurar que a esta vista solo entren aquellos usuarios con rol 1=Capturista 
            int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
            byte? rol = null;
            //linea para validar que no entre a los controladores hasta que detecte una autenticación
            if (idUsuario == 0)
            {
                Response.Redirect("~/Login/Iniciar");
                rol = null;
            }
            //en caso de que si detecte asignar el valor de rol y dar seguridad
            else
            {
                rol = db.USUARIO.Where(s => s.IDUSUARIO == idUsuario).FirstOrDefault().ROL;
                if (rol != 2) // 2 = Administrador
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                {
                    var pLANTILLA = db.PLANTILLA.Include(p => p.TIPO_PLANTILLA).Include(p => p.USUARIO);
                    return View(pLANTILLA.ToList());
                }
            }
            return null;
        }

        // GET: InformacionConocenos/Details/5
        public ActionResult Details(int? id)
        {
            //Asegurar que a esta vista solo entren aquellos usuarios con rol 1=Capturista 
            int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
            byte? rol = null;
            //linea para validar que no entre a los controladores hasta que detecte una autenticación
            if (idUsuario == 0)
            {
                Response.Redirect("~/Login/Iniciar");
                rol = null;
            }
            //en caso de que si detecte asignar el valor de rol y dar seguridad
            else
            {
                rol = db.USUARIO.Where(s => s.IDUSUARIO == idUsuario).FirstOrDefault().ROL;
                if (rol != 2) // 2 = Administrador
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
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
            }
            return null;
        }

        // GET: InformacionConocenos/Create
        public ActionResult Create()
        {
            //Asegurar que a esta vista solo entren aquellos usuarios con rol 1=Capturista 
            int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
            byte? rol = null;
            //linea para validar que no entre a los controladores hasta que detecte una autenticación
            if (idUsuario == 0)
            {
                Response.Redirect("~/Login/Iniciar");
                rol = null;
            }
            //en caso de que si detecte asignar el valor de rol y dar seguridad
            else
            {
                rol = db.USUARIO.Where(s => s.IDUSUARIO == idUsuario).FirstOrDefault().ROL;
                if (rol != 2) // 2 = Administrador
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                {
                    ViewBag.IDTIPO = new SelectList(db.TIPO_PLANTILLA, "IDTIPO", "TIPOPLANTILLA");
                    ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA");
                    return View();
                }
            }
            return null;
        }

        // POST: InformacionConocenos/Create
        [HttpPost]
        [ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Create([Bind(Include = "IDPLANTILLA,IDUSUARIO,IDTIPO,TITULO,CONTENIDO")] PLANTILLA pLANTILLA)
        {
            int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
            if (ModelState.IsValid)
            {
                pLANTILLA.IDUSUARIO = idUsuario;
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
            //Asegurar que a esta vista solo entren aquellos usuarios con rol 1=Capturista 
            int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
            byte? rol = null;
            //linea para validar que no entre a los controladores hasta que detecte una autenticación
            if (idUsuario == 0)
            {
                Response.Redirect("~/Login/Iniciar");
                rol = null;
            }
            //en caso de que si detecte asignar el valor de rol y dar seguridad
            else
            {
                rol = db.USUARIO.Where(s => s.IDUSUARIO == idUsuario).FirstOrDefault().ROL;
                if (rol != 2) // 2 = Administrador
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
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
            }
            return null;
        }

        // POST: InformacionConocenos/Edit/5
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
