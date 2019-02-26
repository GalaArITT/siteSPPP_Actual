using siteSPPP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
using System.Text;
using System.Globalization;

namespace siteSPPP.Controllers
{
    public class AdministradorController : Controller
    {
        private sitio_seplaEntities db = new sitio_seplaEntities();
        //metodo para dar seguridad al usuario
        public ActionResult Seguridad_Administrador()
        {
            return null;
        }

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
                if (rol != 2) // 2 = Administrador
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
        public ActionResult ListaNoticiasAdmin(string filtrarfech, string filtrado, string currentFilter, string busqueda, int? page)
        {
            //Asegurar que a esta vista solo entren aquellos usuarios con rol 2=Administrador 
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
                if (rol != 2) // 2 = Administrador
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
                        noticias = noticias.Where(s => s.TITULO.Contains(busqueda.Replace("ABCDEFGHIJKLMNÑOPQRSTUVWXYZÁÉÍÓÚ", 
                            "abcdefghijklmnñopqrstuvwxyzáéíóú")) 
                        || s.CONTENIDO.Contains(busqueda.Replace("ABCDEFGHIJKLMNÑOPQRSTUVWXYZÁÉÍÓÚ", 
                        "abcdefghijklmnñopqrstuvwxyzáéíóú")));
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

                    return View(noticias.OrderBy(x => x.FECHAPUBLIC).ToPagedList(pageNumber, pageSize));
                }
            }
            return null;
        }
        // GET: NoticiasTesting/Edit/5
        public ActionResult EditarNoticiasAdmin(int? id)
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
                    //instanciar tabla de noticias y encontrar por el id que está en la vista editarnoticia
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
        public ActionResult EditarNoticiasAdmin([Bind(Include = "IDNOTICIA,IDUSUARIO,TITULO,CONTENIDO,FECHACAPTURA,FECHAPUBLIC,LUGAR,PRIORIDAD,ESTATUS")] NOTICIAS_SEPLAN Noticias_Seplan)
        {
            //hay que agregar todos los campos en el bind y los idnoticia, idusuario, estatus, fechacaptura hay que colocarlos 
            //dentro de un  hidden sino este desgraciado segmento marca una excepción
            if (ModelState.IsValid)
            {
                db.Entry(Noticias_Seplan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaNoticiasAdmin");
            }
            ViewBag.IDNOTICIA = new SelectList(db.FOTOS, "IDNOTICIA", "IDNOTICIA", Noticias_Seplan.IDNOTICIA);
            return View(Noticias_Seplan);

        }
        // GET: EliminarNoticias
        public ActionResult EliminarNoticiasAdmin(int? id)
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
        [HttpPost, ActionName("EliminarNoticiasAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarNoticiasAdmin(int id)
        {
            NOTICIAS_SEPLAN nOTICIAS_SEPLAN = db.NOTICIAS.Find(id);
            if (ModelState.IsValid)
            {
                //el valor I significa inactivo
                nOTICIAS_SEPLAN.ESTATUS = "I";
                db.Entry(nOTICIAS_SEPLAN).Property(x => x.ESTATUS).IsModified = true;
                db.Entry(nOTICIAS_SEPLAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaNoticiasAdmin", new { id = nOTICIAS_SEPLAN.IDNOTICIA });
            }
            return View(nOTICIAS_SEPLAN);
        }

        //Seccion de fotos
        public ActionResult ListaFotosAdmin(int? idNoticia)
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
        // GET: FotosTesting/Edit/5
        public ActionResult EditarFotoAdmin(int? id)
        {
            //Asegurar que a esta vista solo entren aquellos usuarios con rol 2=Administrador 
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
        public ActionResult EditarFotoAdmin([Bind(Include = "IDNOTICIA,IDFOTO")] FOTOS fOTOS, HttpPostedFileBase FOTOGRAFIA)
        {
            if (FOTOGRAFIA != null && FOTOGRAFIA.ContentLength > 0)
            {
                byte[] imageData = null;
                using (var foto_nota = new BinaryReader(FOTOGRAFIA.InputStream))
                {
                    imageData = foto_nota.ReadBytes(FOTOGRAFIA.ContentLength);
                }
                fOTOS.FOTOGRAFIA = imageData;
            }
            if (ModelState.IsValid)
            {
                db.Entry(fOTOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaFotosAdmin", new { idNoticia = fOTOS.IDNOTICIA });
            }
            ViewBag.IDNOTICIA = new SelectList(db.NOTICIAS, "IDNOTICIA", "TITULO", fOTOS.IDNOTICIA);
            return View(fOTOS);
        }

        // GET: FOTOS/Delete/5
        public ActionResult EliminarFotoAdmin(int? id)
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

        //Modulo de usuarios
        public ActionResult ListaUsuarios(string filtrado, string currentFilter, string busqueda, int? page)
        {
            //Asegurar que a esta vista solo entren aquellos usuarios con rol 1=Capturista 
            int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
            var usuarios = from s in db.USUARIO
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
                if (rol != 2) // 2 = Administrador
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
                        usuarios = usuarios.Where(s => s.NOMBREUSUARIO.Contains(busqueda.Replace("ABCDEFGHIJKLMNÑOPQRSTUVWXYZaeiou", "abcdefghijklmnñopqrstuvwxyzáéíóú")) 
                        || s.USUARIOINICIA.Contains(busqueda.Replace("ABCDEFGHIJKLMNÑOPQRSTUVWXYZaeiou", "abcdefghijklmnñopqrstuvwxyzáéíóú")));
                        ViewBag.Currentfilter = busqueda;
                    }
                    //filtrar por estatus

                    //condicional para dropdownlist de filtro
                    if (!string.IsNullOrEmpty(filtrado))
                    {
                        filtrado = filtrado.ToString();
                        usuarios = usuarios.Where(s => s.ESTATUS.ToString().Contains(filtrado));
                        ViewBag.filtrado = filtrado;
                    }

                    int pageSize = 5;
                    int pageNumber = (page ?? 1);
                    //codigo de lista de usuarios
                    return View(usuarios.OrderBy(x => x.FECHAREGISTRO).ToPagedList(pageNumber, pageSize));
                }
            }
            return null;
        }
        // GET
        public ActionResult CrearUsuario()
        {
            return View();
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearUsuario([Bind(Include = "IDUSUARIO,USUARIOINICIA,CONTRASENA,NOMBREUSUARIO,ROL,ESTATUS,FECHAREGISTRO,CORREO")]
        USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                uSUARIO.FECHAREGISTRO = DateTime.Now; //capturar la fecha actual
                uSUARIO.ESTATUS = "A";
                db.USUARIO.Add(uSUARIO);
                db.SaveChanges();//guardar cambios
                return RedirectToAction("ListaUsuarios");
            }

            return View(uSUARIO);
        }
        //TRAER FOTOS Y PDF
        public ActionResult MostrarFoto(int id)
        {
            byte[] cover = TraerFoto(id);
            if (cover != null)
            {
                return File(cover, "image/jpg"); //retornar las imagenes en ese formato
            }
            else
            {
                return null;
            }
        }
        public byte[] TraerFoto(int id)
        {
            SERVIDORESPUBLICOS sERVIDORESPUBLICOS = db.SERVIDORESPUBLICOS.Find(id);
            byte[] cover = sERVIDORESPUBLICOS.FOTOPERSONAL;
            return cover;
        }
        public ActionResult MostrarFotoNoticias(int id)
        {
            byte[] cover = TraerFotoNoticias(id);
            if (cover != null)
            {
                return File(cover, "image/jpg"); //retornar las imagenes en ese formato
            }
            else
            {
                return null;
            }
        }
        public byte[] TraerFotoNoticias(int id)
        {
            FOTOS fotos = db.FOTOS.Find(id);
            byte[] cover = fotos.FOTOGRAFIA;
            return cover;
        }
        //PDFS
        public ActionResult MostrarPDF(int id)
        {
            byte[] cover = TraerPDF(id);
            if (cover != null)
            {
                return File(cover, "application/pdf"); 
            }
            else
            {
                return null;
            }
        }
        public byte[] TraerPDF(int id)
        {
            SERVIDORESPUBLICOS sERVIDORESPUBLICOS = db.SERVIDORESPUBLICOS.Find(id);
            byte[] cover = sERVIDORESPUBLICOS.CURRICULUM;
            return cover;
        }
        //MOSTRAR FOTOS ORGANIGRAMAS
        public ActionResult MostrarFotoOrg(int id)
        {
            byte[] cover = TraerFotoOrg(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        public byte[] TraerFotoOrg(int id)
        {
            ORGANIGRAMA oRGANIGRAMA = db.ORGANIGRAMA.Find(id);
            byte[] cover = oRGANIGRAMA.IMAGEN;
            return cover;
        }
        // GET: USUARIOs/Edit/5
        public ActionResult EditarUsuario(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO);
        }

        // POST: USUARIOs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarUsuario([Bind(Include = "IDUSUARIO,USUARIOINICIA,CONTRASENA,NOMBREUSUARIO,ROL,ESTATUS,FECHAREGISTRO,CORREO")] USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSUARIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaUsuarios");
            }
            return View(uSUARIO);
        }
        //Modulo de Funcionarios Públicos
        public ActionResult ListaFuncionarios(string filtrado, string currentFilter, string busqueda, int? page)
        {
            var servidores = from s in db.SERVIDORESPUBLICOS
                             select s;
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
                        servidores = servidores.Where(s => s.NOMBREPERSONAL.Contains(busqueda.Replace("ABCDEFGHIJKLMNÑOPQRSTUVWXYZ0ÁÉÍÓÚáéíóú", "abcdefghijklmnñopqrstuvwxyzáéíóú")) 
                        || s.NOMBRAMIENTO.Contains(busqueda.Replace("ABCDEFGHIJKLMNÑOPQRSTUVWXYZÁÉÍÓÚáéíóú", "abcdefghijklmnñopqrstuvwxzyáéíóú")));
                        ViewBag.Currentfilter = busqueda;
                        
                    }
                    //filtrar por estatus

                    //condicional para dropdownlist de filtro
                    if (!string.IsNullOrEmpty(filtrado))
                    {
                        filtrado = filtrado.ToString();
                        servidores = servidores.Where(s => s.ESTATUS.ToString().Contains(filtrado));
                        ViewBag.filtrado = filtrado;
                    }

                    int pageSize = 5;
                    int pageNumber = (page ?? 1);

                    return View(servidores.OrderBy(x => x.IDDEPARTAMENTO).ToPagedList(pageNumber, pageSize));
                }
            }
            return null;
        }

        // GET: Funcionarios/AgregarFuncionario
        public ActionResult AgregarFuncionario()
        {
            ViewBag.IDDEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "IDDEPARTAMENTO", "NOMBREDEPTO");
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA");
            //crear una lista de fotos con IList de la tabla de fotos donde el id de noticia sea igual al id de noticias de la tabla de fotos
            //Dicha lista agregarla a un objeto Viewbag para poder iterar desde la vista con un foreach
            ViewBag.servidores = new SelectList(db.SERVIDORESPUBLICOS.Where(s => s.ESTATUS == "A").ToList(), "IDSERVPUB", "NOMBRAMIENTO");
            return View();
        }

        // POST: Funcionarios/AgregarFuncionario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarFuncionario([Bind(Include = "IDSERVPUB,IDDEPARTAMENTO,IDUSUARIO,NOMBREPERSONAL,NOMBRAMIENTO,CONMUTADOR,EXTENSION," +
            "CORREO,ESTATUS,FECHAREGISTRO,NIVEL")] SERVIDORESPUBLICOS sERVIDORESPUBLICOS, HttpPostedFileBase FOTOPERSONAL, HttpPostedFileBase CURRICULUM)
        {
            int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
            //
            if (FOTOPERSONAL != null)
            {
                sERVIDORESPUBLICOS.FOTOPERSONAL = new byte[FOTOPERSONAL.ContentLength];
                FOTOPERSONAL.InputStream.Read(sERVIDORESPUBLICOS.FOTOPERSONAL, 0, FOTOPERSONAL.ContentLength);
            }
            if (CURRICULUM != null)
            {
                sERVIDORESPUBLICOS.CURRICULUM = new byte[CURRICULUM.ContentLength];
                CURRICULUM.InputStream.Read(sERVIDORESPUBLICOS.CURRICULUM, 0, CURRICULUM.ContentLength);
            }
            //
            if (ModelState.IsValid)
            {
                sERVIDORESPUBLICOS.FECHAREGISTRO = DateTime.Now;
                //sERVIDORESPUBLICOS.ESTATUS = "A";
                sERVIDORESPUBLICOS.IDUSUARIO = idUsuario; //idUsuario; 

                db.SERVIDORESPUBLICOS.Add(sERVIDORESPUBLICOS);
                db.SaveChanges();
                return RedirectToAction("ListaFuncionarios");
            }

            ViewBag.IDDEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "IDDEPARTAMENTO", "NOMBREDEPTO", sERVIDORESPUBLICOS.IDDEPARTAMENTO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA", sERVIDORESPUBLICOS.IDUSUARIO);
            ViewBag.servidores = new SelectList(db.SERVIDORESPUBLICOS.Where(s => s.ESTATUS == "A").ToList(), "IDSERVPUB", "NOMBRAMIENTO", sERVIDORESPUBLICOS.IDSERVPUB);
            return View(sERVIDORESPUBLICOS);
        }

        // GET: Funcionarios/EditarFuncionario
        public ActionResult EditarFuncionario(int? id)
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
            //Lista de funcionarios 
            ViewBag.servidores = new SelectList(db.SERVIDORESPUBLICOS.Where(s => s.ESTATUS == "A").ToList(),"IDSERVPUB", "NOMBRAMIENTO",sERVIDORESPUBLICOS.IDSERVPUB);
            return View(sERVIDORESPUBLICOS);
        }

        // POST: Funcionarios/EditarFuncionario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarFuncionario([Bind(Include = "IDSERVPUB,IDDEPARTAMENTO,IDUSUARIO,NOMBREPERSONAL,NOMBRAMIENTO,CONMUTADOR," +
            "EXTENSION,FOTOPERSONAL,CORREO,CURRICULUM,ESTATUS,FECHAREGISTRO,NIVEL,ESTATUS")] SERVIDORESPUBLICOS sERVIDORESPUBLICOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sERVIDORESPUBLICOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaFuncionarios");
            }
            ViewBag.IDDEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "IDDEPARTAMENTO", "NOMBREDEPTO", sERVIDORESPUBLICOS.IDDEPARTAMENTO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA", sERVIDORESPUBLICOS.IDUSUARIO);
            ViewBag.servidores = new SelectList(db.SERVIDORESPUBLICOS.Where(s => s.ESTATUS == "A").ToList(), "IDSERVPUB", "NOMBRAMIENTO", sERVIDORESPUBLICOS.IDSERVPUB);
            return View(sERVIDORESPUBLICOS);
        }
        // GET: Funcionarios/CambiarCurriculumFuncionario
        public ActionResult CambiarCurriculumFuncionario(int? id)
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

        // POST: Funcionarios/CambiarCurriculumFuncionario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarCurriculumFuncionario([Bind(Include = "IDSERVPUB,IDDEPARTAMENTO,IDUSUARIO,NOMBREPERSONAL,NOMBRAMIENTO,CONMUTADOR,EXTENSION,FOTOPERSONAL," +
            "CORREO,ESTATUS,FECHAREGISTRO,NIVEL")] SERVIDORESPUBLICOS sERVIDORESPUBLICOS,HttpPostedFileBase CURRICULUM)
        {
            if (CURRICULUM != null)
            {
                sERVIDORESPUBLICOS.CURRICULUM = new byte[CURRICULUM.ContentLength];
                CURRICULUM.InputStream.Read(sERVIDORESPUBLICOS.CURRICULUM, 0, CURRICULUM.ContentLength);
            }
            //
            if (ModelState.IsValid)
            {
                db.Entry(sERVIDORESPUBLICOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaFuncionarios");
            }
            return View(sERVIDORESPUBLICOS);
        }
        // GET: Funcionarios/CambiarFotoFuncionario
        public ActionResult CambiarFotoFuncionario(int? id)
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

        // POST: Funcionarios/CambiarFotoFuncionario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarFotoFuncionario([Bind(Include = "IDSERVPUB,IDDEPARTAMENTO,IDUSUARIO,NOMBREPERSONAL,NOMBRAMIENTO," +
            "CONMUTADOR,EXTENSION,CORREO,CURRICULUM,ESTATUS,FECHAREGISTRO,NIVEL")] SERVIDORESPUBLICOS sERVIDORESPUBLICOS, HttpPostedFileBase FOTOPERSONAL)
        {
            //
            if (FOTOPERSONAL != null)
            {
                sERVIDORESPUBLICOS.FOTOPERSONAL = new byte[FOTOPERSONAL.ContentLength];
                FOTOPERSONAL.InputStream.Read(sERVIDORESPUBLICOS.FOTOPERSONAL, 0, FOTOPERSONAL.ContentLength);
            }
            if (ModelState.IsValid)
            {
                db.Entry(sERVIDORESPUBLICOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaFuncionarios");
            }
            return View(sERVIDORESPUBLICOS);
        }

        // GET: Funcionarios/DetalleFuncionario
        public ActionResult DetalleFuncionario(int? id)
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

        //CONTROL DE ORGANIGRAMAS

        public ActionResult ListaOrganigramas()
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
                    return View(db.ORGANIGRAMA.OrderByDescending(s => s.FECHACREACION).ToList());
                }
            }
            return null;
        }

        //GET 
        public ActionResult AgregarOrganigrama()
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
                    return View();
                }
            }
            return null;
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarOrganigrama([Bind(Include ="IDORGANIGRAMA,IDUSUARIO,FECHACREACION,ESTATUS")] ORGANIGRAMA oRGANIGRAMA, HttpPostedFileBase IMAGEN)
        {
            int idUsuario = Convert.ToInt32(Session["IDUSUARIO"]);
            //
            if (IMAGEN != null)
            {
                oRGANIGRAMA.IMAGEN = new byte[IMAGEN.ContentLength];
                IMAGEN.InputStream.Read(oRGANIGRAMA.IMAGEN, 0, IMAGEN.ContentLength);
            }
            //
            if (ModelState.IsValid)
            {
                oRGANIGRAMA.FECHACREACION = DateTime.Now;
                oRGANIGRAMA.ESTATUS = "A";
                oRGANIGRAMA.IDUSUARIO = 1; //idUsuario; 

                db.ORGANIGRAMA.Add(oRGANIGRAMA);
                db.SaveChanges();
                return RedirectToAction("ListaOrganigramas");
            }
            return View(oRGANIGRAMA);
        }
        //GET
        public ActionResult EditarOrganigrama(int? id)
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
                    ORGANIGRAMA oRGANIGRAMA = db.ORGANIGRAMA.Find(id);
                    if (oRGANIGRAMA == null)
                    {
                        return HttpNotFound();
                    }
                    return View(oRGANIGRAMA);
                }
            }
            return null;
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarOrganigrama([Bind(Include = "IDORGANIGRAMA,IDUSUARIO,IMAGEN,FECHACREACION,ESTATUS")] ORGANIGRAMA oRGANIGRAMA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRGANIGRAMA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaOrganigramas");
            }

            return View(oRGANIGRAMA);
        }

        //GET
        public ActionResult CambiarOrganigrama(int? id)
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
                    ORGANIGRAMA oRGANIGRAMA = db.ORGANIGRAMA.Find(id);
                    if (oRGANIGRAMA == null)
                    {
                        return HttpNotFound();
                    }
                    return View(oRGANIGRAMA);
                }
            }
            return null;
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarOrganigrama([Bind(Include = "IDORGANIGRAMA,IDUSUARIO,FECHACREACION,ESTATUS")] ORGANIGRAMA oRGANIGRAMA, HttpPostedFileBase IMAGEN)
        {
            if (IMAGEN != null)
            {
                oRGANIGRAMA.IMAGEN = new byte[IMAGEN.ContentLength];
                IMAGEN.InputStream.Read(oRGANIGRAMA.IMAGEN, 0, IMAGEN.ContentLength);
            }
            if (ModelState.IsValid)
            {
                db.Entry(oRGANIGRAMA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaOrganigramas");
            }

            return View(oRGANIGRAMA);
        }

        public ActionResult VerOrganigrama(int? id)
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
                    ORGANIGRAMA oRGANIGRAMA = db.ORGANIGRAMA.Find(id);
                    if (oRGANIGRAMA == null)
                    {
                        return HttpNotFound();
                    }
                    return View(oRGANIGRAMA);
                }
            }
            return null;
        }
    }
}
 