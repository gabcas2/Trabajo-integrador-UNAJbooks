using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_integrador_2026.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFechaVencimientoPrestamo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaVencimiento",
                table: "Prestamos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaVencimiento",
                table: "Prestamos");
        }
    }
}
