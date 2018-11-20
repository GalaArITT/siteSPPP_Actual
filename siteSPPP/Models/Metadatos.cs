using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace siteSPPP.Models
{
    [DataContract(IsReference = true)]
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

    public class NOTICIAS_SEPLAN_Metadatos{

        public int IDNOTICIA { get; set; }
        public Nullable<int> IDUSUARIO { get; set; }
        public string TITULO { get; set; }
        public string CONTENIDO { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de captura en sistema")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime FECHACAPTURA { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de publicación")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime FECHAPUBLIC { get; set; }
        public string LUGAR { get; set; }
        [Display(Name = "Prioridad de la nota")]
        public byte PRIORIDAD { get; set; }
        public string ESTATUS { get; set; }
    }

    public class PLANTILLA_Metadatos {

        public int IDPLANTILLA { get; set; }
        public Nullable<int> IDUSUARIO { get; set; }

        [Display(Name = "Plantilla asociada")]
        public Nullable<int> IDTIPO { get; set; }
        [Display(Name = "Titulo")]
        public string TITULO { get; set; }
        [Display(Name = "Contenido")]
        public string CONTENIDO { get; set; }

    }
    public class TIPO_PLANTILLA_Metadatos
    {
        [Display(Name = "Plantilla asociada")]
        public int IDTIPO { get; set; }
        [Display(Name = "Tipo de plantilla")]
        public string TIPOPLANTILLA { get; set; }
    }
    [DataContract(IsReference = true)]
    public class SERVIDORESPUBLICOS_Metadatos
    {
        public int IDSERVPUB { get; set; }
        [Display(Name = "Departamento")]
        public Nullable<int> IDDEPARTAMENTO { get; set; }
        [Display(Name = "Usuario que capturó")]
        public Nullable<int> IDUSUARIO { get; set; }
        [Display(Name = "Nombre Completo")]
        public string NOMBREPERSONAL { get; set; }
        [Display(Name = "Nombramiento")]
        public string NOMBRAMIENTO { get; set; }
        [Display(Name = "Conmutador")]
        public string CONMUTADOR { get; set; }
        [Display(Name = "Extensión")]
        public string EXTENSION { get; set; }
        [Display(Name = "Foto")]
        public byte[] FOTOPERSONAL { get; set; }
        [Display(Name = "Correo")]
        public string CORREO { get; set; }
        [Display(Name = "Curriculum")]
        public byte[] CURRICULUM { get; set; }
        [Display(Name = "Estatus")]
        public string ESTATUS { get; set; }
        [Display(Name = "Fecha registro")]
        public Nullable<System.DateTime> FECHAREGISTRO { get; set; }
        [Display(Name = "Nivel")]
        public Nullable<int> NIVEL { get; set; }
    }
    [DataContract(IsReference = true)]
    public class DEPARTAMENTOS_Metadatos
    {
        public int IDDEPARTAMENTO { get; set; }
        public string NOMBREDEPTO { get; set; }
    }

}