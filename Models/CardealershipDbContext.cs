using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace capestone_CreateAPI.Models
{
    public partial class CardealershipDbContext : DbContext
    {
        public CardealershipDbContext()
        {
        }

        public CardealershipDbContext(DbContextOptions<CardealershipDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CardealershipDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(100);

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasColumnName("make")
                    .HasMaxLength(100);

                entity.Property(e => e.Model)
                    .HasColumnName("model")
                    .HasMaxLength(100);

                entity.Property(e => e.YearMade)
                    .HasColumnName("yearMade")
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
