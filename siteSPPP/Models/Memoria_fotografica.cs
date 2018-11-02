namespace siteSPPP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SEPLAN.V_FOTOS")]
    public partial class Memoria_fotografica
    {
        //public short ID_FOTO { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short ID_NOTICIA { get; set; }

        public byte[] FOTO { get; set; }

        [StringLength(300)]
        public string PIE_FOTO { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte NUM_FOTO { get; set; }

        public virtual Noticias Noticias { get; set; }
    }
}
