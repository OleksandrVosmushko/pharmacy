using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace courseDb.Models
{
    public partial class pharmacyContext : DbContext
    {
        public pharmacyContext()
        {
        }

        public pharmacyContext(DbContextOptions<pharmacyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Availability> Availability { get; set; }
        public virtual DbSet<Maker> Maker { get; set; }
        public virtual DbSet<Medicine> Medicine { get; set; }
        public virtual DbSet<Pharmacist> Pharmacist { get; set; }
        public virtual DbSet<Receipt> Receipt { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=pharmacy;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Availability>(entity =>
            {
                entity.ToTable("availability");

                entity.Property(e => e.Availabilityid)
                    .HasColumnName("availabilityid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Medicineid).HasColumnName("medicineid");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Productiontime)
                    .HasColumnName("productiontime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.Availability)
                    .HasForeignKey(d => d.Medicineid)
                    .HasConstraintName("availability_medicineid_fkey");
            });

            modelBuilder.Entity<Maker>(entity =>
            {
                entity.ToTable("maker");

                entity.Property(e => e.Makerid)
                    .HasColumnName("makerid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Makername)
                    .HasColumnName("makername")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.ToTable("medicine");

                entity.Property(e => e.Medicineid)
                    .HasColumnName("medicineid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Makerid).HasColumnName("makerid");

                entity.Property(e => e.Measurement)
                    .HasColumnName("measurement")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Maker)
                    .WithMany(p => p.Medicine)
                    .HasForeignKey(d => d.Makerid)
                    .HasConstraintName("medicine_makerid_fkey");
            });

            modelBuilder.Entity<Pharmacist>(entity =>
            {
                entity.ToTable("pharmacist");

                entity.Property(e => e.Pharmacistid)
                    .HasColumnName("pharmacistid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("datetime");

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Salary).HasColumnName("salary");
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.ToTable("receipt");

                entity.Property(e => e.Receiptid)
                    .HasColumnName("receiptid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Medicineid).HasColumnName("medicineid");

                entity.Property(e => e.Pharmacistid).HasColumnName("pharmacistid");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.Receipt)
                    .HasForeignKey(d => d.Medicineid)
                    .HasConstraintName("receipt_medicineid_fkey");

                entity.HasOne(d => d.Pharmacist)
                    .WithMany(p => p.Receipt)
                    .HasForeignKey(d => d.Pharmacistid)
                    .HasConstraintName("receipt_pharmacistid_fkey");
            });
        }
    }
}
