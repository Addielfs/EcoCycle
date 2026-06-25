using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoCycle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRolYConfiguracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Usuario");

            migrationBuilder.AddColumn<int>(
                name: "IdPublicacion",
                table: "Recompensas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Configuraciones",
                columns: table => new
                {
                    IdConfiguracion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuraciones", x => x.IdConfiguracion);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recompensas_IdPublicacion",
                table: "Recompensas",
                column: "IdPublicacion");

            migrationBuilder.CreateIndex(
                name: "IX_Configuraciones_Clave",
                table: "Configuraciones",
                column: "Clave",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recompensas_Publicaciones_IdPublicacion",
                table: "Recompensas",
                column: "IdPublicacion",
                principalTable: "Publicaciones",
                principalColumn: "IdPublicacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recompensas_Publicaciones_IdPublicacion",
                table: "Recompensas");

            migrationBuilder.DropTable(
                name: "Configuraciones");

            migrationBuilder.DropIndex(
                name: "IX_Recompensas_IdPublicacion",
                table: "Recompensas");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdPublicacion",
                table: "Recompensas");
        }
    }
}
