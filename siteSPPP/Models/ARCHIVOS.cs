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
    
    public partial class ARCHIVOS
    {
        public int IDARCHIVO { get; set; }
        public int IDTIPO { get; set; }
        public string NOMBREARCHIVO { get; set; }
        public byte[] ARCHIVO { get; set; }
        public byte[] IMAGENARCHIVO { get; set; }
        public System.DateTime FECHA { get; set; }
        public string ESTATUS { get; set; }
    
        public virtual TIPO_PLANTILLA TIPO_PLANTILLA { get; set; }
    }
}
