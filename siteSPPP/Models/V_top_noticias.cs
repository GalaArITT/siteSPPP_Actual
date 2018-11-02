namespace siteSPPP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SEPLAN.V_TOP_NOTICIAS")]
    public partial class V_top_noticias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        [Required]
        [StringLength(500)]
        public string TITULO { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short ID_NOTICIA { get; set; }

        public byte[] FOTO { get; set; }

        public short? R { get; set; }

    }
}