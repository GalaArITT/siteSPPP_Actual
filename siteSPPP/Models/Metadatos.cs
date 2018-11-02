using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace siteSPPP.Models
{
    public class NOTICIAS_SEPLAN_Metadatos
    {
        public int IDNOTICIA { get; set; }
        public Nullable<int> IDUSUARIO { get; set; }
        [Required]
        public string TITULO { get; set; }
        public string ENCABEZADO { get; set; }
        public Nullable<System.DateTime> FECHAPUBLIC { get; set; }
        public string RESPONSABLE { get; set; }
        public string BALAZO1 { get; set; }
        public string BALAZO2 { get; set; }
        public string BALAZO3 { get; set; }
        public string VER_COPLADENAY { get; set; }
        public string VER_SEPLAN { get; set; }
        public string VER_INTRANET { get; set; }
        public string LUGAR { get; set; }
        public Nullable<decimal> PRIORIDAD { get; set; }
        public string SOLO_MEDIOS { get; set; }
        public string PARTICIPANTES { get; set; }
        public string RESENIA { get; set; }
        public byte[] FOTO_PRINCIPAL { get; set; }
        public string PIE_FOTO_PRINCIPAL { get; set; }
        public byte[] FOTO_2 { get; set; }
        public string PIE_FOTO_2 { get; set; }
        public byte[] FOTO_3 { get; set; }
        public string PIE_FOTO_3 { get; set; }
        public byte[] FOTO_4 { get; set; }
        public string PIE_FOTO_4 { get; set; }
        public byte[] FOTO_5 { get; set; }
        public string PIE_FOTO_5 { get; set; }
        public byte[] FOTO_6 { get; set; }
        public string PIE_FOTO_6 { get; set; }

    }
    public class USUARIO_Metadatos
    {
        public int IDUSUARIO { get; set; }
        [Required]
        [Display(Name = "Nombre de usuario")]
        public string USUARIOINICIA { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string CONTRASENA { get; set; }
        public string NOMBREUSUARIO { get; set; }
        public Nullable<byte> ROL { get; set; }
        public string ESTATUS { get; set; }
    }
}