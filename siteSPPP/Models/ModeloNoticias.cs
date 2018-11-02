namespace siteSPPP.Models
{
    using System.Data.Entity;

    public partial class ModeloNoticias : DbContext
    {
        public ModeloNoticias()
            : base("name=ModeloNoticias")
        {
        }

        public virtual DbSet<Memoria_fotografica> Memoria_fotografica { get; set; }
        public virtual DbSet<Noticias> Noticias { get; set; }
        public virtual DbSet<V_top_noticias> V_top_noticias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Memoria_fotografica>()
                .Property(e => e.PIE_FOTO)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.EVENTO)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.TITULO)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.ENCABEZADO)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.RESPONSABLE)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.EVENTO2)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.BALAZO1)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.VER_COPLADENAY)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.VER_SEPLAN)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.VER_COCYTEN)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.VER_FOMIX)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.VER_INTRANET)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.BALAZO2)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.BALAZO3)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.LUGAR)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.EVENTO3)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.SOLO_MEDIOS)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.PARTICIPANTES)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.RESENIA)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.VER_DIF)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.USUARIO)
                .IsUnicode(false);

            modelBuilder.Entity<Noticias>()
                .Property(e => e.EVENTO4)
                .IsUnicode(false);

            //modelBuilder.Entity<Noticias>()
            //    .HasMany(e => e.Memoria_fotografica)
            //    .WithRequired(e => e.Noticias)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<V_top_noticias>()
                .Property(e => e.ID_NOTICIA);

            modelBuilder.Entity<V_top_noticias>()
                .Property(e => e.TITULO)
                .IsUnicode(false);

            modelBuilder.Entity<V_top_noticias>()
                .Property(e => e.FOTO);

            modelBuilder.Entity<V_top_noticias>()
                .Property(e => e.R);
        }
    }
}
