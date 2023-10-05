using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancolombiaExtractos.Data.Migrations
{
    /// <summary>
    /// Migration to add <i>TR_movimientos_actualizar_saldo_after_insert</i> Trigger to database table <b>movimientos</b>
    /// </summary>
    public partial class Trigger_Movimientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                 CREATE OR ALTER TRIGGER [TR_movimientos_actualizar_saldo_after_insert]
                                     ON [movimientos]
                                     AFTER INSERT
                                     AS
                                 BEGIN
                                     UPDATE [c]
                                     SET [saldo] = [saldo] + [i].[valor]
                                     FROM [cuentas] [c]
                                              INNER JOIN [inserted] [i] ON [c].[numero_cuenta] = [i].[numero_cuenta]
                                 END
                                 """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER [TR_movimientos_actualizar_saldo_after_insert]");
        }
    }
}
