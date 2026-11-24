using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PruebaBackend.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarMasMarcas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MarcasAutos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nombre",
                value: "AUDI");

            migrationBuilder.InsertData(
                table: "MarcasAutos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 4, "subaru" },
                    { 5, "Ford" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MarcasAutos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MarcasAutos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "MarcasAutos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nombre",
                value: "Ford");
        }
    }
}
