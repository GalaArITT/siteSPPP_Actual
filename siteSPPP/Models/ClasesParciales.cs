using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace siteSPPP.Models
{
    public class ClasesParciales
    {
        //NOTICIAS_SEPLAN
        [MetadataType(typeof(NOTICIAS_SEPLAN_Metadatos))]
        public partial class NOTICIAS_SEPLAN { }
    }
}