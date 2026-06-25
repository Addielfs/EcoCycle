using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoCycle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUbicacionCoordenadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Latitud",
                table: "Publicaciones",
                type: "decimal(9,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitud",
                table: "Publicaciones",
                type: "decimal(9,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Latitud",
                table: "CentrosReciclaje",
                type: "decimal(9,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitud",
                table: "CentrosReciclaje",
                type: "decimal(9,6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitud",
                table: "Publicaciones");

            migrationBuilder.DropColumn(
                name: "Longitud",
                table: "Publicaciones");

            migrationBuilder.DropColumn(
                name: "Latitud",
                table: "CentrosReciclaje");

            migrationBuilder.DropColumn(
                name: "Longitud",
                table: "CentrosReciclaje");
        }
    }
}
