using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancolombiaExtractos.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titular = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuarios__3213E83F97EAE4B7", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cuentas",
                columns: table => new
                {
                    numero_cuenta = table.Column<int>(type: "int", nullable: false),
                    usuario_id = table.Column<int>(type: "int", nullable: false),
                    tipo_cuenta = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cuentas__C6B74B89AED3B7F0", x => x.numero_cuenta);
                    table.ForeignKey(
                        name: "FK__cuentas__usuario__3B75D760",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "movimientos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    numero_cuenta = table.Column<int>(type: "int", nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__movimien__3213E83F1A541F16", x => x.id);
                    table.ForeignKey(
                        name: "FK__movimient__numer__3F466844",
                        column: x => x.numero_cuenta,
                        principalTable: "cuentas",
                        principalColumn: "numero_cuenta");
                });

            migrationBuilder.CreateIndex(
                name: "IX_cuentas_usuario_id",
                table: "cuentas",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_movimientos_numero_cuenta",
                table: "movimientos",
                column: "numero_cuenta");

            migrationBuilder.CreateIndex(
                name: "UQ__usuarios__AB6E6164FD35CE4C",
                table: "usuarios",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movimientos");

            migrationBuilder.DropTable(
                name: "cuentas");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
