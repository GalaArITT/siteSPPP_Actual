//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace siteSPPP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SERVIDORESPUBLICOS
    {
        public int IDSERVPUB { get; set; }
        public Nullable<int> IDDEPARTAMENTO { get; set; }
        public Nullable<int> IDUSUARIO { get; set; }
        public string NOMBREPERSONAL { get; set; }
        public string NOMBRAMIENTO { get; set; }
        public string CONMUTADOR { get; set; }
        public string EXTENSION { get; set; }
        public byte[] FOTOPERSONAL { get; set; }
        public string CORREO { get; set; }
        public byte[] CURRICULUM { get; set; }
        public string ESTATUS { get; set; }
        public Nullable<System.DateTime> FECHAREGISTRO { get; set; }
        public Nullable<byte> NIVEL { get; set; }
    
        public virtual DEPARTAMENTOS DEPARTAMENTOS { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
