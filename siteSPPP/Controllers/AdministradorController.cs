using siteSPPP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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
        //Modulo de usuarios
        public ActionResult ListaUsuarios()
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
                    //codigo de lista de usuarios
                    return View(db.USUARIO.ToList());
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
                return File(cover, "image/jpg");
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
        public ActionResult EditarUsuario([Bind(Include = "IDUSUARIO,USUARIOINICIA,CONTRASENA,NOMBREUSUARIO,ROL,ESTATUS,FECHAREGISTRO")] USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                uSUARIO.FECHAREGISTRO = DateTime.Now;
                db.Entry(uSUARIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaUsuarios");
            }
            return View(uSUARIO);
        }

        // GET: USUARIOs/Delete/5
        public ActionResult EliminarUsuario(int? id)
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

        // POST: USUARIOs/Delete/5
        [HttpPost, ActionName("EliminarUsuario")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarUsuarioConfirmed(int id)
        {
            USUARIO uSUARIO = db.USUARIO.Find(id);
            db.USUARIO.Remove(uSUARIO);
            db.SaveChanges();
            return RedirectToAction("ListaUsuarios");
        }

        //Modulo de Funcionarios Públicos
        public ActionResult ListaFuncionarios()
        {
            return View(db.SERVIDORESPUBLICOS.ToList());
        }

        // GET: Funcionarios/Create
        public ActionResult AgregarFuncionario()
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
                sERVIDORESPUBLICOS.ESTATUS = "A";
                sERVIDORESPUBLICOS.IDUSUARIO = 1; //idUsuario; 

                db.SERVIDORESPUBLICOS.Add(sERVIDORESPUBLICOS);
                db.SaveChanges();
                return RedirectToAction("ListaFuncionarios");
            }

            ViewBag.IDDEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "IDDEPARTAMENTO", "NOMBREDEPTO", sERVIDORESPUBLICOS.IDDEPARTAMENTO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA", sERVIDORESPUBLICOS.IDUSUARIO);
            return View(sERVIDORESPUBLICOS);
        }

        // GET: Funcionarios/Edit/5
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
            return View(sERVIDORESPUBLICOS);
        }

        // POST: Funcionarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarFuncionario([Bind(Include = "IDSERVPUB,IDDEPARTAMENTO,IDUSUARIO,NOMBREPERSONAL,NOMBRAMIENTO,CONMUTADOR," +
            "EXTENSION,FOTOPERSONAL,CORREO,CURRICULUM,ESTATUS,FECHAREGISTRO,NIVEL")] SERVIDORESPUBLICOS sERVIDORESPUBLICOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sERVIDORESPUBLICOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaFuncionarios");
            }
            ViewBag.IDDEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "IDDEPARTAMENTO", "NOMBREDEPTO", sERVIDORESPUBLICOS.IDDEPARTAMENTO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "USUARIOINICIA", sERVIDORESPUBLICOS.IDUSUARIO);
            return View(sERVIDORESPUBLICOS);
        }
        // GET: Funcionarios/Edit/5
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

        // POST: Funcionarios/Edit/5
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
        // GET: Funcionarios/Edit/5
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

        // POST: Funcionarios/Edit/5
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

        // GET: Funcionarios/Details/5
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


    }
}