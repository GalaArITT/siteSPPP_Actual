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
        public ActionResult CrearUsuario([Bind(Include = "IDUSUARIO,USUARIOINICIA,CONTRASENA,NOMBREUSUARIO,ROL,ESTATUS,FECHAREGISTRO")]
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
        /*
        // GET: USUARIOs/Edit/5
        public ActionResult Edit(int? id)
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
        public ActionResult Edit([Bind(Include = "IDUSUARIO,USUARIOINICIA,CONTRASENA,NOMBREUSUARIO,ROL,ESTATUS,FECHAREGISTRO")] USUARIO uSUARIO)
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
        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            USUARIO uSUARIO = db.USUARIO.Find(id);
            db.USUARIO.Remove(uSUARIO);
            db.SaveChanges();
            return RedirectToAction("ListaUsuarios");
        }*/
    }
}