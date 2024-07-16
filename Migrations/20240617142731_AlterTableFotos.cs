using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableFotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "foto1_equipamento",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "foto2_equipamento",
                table: "Equipamentos");

            migrationBuilder.AddColumn<byte[]>(
                name: "foto1_equipamento",
                table: "Equipamentos",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "foto2_equipamento",
                table: "Equipamentos",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "foto1_equipamento",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "foto2_equipamento",
                table: "Equipamentos");

            migrationBuilder.AddColumn<string>(
                name: "foto1_equipamento",
                table: "Equipamentos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "foto2_equipamento",
                table: "Equipamentos",
                type: "text",
                nullable: true);
        }
    }
}
