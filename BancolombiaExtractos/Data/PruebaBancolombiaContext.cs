using System;
using System.Collections.Generic;
using BancolombiaExtractos.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BancolombiaExtractos.Data;

public partial class PruebaBancolombiaContext : DbContext
{
    public PruebaBancolombiaContext()
    {
    }

    public PruebaBancolombiaContext(DbContextOptions<PruebaBancolombiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:bancolombia");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.NumeroCuenta).HasName("PK__cuentas__C6B74B89AED3B7F0");

            entity.Property(e => e.NumeroCuenta).ValueGeneratedNever();
            entity.Property(e => e.Saldo).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Cuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cuentas__usuario__3B75D760");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__movimien__3213E83F1A541F16");

            entity.ToTable("movimientos", tb => tb.HasTrigger("TR_movimientos_actualizar_saldo_after_insert"));

            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.NumeroCuentaNavigation).WithMany(p => p.Movimientos).HasConstraintName("FK__movimient__numer__3F466844");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3213E83F97EAE4B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
