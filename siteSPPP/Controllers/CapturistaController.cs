using siteSPPP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace siteSPPP.Controllers
{
    public class CapturistaController : Controller
    {
        private sitio_seplaEntities db = new sitio_seplaEntities();

        public ActionResult Bienvenido()
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
                if (rol != 1) // 1 = Capturista
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                {
                    return View();
                }
            }
            return null;
        }

        //MODULO DE NOTICIAS//

        public ActionResult ListaNoticias(string filtrarfech, string filtrado, string currentFilter, string busqueda, int? page)
        {
            //Asegurar que a esta vista solo entren aquellos usuarios con rol 1=Capturista 
            int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
            var noticias = from s in db.NOTICIAS
                             select s;
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
                if (rol != 1) // 1 = Capturista
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                {
                    if (busqueda != null)
                    {
                        page = 1;
                    }
                    else
                    {
                        busqueda = currentFilter;
                    }
                    //buscar por nombre de servidor 
                    // busqueda = busqueda.ToString();
                    if (!String.IsNullOrEmpty(busqueda))
                    {
                        noticias = noticias.Where(s => s.TITULO.Contains(busqueda) || s.CONTENIDO.Contains(busqueda));
                        ViewBag.Currentfilter = busqueda;
                    }
                    //Filtrar por estatus
                    if (!string.IsNullOrEmpty(filtrado))
                    {
                        filtrado = filtrado.ToString();
                        noticias = noticias.Where(s => s.ESTATUS.ToString().Contains(filtrado));
                        ViewBag.filtrado = filtrado;
                    }
                    //Filtrar por fechas
                    if (!string.IsNullOrEmpty(filtrarfech))
                    {
                        DateTime filtrarfech1 = Convert.ToDateTime(filtrarfech).Date;
                        noticias = noticias.Where(s => s.FECHAPUBLIC == filtrarfech1);
                        ViewBag.filtrarfech = filtrarfech;
                    }
                    //Mostrar una lista de noticias desde la tabla objeto NOTICIAS que hace referencia a NOTICIAS_SEPLA
                    int pageSize = 5;
                    int pageNumber = (page ?? 1);

                    return View(noticias.OrderBy(x => x.FECHAPUBLIC).Where(s=>s.IDUSUARIO== idUsuario).ToPagedList(pageNumber, pageSize));
                }
            }
            return null;
        }
        //GET mostrar la vista de crearNoticas
        public ActionResult CrearNoticias()
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
                if (rol != 1) // 1 = Capturista
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                {
                    return View();
                }
            }
            return null;
        }
        //Recepción de los datos de noticias
        [HttpPost]
        [ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult CrearNoticias([Bind(Include = "IDNOTICIA,IDUSUARIO,TITULO,CONTENIDO,FECHACAPTURA,FECHAPUBLIC,LUGAR,PRIORIDAD,ESTATUS")]
        NOTICIAS_SEPLAN Noticias_Seplan)
        {
            int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
            if (ModelState.IsValid)
            {
                Noticias_Seplan.IDUSUARIO = idUsuario;
                Noticias_Seplan.FECHACAPTURA = DateTime.Now;
                Noticias_Seplan.ESTATUS = "A";
                db.NOTICIAS.Add(Noticias_Seplan);
                db.SaveChanges();
                return RedirectToAction("ListaNoticias");
            }

            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA", Noticias_Seplan.IDUSUARIO);
            return View(Noticias_Seplan);
        }
        //Obtener idUsuario para la PK
        
        // GET: NoticiasTesting/Details/5
        public ActionResult DetalleNoticias(int? id)
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
                if (rol != 1) // 1 = Capturista
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                {
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
            return null;
        }
        // GET: NoticiasTesting/Edit/5
        public ActionResult EditarNoticias(int? id)
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
                if (rol != 1) // 1 = Capturista
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    NOTICIAS_SEPLAN nOTICIAS_SEPLAN = db.NOTICIAS.Find(id);
                    if (nOTICIAS_SEPLAN == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.IDNOTICIA = new SelectList(db.FOTOS, "IDNOTICIA", "IDNOTICIA", nOTICIAS_SEPLAN.IDNOTICIA);
                    return View(nOTICIAS_SEPLAN);
                }
            }
            return null;
        }

        // POST: Editar Noticas
        [HttpPost]
        [ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult EditarNoticias([Bind(Include = "IDNOTICIA,IDUSUARIO,TITULO,CONTENIDO,FECHACAPTURA,FECHAPUBLIC,LUGAR,PRIORIDAD,ESTATUS")] NOTICIAS_SEPLAN Noticias_Seplan)
        {
            //hay que agregar todos los campos en el bind y los idnoticia, idusuario, estatus, fechacaptura hay que colocarlos dentro de un 
            //hidden sino este desgraciado segmento
            if (ModelState.IsValid)
            {
                db.Entry(Noticias_Seplan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaNoticias");
            }
            ViewBag.IDNOTICIA = new SelectList(db.FOTOS, "IDNOTICIA", "IDNOTICIA", Noticias_Seplan.IDNOTICIA);
            return View(Noticias_Seplan);

        }
        // GET: EliminarNoticias
        public ActionResult EliminarNoticias(int? id)
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
                if (rol != 1) // 1 = Capturista
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                {
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
            return null;
        }

        // POST: SalidaPersonas/Delete/5
        [HttpPost, ActionName("EliminarNoticias")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarNoticias(int id)
        {
            NOTICIAS_SEPLAN nOTICIAS_SEPLAN = db.NOTICIAS.Find(id);
            if (ModelState.IsValid)
            {
                //el valor I significa inactivo
                nOTICIAS_SEPLAN.ESTATUS = "I";
                db.Entry(nOTICIAS_SEPLAN).Property(x => x.ESTATUS).IsModified = true;
                db.Entry(nOTICIAS_SEPLAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaNoticias", new { id = nOTICIAS_SEPLAN.IDNOTICIA });
            }
            return View(nOTICIAS_SEPLAN);
        }

        //Seccion de fotos
        public ActionResult ListaFotos(int? idNoticia)
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
                if (rol != 1) // 1 = Capturista
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                {
                    //encontrar por id 
                    if (idNoticia == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    //mostrar lista referente a idnoticias
                    ViewBag.idNoticia = idNoticia;
                    return View(db.FOTOS.Where(p => p.IDNOTICIA == idNoticia.Value).ToList());
                }
            }
            return null;
        }

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
            FOTOS fotos = db.FOTOS.Find(id);
            byte[] cover = fotos.FOTOGRAFIA;
            return cover;
        }
        // GET: FotosTesting/Create
        public ActionResult AgregarFoto(int? idNoticia)
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
                if (rol != 1) // 1 = Capturista
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                {
                    if (idNoticia == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    NOTICIAS_SEPLAN Noticias = db.NOTICIAS.Find(idNoticia);
                    if (idNoticia == null)
                    {
                        return HttpNotFound();
                    }
                    FOTOS fotos = new FOTOS();
                    fotos.IDNOTICIA = idNoticia.Value; // asignar valor de id
                    return View(fotos);
                }
            }
            return null;
        }
        // POST: FotosTesting/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarFoto([Bind(Include = "IDNOTICIA,IDFOTO")] FOTOS fOTOS, HttpPostedFileBase FOTOGRAFIA)
        {
            if (FOTOGRAFIA != null)
            {
                fOTOS.FOTOGRAFIA = new byte[FOTOGRAFIA.ContentLength];
                FOTOGRAFIA.InputStream.Read(fOTOS.FOTOGRAFIA, 0, FOTOGRAFIA.ContentLength);
            }
            if (ModelState.IsValid)
            {
                db.FOTOS.Add(fOTOS);
                db.SaveChanges();
                return RedirectToAction("ListaFotos", new { idNoticia = fOTOS.IDNOTICIA });
            }

            ViewBag.IDNOTICIA = new SelectList(db.NOTICIAS, "IDNOTICIA", "TITULO", fOTOS.IDNOTICIA);
            return View(fOTOS);
        }

        // GET: FotosTesting/Edit/5
        public ActionResult EditarFoto(int? id)
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
                if (rol != 1) // 1 = Capturista
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    FOTOS fOTOS = db.FOTOS.Find(id);
                    if (fOTOS == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.IDNOTICIA = new SelectList(db.NOTICIAS, "IDNOTICIA", "TITULO", fOTOS.IDNOTICIA);
                    return View(fOTOS);
                }
            }
            return null;
        }

        // POST: FotosTesting/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarFoto([Bind(Include = "IDNOTICIA,IDFOTO")] FOTOS fOTOS, HttpPostedFileBase FOTOGRAFIA)
        {
            if (FOTOGRAFIA != null && FOTOGRAFIA.ContentLength > 0)
            {
                byte[] imageData = null;
                using (var binaryMunicipio = new BinaryReader(FOTOGRAFIA.InputStream))
                {
                    imageData = binaryMunicipio.ReadBytes(FOTOGRAFIA.ContentLength);
                }
                fOTOS.FOTOGRAFIA = imageData;
            }
            if (ModelState.IsValid)
            {
                db.Entry(fOTOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaFotos", new { idNoticia = fOTOS.IDNOTICIA });
            }
            ViewBag.IDNOTICIA = new SelectList(db.NOTICIAS, "IDNOTICIA", "TITULO", fOTOS.IDNOTICIA);
            return View(fOTOS);
        }
        
        // GET: FOTOS/Delete/5
        public ActionResult EliminarFoto(int? id)
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
                if (rol != 1) // 1 = Capturista
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                else
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    FOTOS fOTOS = db.FOTOS.Find(id);
                    if (fOTOS == null)
                    {
                        return HttpNotFound();
                    }
                    return View(fOTOS);
                }
            }
            return null;
        }

        // POST: FOTOS/Delete/5
        [HttpPost, ActionName("EliminarFoto")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarFotoConfirmed(int id)
        {
            FOTOS fOTOS = db.FOTOS.Find(id);
            db.FOTOS.Remove(fOTOS);
            db.SaveChanges();
            return RedirectToAction("ListaFotos", new { idNoticia = fOTOS.IDNOTICIA });
        }

    }
}