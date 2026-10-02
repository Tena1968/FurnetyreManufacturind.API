using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FurnetureManufacturind.API.Models;

public partial class FurnitureManufacturingContext : DbContext
{
    public FurnitureManufacturingContext()
    {
    }

    public FurnitureManufacturingContext(DbContextOptions<FurnitureManufacturingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contractor> Contractors { get; set; }

    public virtual DbSet<ContractorType> ContractorTypes { get; set; }

    public virtual DbSet<CostCalculation> CostCalculations { get; set; }

    public virtual DbSet<CostCalculationMaterial> CostCalculationMaterials { get; set; }

    public virtual DbSet<CostCalculationOperation> CostCalculationOperations { get; set; }

    public virtual DbSet<CustomerOrder> CustomerOrders { get; set; }

    public virtual DbSet<CustomerOrderItem> CustomerOrderItems { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Nomenclature> Nomenclatures { get; set; }

    public virtual DbSet<NomenclatureType> NomenclatureTypes { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<ProductionOrder> ProductionOrders { get; set; }

    public virtual DbSet<ProductionOrderMaterial> ProductionOrderMaterials { get; set; }

    public virtual DbSet<ProductionOrderOperation> ProductionOrderOperations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Specification> Specifications { get; set; }

    public virtual DbSet<SpecificationComponent> SpecificationComponents { get; set; }

    public virtual DbSet<SpecificationOperation> SpecificationOperations { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-EK55CGM\\SQLEXPRESS;Database=FurnitureManufacturing;TrustServerCertificate=true;Trusted_Connection=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contractor>(entity =>
        {
            entity.HasKey(e => e.ContractorId).HasName("PK__Contract__E964EB5D75C72E78");

            entity.HasIndex(e => e.ContractorCode, "UQ__Contract__356855329C930B9D").IsUnique();

            entity.Property(e => e.ContractorId).HasColumnName("ContractorID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.ContractorCode).HasMaxLength(20);
            entity.Property(e => e.ContractorName).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Inn)
                .HasMaxLength(20)
                .HasColumnName("INN");
            entity.Property(e => e.Phone).HasMaxLength(20);

            entity.HasMany(d => d.ContractorTypes).WithMany(p => p.Contractors)
                .UsingEntity<Dictionary<string, object>>(
                    "ContractorTypeLink",
                    r => r.HasOne<ContractorType>().WithMany()
                        .HasForeignKey("ContractorTypeId")
                        .HasConstraintName("FK_CTL_ContractorTypes"),
                    l => l.HasOne<Contractor>().WithMany()
                        .HasForeignKey("ContractorId")
                        .HasConstraintName("FK_CTL_Contractors"),
                    j =>
                    {
                        j.HasKey("ContractorId", "ContractorTypeId");
                        j.ToTable("ContractorTypeLinks");
                        j.IndexerProperty<int>("ContractorId").HasColumnName("ContractorID");
                        j.IndexerProperty<int>("ContractorTypeId").HasColumnName("ContractorTypeID");
                    });
        });

        modelBuilder.Entity<ContractorType>(entity =>
        {
            entity.HasKey(e => e.ContractorTypeId).HasName("PK__Contract__ED890E6D65052EBB");

            entity.HasIndex(e => e.TypeName, "UQ__Contract__D4E7DFA8B159F239").IsUnique();

            entity.Property(e => e.ContractorTypeId).HasColumnName("ContractorTypeID");
            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<CostCalculation>(entity =>
        {
            entity.HasKey(e => e.CostCalculationId).HasName("PK__CostCalc__75B2A8052C308761");

            entity.HasIndex(e => e.NomenclatureId, "IX_CC_Nom");

            entity.Property(e => e.CostCalculationId).HasColumnName("CostCalculationID");
            entity.Property(e => e.CalculationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.NomenclatureId).HasColumnName("NomenclatureID");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Nomenclature).WithMany(p => p.CostCalculations)
                .HasForeignKey(d => d.NomenclatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CC_Nom");
        });

        modelBuilder.Entity<CostCalculationMaterial>(entity =>
        {
            entity.HasKey(e => e.CostCalculationMaterialId).HasName("PK__CostCalc__BFC626DB96B45AFF");

            entity.Property(e => e.CostCalculationMaterialId).HasColumnName("CostCalculationMaterialID");
            entity.Property(e => e.CostCalculationId).HasColumnName("CostCalculationID");
            entity.Property(e => e.NomenclatureId).HasColumnName("NomenclatureID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CostCalculation).WithMany(p => p.CostCalculationMaterials)
                .HasForeignKey(d => d.CostCalculationId)
                .HasConstraintName("FK_CCM_CC");

            entity.HasOne(d => d.Nomenclature).WithMany(p => p.CostCalculationMaterials)
                .HasForeignKey(d => d.NomenclatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CCM_Nom");
        });

        modelBuilder.Entity<CostCalculationOperation>(entity =>
        {
            entity.HasKey(e => e.CostCalculationOperationId).HasName("PK__CostCalc__F43DC7169255E6C3");

            entity.Property(e => e.CostCalculationOperationId).HasColumnName("CostCalculationOperationID");
            entity.Property(e => e.CostCalculationId).HasColumnName("CostCalculationID");
            entity.Property(e => e.NomenclatureId).HasColumnName("NomenclatureID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CostCalculation).WithMany(p => p.CostCalculationOperations)
                .HasForeignKey(d => d.CostCalculationId)
                .HasConstraintName("FK_CCO_CC");

            entity.HasOne(d => d.Nomenclature).WithMany(p => p.CostCalculationOperations)
                .HasForeignKey(d => d.NomenclatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CCO_Nom");
        });

        modelBuilder.Entity<CustomerOrder>(entity =>
        {
            entity.HasKey(e => e.CustomerOrderId).HasName("PK__Customer__28FBA0DC3D694E38");

            entity.HasIndex(e => e.ContractorId, "IX_CO_Contr");

            entity.HasIndex(e => e.OrderDate, "IX_CO_Date");

            entity.Property(e => e.CustomerOrderId).HasColumnName("CustomerOrderID");
            entity.Property(e => e.ContractorId).HasColumnName("ContractorID");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");
            entity.Property(e => e.OrderNumber).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Contractor).WithMany(p => p.CustomerOrders)
                .HasForeignKey(d => d.ContractorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CO_Contr");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.CustomerOrders)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CO_Manuf");
        });

        modelBuilder.Entity<CustomerOrderItem>(entity =>
        {
            entity.HasKey(e => e.CustomerOrderItemId).HasName("PK__Customer__AC476F51EFB0C986");

            entity.Property(e => e.CustomerOrderItemId).HasColumnName("CustomerOrderItemID");
            entity.Property(e => e.CustomerOrderId).HasColumnName("CustomerOrderID");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NomenclatureId).HasColumnName("NomenclatureID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CustomerOrder).WithMany(p => p.CustomerOrderItems)
                .HasForeignKey(d => d.CustomerOrderId)
                .HasConstraintName("FK_COI_CO");

            entity.HasOne(d => d.Nomenclature).WithMany(p => p.CustomerOrderItems)
                .HasForeignKey(d => d.NomenclatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COI_Nom");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCDA9FE5225");

            entity.HasIndex(e => e.DepartmentName, "UQ__Departme__D949CC34059D500C").IsUnique();

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(255);
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("PK__Manufact__357E5CA1BDD0B71C");

            entity.HasIndex(e => e.ManufacturerName, "UQ__Manufact__3B9CDE2E60395020").IsUnique();

            entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");
            entity.Property(e => e.ManufacturerName).HasMaxLength(255);
        });

        modelBuilder.Entity<Nomenclature>(entity =>
        {
            entity.HasKey(e => e.NomenclatureId).HasName("PK__Nomencla__45D98DA15FE13764");

            entity.HasIndex(e => e.NomenclatureTypeId, "IX_Nom_Type");

            entity.HasIndex(e => e.UnitId, "IX_Nom_Unit");

            entity.HasIndex(e => e.NomenclatureCode, "UQ__Nomencla__5BD5D156B82CDF63").IsUnique();

            entity.Property(e => e.NomenclatureId).HasColumnName("NomenclatureID");
            entity.Property(e => e.NomenclatureCode).HasMaxLength(20);
            entity.Property(e => e.NomenclatureName).HasMaxLength(255);
            entity.Property(e => e.NomenclatureTypeId).HasColumnName("NomenclatureTypeID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.NomenclatureType).WithMany(p => p.Nomenclatures)
                .HasForeignKey(d => d.NomenclatureTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nom_NomTypes");

            entity.HasOne(d => d.Unit).WithMany(p => p.Nomenclatures)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nom_Units");
        });

        modelBuilder.Entity<NomenclatureType>(entity =>
        {
            entity.HasKey(e => e.NomenclatureTypeId).HasName("PK__Nomencla__BFB5B4A4CE30EF00");

            entity.HasIndex(e => e.TypeName, "UQ__Nomencla__D4E7DFA891089258").IsUnique();

            entity.Property(e => e.NomenclatureTypeId).HasColumnName("NomenclatureTypeID");
            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.NotesId);

            entity.Property(e => e.NotesId).HasColumnName("NotesID");
            entity.Property(e => e.Content).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notes_Users");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__Prices__4957584F419089F5");

            entity.HasIndex(e => e.NomenclatureId, "IX_Prices_Nom");

            entity.Property(e => e.PriceId).HasColumnName("PriceID");
            entity.Property(e => e.EffectiveDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.NomenclatureId).HasColumnName("NomenclatureID");
            entity.Property(e => e.Price1)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Price");

            entity.HasOne(d => d.Nomenclature).WithMany(p => p.Prices)
                .HasForeignKey(d => d.NomenclatureId)
                .HasConstraintName("FK_Prices_Nom");
        });

        modelBuilder.Entity<ProductionOrder>(entity =>
        {
            entity.HasKey(e => e.ProductionOrderId).HasName("PK__Producti__E86155F05CA97554");

            entity.HasIndex(e => e.NomenclatureId, "IX_PO_Nom");

            entity.HasIndex(e => e.StartDate, "IX_PO_StartDate");

            entity.Property(e => e.ProductionOrderId).HasColumnName("ProductionOrderID");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.ExecutorId).HasColumnName("ExecutorID");
            entity.Property(e => e.NomenclatureId).HasColumnName("NomenclatureID");
            entity.Property(e => e.OrderNumber).HasMaxLength(50);
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

            entity.HasOne(d => d.Department).WithMany(p => p.ProductionOrders)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PO_Dept");

            entity.HasOne(d => d.Executor).WithMany(p => p.ProductionOrders)
                .HasForeignKey(d => d.ExecutorId)
                .HasConstraintName("FK_ProductionOrders_Users");

            entity.HasOne(d => d.Nomenclature).WithMany(p => p.ProductionOrders)
                .HasForeignKey(d => d.NomenclatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PO_Nom");
        });

        modelBuilder.Entity<ProductionOrderMaterial>(entity =>
        {
            entity.HasKey(e => e.ProductionOrderMaterialId).HasName("PK__Producti__A500C09E06D59417");

            entity.Property(e => e.ProductionOrderMaterialId).HasColumnName("ProductionOrderMaterialID");
            entity.Property(e => e.NomenclatureId).HasColumnName("NomenclatureID");
            entity.Property(e => e.ProductionOrderId).HasColumnName("ProductionOrderID");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

            entity.HasOne(d => d.Nomenclature).WithMany(p => p.ProductionOrderMaterials)
                .HasForeignKey(d => d.NomenclatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_POM_Nom");

            entity.HasOne(d => d.ProductionOrder).WithMany(p => p.ProductionOrderMaterials)
                .HasForeignKey(d => d.ProductionOrderId)
                .HasConstraintName("FK_POM_PO");
        });

        modelBuilder.Entity<ProductionOrderOperation>(entity =>
        {
            entity.HasKey(e => e.ProductionOrderOperationId).HasName("PK__Producti__2F3D0CD57A2C2701");

            entity.Property(e => e.ProductionOrderOperationId).HasColumnName("ProductionOrderOperationID");
            entity.Property(e => e.NomenclatureId).HasColumnName("NomenclatureID");
            entity.Property(e => e.ProductionOrderId).HasColumnName("ProductionOrderID");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Nomenclature).WithMany(p => p.ProductionOrderOperations)
                .HasForeignKey(d => d.NomenclatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_POO_Nom");

            entity.HasOne(d => d.ProductionOrder).WithMany(p => p.ProductionOrderOperations)
                .HasForeignKey(d => d.ProductionOrderId)
                .HasConstraintName("FK_POO_PO");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3AEF9C9A53");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160E941EC17").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Specification>(entity =>
        {
            entity.HasKey(e => e.SpecificationId).HasName("PK__Specific__A384CC1D257576CC");

            entity.Property(e => e.SpecificationId).HasColumnName("SpecificationID");
            entity.Property(e => e.ApprovedBy).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");
            entity.Property(e => e.NomenclatureId).HasColumnName("NomenclatureID");
            entity.Property(e => e.SpecificationNumber).HasMaxLength(50);

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Specifications)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Spec_Manuf");

            entity.HasOne(d => d.Nomenclature).WithMany(p => p.Specifications)
                .HasForeignKey(d => d.NomenclatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Spec_Nom");
        });

        modelBuilder.Entity<SpecificationComponent>(entity =>
        {
            entity.HasKey(e => e.SpecificationComponentId).HasName("PK__Specific__0639FDEAD1341460");

            entity.Property(e => e.SpecificationComponentId).HasColumnName("SpecificationComponentID");
            entity.Property(e => e.NomenclatureId).HasColumnName("NomenclatureID");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.SpecificationId).HasColumnName("SpecificationID");

            entity.HasOne(d => d.Nomenclature).WithMany(p => p.SpecificationComponents)
                .HasForeignKey(d => d.NomenclatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SC_Nom");

            entity.HasOne(d => d.Specification).WithMany(p => p.SpecificationComponents)
                .HasForeignKey(d => d.SpecificationId)
                .HasConstraintName("FK_SC_Spec");
        });

        modelBuilder.Entity<SpecificationOperation>(entity =>
        {
            entity.HasKey(e => e.SpecificationOperationId).HasName("PK__Specific__9DB8D9D81A455C09");

            entity.Property(e => e.SpecificationOperationId).HasColumnName("SpecificationOperationID");
            entity.Property(e => e.NomenclatureId).HasColumnName("NomenclatureID");
            entity.Property(e => e.SpecificationId).HasColumnName("SpecificationID");
            entity.Property(e => e.TimeNorm).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Nomenclature).WithMany(p => p.SpecificationOperations)
                .HasForeignKey(d => d.NomenclatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SO_Nom");

            entity.HasOne(d => d.Specification).WithMany(p => p.SpecificationOperations)
                .HasForeignKey(d => d.SpecificationId)
                .HasConstraintName("FK_SO_Spec");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__Units__44F5EC9583489915");

            entity.HasIndex(e => e.UnitCode, "UQ__Units__0665E6D9ECBB830B").IsUnique();

            entity.HasIndex(e => e.UnitName, "UQ__Units__B5EE667806137E7D").IsUnique();

            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UnitCode).HasMaxLength(10);
            entity.Property(e => e.UnitName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC28376615");

            entity.HasIndex(e => e.Login, "UQ__Users__5E55825B2C2BCF35").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
