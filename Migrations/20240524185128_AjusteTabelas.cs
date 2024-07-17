using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql(
                //@"ALTER TABLE ""Equipamentos""
                 // ALTER COLUMN status DROP DEFAULT;"
           // );

            //migrationBuilder.Sql(
               // @"ALTER TABLE ""Equipamentos""
                //  ALTER COLUMN status
                //  TYPE integer;"
           // );

            migrationBuilder.Sql(
                @"ALTER TABLE ""Equipamentos""
                  ALTER COLUMN status SET DEFAULT 1;"
            );

            migrationBuilder.Sql(
                @"ALTER TABLE ""Equipamentos""
                  RENAME COLUMN status TO status_equipamento;"
            );


            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Emprestimos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_status_equipamento",
                table: "Equipamentos",
                column: "status_equipamento");

            //migrationBuilder.DropForeignKey(
               // name: "FK_Equipamentos_Emprestimos_status",
                //table: "Equipamentos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipamentos_Emprestimos_status",
                table: "Equipamentos");

            migrationBuilder.DropIndex(
                name: "IX_Equipamentos_status",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "status_equipamento",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Emprestimos");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Equipamentos",
                type: "varchar(20) default 'Disponível'",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
