using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarServiceGame.Domain.Database
{
    public partial class CarServiceContext : DbContext
    {
        public virtual DbSet<Garage> Garage { get; set; }
        public virtual DbSet<RepairOrder> RepairOrder { get; set; }
        public virtual DbSet<RepairProcess> RepairProcess { get; set; }
        public virtual DbSet<Worker> Worker { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=145.239.86.224;User id = cargame; Password=Car0game; Database=CarServiceGame");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Garage>(entity =>
            {
                entity.Property(e => e.GarageId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<RepairOrder>(entity =>
            {
                entity.Property(e => e.RepairOrderId).ValueGeneratedNever();

                entity.Property(e => e.CarName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Reward).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<RepairProcess>(entity =>
            {
                entity.HasIndex(e => e.RepairOrderId)
                    .HasName("UQ__RepairPr__016C098FC898165A")
                    .IsUnique();

                entity.Property(e => e.RepairProcessId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Garage)
                    .WithMany(p => p.RepairProcess)
                    .HasForeignKey(d => d.GarageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RepairPro__Garag__440B1D61");

                entity.HasOne(d => d.RepairOrder)
                    .WithOne(p => p.RepairProcess)
                    .HasForeignKey<RepairProcess>(d => d.RepairOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RepairPro__Repai__45F365D3");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.RepairProcess)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RepairPro__Worke__44FF419A");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.Property(e => e.WorkerId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Garage)
                    .WithMany(p => p.Worker)
                    .HasForeignKey(d => d.GarageId)
                    .HasConstraintName("FK__Worker__GarageId__3D5E1FD2");
            });
        }
    }
}
