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

            entity.ToTable("cuentas");

            entity.Property(e => e.NumeroCuenta)
                .ValueGeneratedNever()
                .HasColumnName("numero_cuenta");
            entity.Property(e => e.Saldo)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("saldo");
            entity.Property(e => e.TipoCuenta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_cuenta");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cuentas__usuario__3B75D760");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__movimien__3213E83F1A541F16");

            entity.ToTable("movimientos", tb => tb.HasTrigger("TR_movimientos_actualizar_saldo_after_insert"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha");
            entity.Property(e => e.NumeroCuenta).HasColumnName("numero_cuenta");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.NumeroCuentaNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.NumeroCuenta)
                .HasConstraintName("FK__movimient__numer__3F466844");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3213E83F97EAE4B7");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "UQ__usuarios__AB6E6164FD35CE4C").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Titular)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titular");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
