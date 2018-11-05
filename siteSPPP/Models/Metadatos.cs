using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace siteSPPP.Models
{
    public class USUARIO_Metadatos
    {
        public int IDUSUARIO { get; set; }
        [Required]
        [Display(Name = "Nombre de usuario")]
        public string USUARIOINICIA { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string CONTRASENA { get; set; }
        [Display(Name = "Nombre completo")]
        public string NOMBREUSUARIO { get; set; }
        [Display(Name = "Rol de usuario")]
        public Nullable<byte> ROL { get; set; }
        [Display(Name = "Estatus")]
        public string ESTATUS { get; set; }
        [Display(Name = "Fecha de registro")]
        public Nullable<System.DateTime> FECHAREGISTRO { get; set; }
    }
}