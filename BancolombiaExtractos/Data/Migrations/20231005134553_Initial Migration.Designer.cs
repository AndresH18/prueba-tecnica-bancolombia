﻿// <auto-generated />
using System;
using BancolombiaExtractos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BancolombiaExtractos.Data.Migrations
{
    [DbContext(typeof(PruebaBancolombiaContext))]
    [Migration("20231005134553_Initial Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BancolombiaExtractos.Data.Models.Cuenta", b =>
                {
                    b.Property<int>("NumeroCuenta")
                        .HasColumnType("int")
                        .HasColumnName("numero_cuenta");

                    b.Property<decimal?>("Saldo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("saldo")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("TipoCuenta")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("tipo_cuenta");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id");

                    b.HasKey("NumeroCuenta")
                        .HasName("PK__cuentas__C6B74B89AED3B7F0");

                    b.HasIndex("UsuarioId");

                    b.ToTable("cuentas");
                });

            modelBuilder.Entity("BancolombiaExtractos.Data.Models.Movimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("NumeroCuenta")
                        .HasColumnType("int")
                        .HasColumnName("numero_cuenta");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("valor");

                    b.HasKey("Id")
                        .HasName("PK__movimien__3213E83F1A541F16");

                    b.HasIndex("NumeroCuenta");

                    b.ToTable("movimientos", null, t =>
                        {
                            t.HasTrigger("TR_movimientos_actualizar_saldo_after_insert");
                        });
                });

            modelBuilder.Entity("BancolombiaExtractos.Data.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Titular")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("titular");

                    b.HasKey("Id")
                        .HasName("PK__usuarios__3213E83F97EAE4B7");

                    b.HasIndex(new[] { "Email" }, "UQ__usuarios__AB6E6164FD35CE4C")
                        .IsUnique();

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("BancolombiaExtractos.Data.Models.Cuenta", b =>
                {
                    b.HasOne("BancolombiaExtractos.Data.Models.Usuario", "Usuario")
                        .WithMany("Cuenta")
                        .HasForeignKey("UsuarioId")
                        .IsRequired()
                        .HasConstraintName("FK__cuentas__usuario__3B75D760");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BancolombiaExtractos.Data.Models.Movimiento", b =>
                {
                    b.HasOne("BancolombiaExtractos.Data.Models.Cuenta", "NumeroCuentaNavigation")
                        .WithMany("Movimientos")
                        .HasForeignKey("NumeroCuenta")
                        .HasConstraintName("FK__movimient__numer__3F466844");

                    b.Navigation("NumeroCuentaNavigation");
                });

            modelBuilder.Entity("BancolombiaExtractos.Data.Models.Cuenta", b =>
                {
                    b.Navigation("Movimientos");
                });

            modelBuilder.Entity("BancolombiaExtractos.Data.Models.Usuario", b =>
                {
                    b.Navigation("Cuenta");
                });
#pragma warning restore 612, 618
        }
    }
}
