using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace siteSPPP.Models
{
    //metadatos
    [MetadataType(typeof(USUARIO_Metadatos))]
    public partial class USUARIO{ }

    [MetadataType(typeof(NOTICIAS_SEPLAN_Metadatos))]
    public partial class NOTICIAS_SEPLAN { }

    [MetadataType(typeof(PLANTILLA_Metadatos))]
    public partial class PLANTILLA { }

    [MetadataType(typeof(TIPO_PLANTILLA_Metadatos))]
    public partial class TIPO_PLANTILLA { }

    [MetadataType(typeof(SERVIDORESPUBLICOS_Metadatos))]
    public partial class SERVIDORESPUBLICOS { }

    [MetadataType(typeof(DEPARTAMENTOS_Metadatos))]
    public partial class DEPARTAMENTOS { }
}