using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using DTO.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DTO.Data.Cnotext;

public partial class EddbAppContext : DbContext
{
    public EddbAppContext()
    {
    }

    public EddbAppContext(DbContextOptions<EddbAppContext> options)
        : base(options)
    {

        

    }

    public virtual DbSet<CompanyAuditLog> CompanyAuditLogs { get; set; }

    public virtual DbSet<CompanyDet> CompanyDets { get; set; }
    
    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    // GetDrugsData View for consolidated drug and company information
    public virtual DbSet<GetDrugsData> GetDrugsDataView { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyAuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__CompanyA__A17F23989A0C2073");

            entity.Property(e => e.PerformedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07A2C1E46F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07927E5111");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRole__RoleId__7FEAFD3E"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRole__UserId__7EF6D905"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760AD2B0787BD");
                        j.ToTable("UserRole");
                    });
        });

        // Configure GetDrugsData view
        modelBuilder.Entity<GetDrugsData>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("GetDrugsData");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
