using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_integrador_2026.Migrations
{
    /// <inheritdoc />
    public partial class PermitirEliminarSocioConHistorial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Socios_SocioId",
                table: "Prestamos");

            migrationBuilder.AlterColumn<int>(
                name: "SocioId",
                table: "Prestamos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Socios_SocioId",
                table: "Prestamos",
                column: "SocioId",
                principalTable: "Socios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Socios_SocioId",
                table: "Prestamos");

            migrationBuilder.AlterColumn<int>(
                name: "SocioId",
                table: "Prestamos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Socios_SocioId",
                table: "Prestamos",
                column: "SocioId",
                principalTable: "Socios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
