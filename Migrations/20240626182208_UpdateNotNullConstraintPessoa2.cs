using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimosAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotNullConstraintPessoa2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "foto1_pessoa",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "foto2_pessoa",
                table: "Pessoas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "foto1_pessoa",
                table: "Pessoas",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "foto2_pessoa",
                table: "Pessoas",
                type: "bytea",
                nullable: true);
        }
    }
}
