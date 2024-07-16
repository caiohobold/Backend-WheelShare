using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnIdAssociacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idassociacao",
                table: "Pessoas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idassociacao",
                table: "Locais",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idassociacao",
                table: "Feedbacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idassociacao",
                table: "Equipamentos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idassociacao",
                table: "Emprestimos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idassociacao",
                table: "Categorias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_idassociacao",
                table: "Pessoas",
                column: "idassociacao");

            migrationBuilder.CreateIndex(
                name: "IX_Locais_idassociacao",
                table: "Locais",
                column: "idassociacao");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_idassociacao",
                table: "Feedbacks",
                column: "idassociacao");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_idassociacao",
                table: "Equipamentos",
                column: "idassociacao");

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_idassociacao",
                table: "Emprestimos",
                column: "idassociacao");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_idassociacao",
                table: "Categorias",
                column: "idassociacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Associacoes_idassociacao",
                table: "Categorias",
                column: "idassociacao",
                principalTable: "Associacoes",
                principalColumn: "idassociacao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Associacoes_idassociacao",
                table: "Emprestimos",
                column: "idassociacao",
                principalTable: "Associacoes",
                principalColumn: "idassociacao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamentos_Associacoes_idassociacao",
                table: "Equipamentos",
                column: "idassociacao",
                principalTable: "Associacoes",
                principalColumn: "idassociacao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Associacoes_idassociacao",
                table: "Feedbacks",
                column: "idassociacao",
                principalTable: "Associacoes",
                principalColumn: "idassociacao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locais_Associacoes_idassociacao",
                table: "Locais",
                column: "idassociacao",
                principalTable: "Associacoes",
                principalColumn: "idassociacao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Associacoes_idassociacao",
                table: "Pessoas",
                column: "idassociacao",
                principalTable: "Associacoes",
                principalColumn: "idassociacao",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Associacoes_idassociacao",
                table: "Categorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Associacoes_idassociacao",
                table: "Emprestimos");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipamentos_Associacoes_idassociacao",
                table: "Equipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Associacoes_idassociacao",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Locais_Associacoes_idassociacao",
                table: "Locais");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Associacoes_idassociacao",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_idassociacao",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Locais_idassociacao",
                table: "Locais");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_idassociacao",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Equipamentos_idassociacao",
                table: "Equipamentos");

            migrationBuilder.DropIndex(
                name: "IX_Emprestimos_idassociacao",
                table: "Emprestimos");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_idassociacao",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "idassociacao",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "idassociacao",
                table: "Locais");

            migrationBuilder.DropColumn(
                name: "idassociacao",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "idassociacao",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "idassociacao",
                table: "Emprestimos");

            migrationBuilder.DropColumn(
                name: "idassociacao",
                table: "Categorias");
        }
    }
}
