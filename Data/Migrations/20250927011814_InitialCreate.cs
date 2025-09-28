using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PC2.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inmuebles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Codigo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Imagen = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Ciudad = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Dormitorios = table.Column<int>(type: "INTEGER", nullable: false),
                    Banos = table.Column<int>(type: "INTEGER", nullable: false),
                    MetrosCuadrados = table.Column<double>(type: "REAL", nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inmuebles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InmuebleId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InmuebleId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    Notas = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Inmuebles",
                columns: new[] { "Id", "Activo", "Banos", "Ciudad", "Codigo", "Direccion", "Dormitorios", "Imagen", "MetrosCuadrados", "Precio", "Tipo", "Titulo" },
                values: new object[,]
                {
                    { 1, true, 2, "Madrid", "INM-001", "Av. Gran Via, 12", 3, "https://tse4.mm.bing.net/th/id/OIP.RN2FM_PYPFBQL-UxY03gaQHaE8?rs=1&pid=ImgDetMain&o=7&rm=3", 85.5, 200000m, 0, "Departamento Moderno" },
                    { 2, true, 3, "Barcelona", "INM-002", "Carrer de Pau, 5", 4, "https://tse4.mm.bing.net/th/id/OIP.JNgdoxHcMPEFZm-MzpH1tgHaFL?rs=1&pid=ImgDetMain&o=7&rm=3", 200.0, 450000m, 1, "Casa de Lujo" },
                    { 3, true, 1, "Valencia", "INM-003", "Carrer del Mar, 1", 0, "https://www.rescombuilds.com/wp-content/uploads/2022/06/3-exquisicare-senior-living-210415112025-78.jpg", 120.0, 150000m, 2, "Oficina Comercial" },
                    { 4, true, 1, "Sevilla", "INM-004", "Calle San Pedro, 8", 0, "https://www.rescombuilds.com/wp-content/uploads/2022/06/3-exquisicare-senior-living-210415112025-78.jpg", 90.0, 180000m, 3, "Local Comercial" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inmuebles");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Visitas");
        }
    }
}
