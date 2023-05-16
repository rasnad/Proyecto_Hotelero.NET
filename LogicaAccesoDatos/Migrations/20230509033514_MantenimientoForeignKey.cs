using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class MantenimientoForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mantenimientos_Cabañas_CabañaNumeroHabitacion",
                table: "Mantenimientos");

            migrationBuilder.DropIndex(
                name: "IX_Mantenimientos_CabañaNumeroHabitacion",
                table: "Mantenimientos");

            migrationBuilder.DropColumn(
                name: "CabañaNumeroHabitacion",
                table: "Mantenimientos");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_IdCabaña",
                table: "Mantenimientos",
                column: "IdCabaña");

            migrationBuilder.AddForeignKey(
                name: "FK_Mantenimientos_Cabañas_IdCabaña",
                table: "Mantenimientos",
                column: "IdCabaña",
                principalTable: "Cabañas",
                principalColumn: "NumeroHabitacion",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mantenimientos_Cabañas_IdCabaña",
                table: "Mantenimientos");

            migrationBuilder.DropIndex(
                name: "IX_Mantenimientos_IdCabaña",
                table: "Mantenimientos");

            migrationBuilder.AddColumn<int>(
                name: "CabañaNumeroHabitacion",
                table: "Mantenimientos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_CabañaNumeroHabitacion",
                table: "Mantenimientos",
                column: "CabañaNumeroHabitacion");

            migrationBuilder.AddForeignKey(
                name: "FK_Mantenimientos_Cabañas_CabañaNumeroHabitacion",
                table: "Mantenimientos",
                column: "CabañaNumeroHabitacion",
                principalTable: "Cabañas",
                principalColumn: "NumeroHabitacion",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
