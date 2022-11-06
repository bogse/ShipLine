using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ShipLine.Models.DBObjects;
using Route = ShipLine.Models.DBObjects.Route;

namespace ShipLine.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Port> Ports { get; set; } = null!;
        public virtual DbSet<Route> Routes { get; set; } = null!;
        public virtual DbSet<Ship> Ships { get; set; } = null!;
        public virtual DbSet<Shipment> Shipments { get; set; } = null!;
        public virtual DbSet<Voyage> Voyages { get; set; } = null!;
        public virtual DbSet<VoyageShipment> VoyageShipments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-LEOOKSG;Initial Catalog=ShipLine;Integrated Security=True;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.ClientId).ValueGeneratedNever();

                entity.Property(e => e.ClientName).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(15);
            });

            modelBuilder.Entity<Port>(entity =>
            {
                entity.ToTable("Port");

                entity.Property(e => e.PortId)
                    .ValueGeneratedNever()
                    .HasColumnName("PortID");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("Route");

                entity.Property(e => e.RouteId)
                    .ValueGeneratedNever()
                    .HasColumnName("RouteID");

                entity.Property(e => e.DestinationPortId).HasColumnName("DestinationPortID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.SourcePortId).HasColumnName("SourcePortID");

                entity.HasOne(d => d.DestinationPort)
                    .WithMany(p => p.RouteDestinationPorts)
                    .HasForeignKey(d => d.DestinationPortId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Route_Port1");

                entity.HasOne(d => d.SourcePort)
                    .WithMany(p => p.RouteSourcePorts)
                    .HasForeignKey(d => d.SourcePortId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Route_Port");
            });

            modelBuilder.Entity<Ship>(entity =>
            {
                entity.ToTable("Ship");

                entity.Property(e => e.ShipId)
                    .ValueGeneratedNever()
                    .HasColumnName("ShipID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("Shipment");

                entity.Property(e => e.ShipmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("ShipmentID");

                entity.Property(e => e.CargoContents).HasMaxLength(50);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.NeedByDate).HasColumnType("datetime");

                entity.Property(e => e.QuantityTeq).HasColumnName("QuantityTEQ");

                entity.Property(e => e.ShipRequestDate).HasColumnType("datetime");

                entity.Property(e => e.ShipmentNumber).ValueGeneratedOnAdd();

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shipment_Client");

                entity.HasOne(d => d.DestinationPort)
                    .WithMany(p => p.ShipmentDestinationPorts)
                    .HasForeignKey(d => d.DestinationPortId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipmentDestination_Port");

                entity.HasOne(d => d.SourcePort)
                    .WithMany(p => p.ShipmentSourcePorts)
                    .HasForeignKey(d => d.SourcePortId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipmentSourcePort_Port");
            });

            modelBuilder.Entity<Voyage>(entity =>
            {
                entity.ToTable("Voyage");

                entity.Property(e => e.VoyageId)
                    .ValueGeneratedNever()
                    .HasColumnName("VoyageID");

                entity.Property(e => e.CostPerTeq).HasColumnName("CostPerTEQ");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.RouteId).HasColumnName("RouteID");

                entity.Property(e => e.ShipId).HasColumnName("ShipID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Voyages)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voyage_Route");

                entity.HasOne(d => d.Ship)
                    .WithMany(p => p.Voyages)
                    .HasForeignKey(d => d.ShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voyage_Ship");
            });

            modelBuilder.Entity<VoyageShipment>(entity =>
            {
                entity.ToTable("VoyageShipment");

                entity.Property(e => e.VoyageShipmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("VoyageShipmentID");

                entity.Property(e => e.ShipmentId).HasColumnName("ShipmentID");

                entity.Property(e => e.VoyageId).HasColumnName("VoyageID");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.VoyageShipments)
                    .HasForeignKey(d => d.ShipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoyageShipment_Shipment");

                entity.HasOne(d => d.Voyage)
                    .WithMany(p => p.VoyageShipments)
                    .HasForeignKey(d => d.VoyageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoyageShipment_Voyage");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
