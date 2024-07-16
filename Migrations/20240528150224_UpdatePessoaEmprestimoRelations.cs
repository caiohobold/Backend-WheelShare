using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimosAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePessoaEmprestimoRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Pessoas_IdPessoa",
                table: "Emprestimos");

            migrationBuilder.AddColumn<int>(
                name: "PessoaIdPessoa",
                table: "Emprestimos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PessoaIdPessoa1",
                table: "Emprestimos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_PessoaIdPessoa",
                table: "Emprestimos",
                column: "PessoaIdPessoa");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Pessoas_IdPessoa",
                table: "Emprestimos",
                column: "IdPessoa",
                principalTable: "Pessoas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Pessoas_PessoaIdPessoa",
                table: "Emprestimos",
                column: "PessoaIdPessoa",
                principalTable: "Pessoas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Pessoas_IdPessoa",
                table: "Emprestimos");

            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Pessoas_PessoaIdPessoa",
                table: "Emprestimos");

            migrationBuilder.DropIndex(
                name: "IX_Emprestimos_PessoaIdPessoa",
                table: "Emprestimos");

            migrationBuilder.DropColumn(
                name: "PessoaIdPessoa",
                table: "Emprestimos");

            migrationBuilder.DropColumn(
                name: "PessoaIdPessoa1",
                table: "Emprestimos");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Pessoas_IdPessoa",
                table: "Emprestimos",
                column: "IdPessoa",
                principalTable: "Pessoas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
