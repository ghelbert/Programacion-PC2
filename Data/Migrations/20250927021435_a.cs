using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PC2.Data.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Visitas",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Reservas",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<bool>(
                name: "ReservaActiva",
                table: "Inmuebles",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Inmuebles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReservaActiva",
                value: false);

            migrationBuilder.UpdateData(
                table: "Inmuebles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReservaActiva",
                value: false);

            migrationBuilder.UpdateData(
                table: "Inmuebles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Dormitorios", "ReservaActiva" },
                values: new object[] { 2, false });

            migrationBuilder.UpdateData(
                table: "Inmuebles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Dormitorios", "ReservaActiva" },
                values: new object[] { 1, false });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_InmuebleId",
                table: "Reservas",
                column: "InmuebleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Inmuebles_InmuebleId",
                table: "Reservas",
                column: "InmuebleId",
                principalTable: "Inmuebles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Inmuebles_InmuebleId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_InmuebleId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "ReservaActiva",
                table: "Inmuebles");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Visitas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Reservas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "Inmuebles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Dormitorios",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Inmuebles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Dormitorios",
                value: 0);
        }
    }
}
