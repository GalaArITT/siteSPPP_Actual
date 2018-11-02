namespace siteSPPP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SEPLAN.NOTICIAS")]
    public partial class Noticias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Noticias()
        {
            Memoria_fotografica = new HashSet<Memoria_fotografica>();
        }

        public DateTime FECHA { get; set; }

        [Required]
        [StringLength(4000)]
        public string EVENTO { get; set; }

        [Required]
        [StringLength(500)]
        public string TITULO { get; set; }

        [Required]
        [StringLength(1000)]
        public string ENCABEZADO { get; set; }

        public DateTime FECHA_PUBLICACION { get; set; }

        [Required]
        [StringLength(150)]
        public string RESPONSABLE { get; set; }

        [StringLength(4000)]
        public string EVENTO2 { get; set; }

        [StringLength(200)]
        public string BALAZO1 { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short ID_NOTICIA { get; set; }

        public short? ID_FOTO { get; set; }

        [StringLength(1)]
        public string VER_COPLADENAY { get; set; }

        [StringLength(1)]
        public string VER_SEPLAN { get; set; }

        [StringLength(1)]
        public string VER_COCYTEN { get; set; }

        [StringLength(1)]
        public string VER_FOMIX { get; set; }

        [StringLength(1)]
        public string VER_INTRANET { get; set; }

        [StringLength(200)]
        public string BALAZO2 { get; set; }

        [StringLength(200)]
        public string BALAZO3 { get; set; }

        [StringLength(150)]
        public string LUGAR { get; set; }

        public byte? PRIORIDAD { get; set; }

        [StringLength(3000)]
        public string EVENTO3 { get; set; }

        [StringLength(1)]
        public string SOLO_MEDIOS { get; set; }

        [StringLength(300)]
        public string PARTICIPANTES { get; set; }

        [StringLength(400)]
        public string RESENIA { get; set; }

        [StringLength(1)]
        public string VER_DIF { get; set; }

        [StringLength(255)]
        public string USUARIO { get; set; }

        [StringLength(4000)]
        public string EVENTO4 { get; set; }

        public byte[] FOTO_PRINCIPAL { get; set; }

        [StringLength(300)]
        public string PIE_FOTO_PRINCIPAL { get; set; }

        public byte[] FOTO_2 { get; set; }

        [StringLength(300)]
        public string PIE_FOTO_2 { get; set; }

        public byte[] FOTO_3 { get; set; }

        [StringLength(300)]
        public string PIE_FOTO_3 { get; set; }

        public byte[] FOTO_4 { get; set; }

        [StringLength(300)]
        public string PIE_FOTO_4 { get; set; }

        public byte[] FOTO_5 { get; set; }

        [StringLength(300)]
        public string PIE_FOTO_5 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Memoria_fotografica> Memoria_fotografica { get; set; }
    }
}
