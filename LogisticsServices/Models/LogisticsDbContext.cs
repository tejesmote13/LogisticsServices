using System;
using System.Collections.Generic;
using LogisticsServices.Repositories.Order;
using Microsoft.EntityFrameworkCore;

namespace LogisticsServices.Models;

public partial class LogisticsDbContext : DbContext
{
    public LogisticsDbContext()
    {
    }

    public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrier> Carriers { get; set; }

    public virtual DbSet<CarrierRep> CarrierReps { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerRep> CustomerReps { get; set; }

    public virtual DbSet<Equipment> Equipments { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PendingOrder> PendingOrders { get; set; }

    public virtual DbSet<Zip> Zips { get; set; }
    public virtual DbSet<OrderDTO> OrderDTO { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source =(localdb)\\MSSQLLocalDB;Initial Catalog=LogisticsDB;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrier>(entity =>
        {
            entity.HasKey(e => e.CarrierId).HasName("PK__Carriers__CB8205594BC46448");

            entity.ToTable("Carriers", "wrk");

            entity.HasIndex(e => e.UserId, "UQ__Carriers__1788CC4DE67996C0").IsUnique();

            entity.HasIndex(e => e.EmailId, "UQ__Carriers__7ED91ACE303E92FA").IsUnique();

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Zip).WithMany(p => p.Carriers)
                .HasForeignKey(d => d.ZipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ZipId_Carrier");
        });

        modelBuilder.Entity<CarrierRep>(entity =>
        {
            entity.HasKey(e => e.CarrierRepId).HasName("PK__CarrierR__8465DD733CFA6C85");

            entity.ToTable("CarrierRep", "wrk");

            entity.HasIndex(e => e.UserId, "UQ__CarrierR__1788CC4D0492FBB5").IsUnique();

            entity.HasIndex(e => e.EmailId, "UQ__CarrierR__7ED91ACEBE52ED4A").IsUnique();

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Carrier).WithMany(p => p.CarrierReps)
                .HasForeignKey(d => d.CarrierId)
                .HasConstraintName("FK_CarrierId_CarrierRep");

            entity.HasOne(d => d.Zip).WithMany(p => p.CarrierReps)
                .HasForeignKey(d => d.ZipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ZipId_CarrierRep");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D873829966");

            entity.ToTable("Customers", "wrk");

            entity.HasIndex(e => e.UserId, "UQ__Customer__1788CC4DF821CAD6").IsUnique();

            entity.HasIndex(e => e.EmailId, "UQ__Customer__7ED91ACE1F350F0B").IsUnique();

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Zip).WithMany(p => p.Customers)
                .HasForeignKey(d => d.ZipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ZipId_Customer");
        });

        modelBuilder.Entity<CustomerRep>(entity =>
        {
            entity.HasKey(e => e.CustomerRepId).HasName("PK__Customer__E3B15D65336412FE");

            entity.ToTable("CustomerRep", "wrk");

            entity.HasIndex(e => e.UserId, "UQ__Customer__1788CC4D7C48F0D4").IsUnique();

            entity.HasIndex(e => e.EmailId, "UQ__Customer__7ED91ACE00414058").IsUnique();

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerReps)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CustomerId_CustomerRep");

            entity.HasOne(d => d.Zip).WithMany(p => p.CustomerReps)
                .HasForeignKey(d => d.ZipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ZipId_CustomerRep");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("PK__Equipmen__344744797DB5A91D");

            entity.ToTable("Equipments", "lkp");

            entity.Property(e => e.EquipmentCategory)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EquipmentName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCFF17BB606");

            entity.ToTable("Orders", "wrk");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.PickUpDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Carrier).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CarrierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarrierId_Orders");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerId_Orders");

            entity.HasOne(d => d.DestinationZip).WithMany(p => p.OrderDestinationZips)
                .HasForeignKey(d => d.DestinationZipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DestinationZipId_Orders");

            entity.HasOne(d => d.Equipment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EquipmentId_Orders");

            entity.HasOne(d => d.OriginZip).WithMany(p => p.OrderOriginZips)
                .HasForeignKey(d => d.OriginZipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OriginZipId_Orders");
        });

        modelBuilder.Entity<PendingOrder>(entity =>
        {
            entity.HasKey(e => e.PendingOrderId).HasName("PK__PendingO__7205156F54942D07");

            entity.ToTable("PendingOrders", "wrk");

            entity.Property(e => e.IsOrderCreated).HasDefaultValueSql("((0))");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.PickUpDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Carrier).WithMany(p => p.PendingOrders)
                .HasForeignKey(d => d.CarrierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarrierId_PendingOrders");

            entity.HasOne(d => d.Customer).WithMany(p => p.PendingOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerId_PendingOrders");

            entity.HasOne(d => d.DestinationZip).WithMany(p => p.PendingOrderDestinationZips)
                .HasForeignKey(d => d.DestinationZipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DestinationZipId_PendingOrders");

            entity.HasOne(d => d.Equipment).WithMany(p => p.PendingOrders)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EquipmentId_PendingOrders");

            entity.HasOne(d => d.OriginZip).WithMany(p => p.PendingOrderOriginZips)
                .HasForeignKey(d => d.OriginZipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OriginZipId_PendingOrders");
        });

        modelBuilder.Entity<Zip>(entity =>
        {
            entity.HasKey(e => e.ZipId).HasName("PK__Zips__A5C55257C081F15B");

            entity.ToTable("Zips", "lkp");

            entity.Property(e => e.ZipId).ValueGeneratedNever();
            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Latitude).HasColumnType("decimal(10, 8)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(11, 8)");
            entity.Property(e => e.State)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
