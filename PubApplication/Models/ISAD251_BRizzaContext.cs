using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PubApplication.Models
{
    public partial class ISAD251_BRizzaContext : DbContext
    {
        public ISAD251_BRizzaContext()
        {
        }

        public ISAD251_BRizzaContext(DbContextOptions<ISAD251_BRizzaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PubItems> PubItems { get; set; }
        public virtual DbSet<PubOrderItems> PubOrderItems { get; set; }
        public virtual DbSet<PubOrders> PubOrders { get; set; }
        public virtual DbSet<PubUsers> PubUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=socem1.uopnet.plymouth.ac.uk;Database=ISAD251_BRizza;User Id=BRizza; Password=ISAD251_22215888");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PubItems>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.Property(e => e.ItemId)
                    .HasColumnName("ItemID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ItemPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ItemType)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PubOrderItems>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ItemId });

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.PubOrderItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("ItemIDFK");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.PubOrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("OrderIDFK");
            });

            modelBuilder.Entity<PubOrders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PubOrders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("UserIDFK");
            });

            modelBuilder.Entity<PubUsers>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserFirstName).HasMaxLength(60);

                entity.Property(e => e.UserLastName).HasMaxLength(80);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
