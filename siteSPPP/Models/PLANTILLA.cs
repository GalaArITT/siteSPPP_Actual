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
    
    public partial class PLANTILLA
    {
        public int IDPLANTILLA { get; set; }
        public Nullable<int> IDUSUARIO { get; set; }
        public Nullable<int> IDTIPO { get; set; }
        public string TITULO { get; set; }
        public string CONTENIDO { get; set; }
    
        public virtual TIPO_PLANTILLA TIPO_PLANTILLA { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
