using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimosAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // Define o valor padrão para a coluna
            migrationBuilder.Sql("ALTER TABLE \"Equipamentos\" ALTER COLUMN status SET DEFAULT 'Disponível';");

            migrationBuilder.RenameColumn(
                name: "IdAssociacao",
                table: "Usuarios",
                newName: "idassociacao");

            migrationBuilder.RenameColumn(
                name: "IdAssociacao",
                table: "Associacoes",
                newName: "idassociacao");







    
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Equipamentos\" ALTER COLUMN status DROP DEFAULT;");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipamentos_Categorias_id_categoria",
                table: "Equipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Associacoes_AssociacaoIdAssociacao",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_AssociacaoIdAssociacao",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Equipamentos_id_categoria",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "AssociacaoIdAssociacao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "id_categoria",
                table: "Equipamentos");

            migrationBuilder.RenameColumn(
                name: "idassociacao",
                table: "Usuarios",
                newName: "IdAssociacao");

            migrationBuilder.RenameColumn(
                name: "idassociacao",
                table: "Associacoes",
                newName: "IdAssociacao");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdAssociacao",
                table: "Usuarios",
                column: "IdAssociacao");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_IdCategoria",
                table: "Equipamentos",
                column: "IdCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamentos_Categorias_IdCategoria",
                table: "Equipamentos",
                column: "IdCategoria",
                principalTable: "Categorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Associacoes_IdAssociacao",
                table: "Usuarios",
                column: "IdAssociacao",
                principalTable: "Associacoes",
                principalColumn: "IdAssociacao",
                onDelete: ReferentialAction.Cascade);



        }
    }
}
