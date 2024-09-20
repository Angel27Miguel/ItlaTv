using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Productora> Productoras { get; set; }
        public DbSet<Series> Series { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Tables
            modelBuilder.Entity<Genero>().ToTable("Generos");
            modelBuilder.Entity<Productora>().ToTable("Productoras");
            modelBuilder.Entity<Series>().ToTable("Series");
            #endregion

            #region Primary Keys
            modelBuilder.Entity<Genero>().HasKey(genero => genero.Id);
            modelBuilder.Entity<Productora>().HasKey(productora => productora.Id);
            modelBuilder.Entity<Series>().HasKey(series => series.Id);
            #endregion

            #region Relationships
            modelBuilder.Entity<Series>()
                .HasOne(series => series.Productora)
                .WithMany(productora => productora.Series)
                .HasForeignKey(series => series.ProductoraId);

            modelBuilder.Entity<Series>()
        .HasOne(series => series.GeneroPrimario)
        .WithMany(genero => genero.Series)
        .HasForeignKey(series => series.GeneroPrimarioId)
        .OnDelete(DeleteBehavior.Restrict); // Puedes ajustar el comportamiento de eliminación según tus necesidades

            // Configuración para el género secundario
            modelBuilder.Entity<Series>()
                .HasOne(series => series.GeneroSecundario)
                .WithMany()
                .HasForeignKey(series => series.GeneroSecundarioId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Properties configuration
            modelBuilder.Entity<Series>()
                .Property(series => series.ImagenPortada).HasMaxLength(250);

            modelBuilder.Entity<Series>()
                .Property(series => series.EnlaceVideo).HasMaxLength(250);
            #endregion
        }
    }
}
