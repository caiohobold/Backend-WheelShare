using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmprestimosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddLocaisTableAndForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_local",
                table: "Equipamentos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Locais",
                columns: table => new
                {
                    id_local = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome_local = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locais", x => x.id_local);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_id_local",
                table: "Equipamentos",
                column: "id_local");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamentos_Locais_id_local",
                table: "Equipamentos",
                column: "id_local",
                principalTable: "Locais",
                principalColumn: "id_local",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipamentos_Locais_id_local",
                table: "Equipamentos");

            migrationBuilder.DropTable(
                name: "Locais");

            migrationBuilder.DropIndex(
                name: "IX_Equipamentos_id_local",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "id_local",
                table: "Equipamentos");
        }
    }
}
