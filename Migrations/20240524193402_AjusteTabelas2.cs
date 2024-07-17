using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTabelas2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
              //  name: "IX_Equipamentos_status",
               // table: "Equipamentos");

            //migrationBuilder.DropColumn(
              //  name: "status",
               // table: "Equipamentos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
              //  name: "status",
                //table: "Equipamentos",
                //type: "integer",
               // nullable: false,
               // defaultValue: 0);

          //  migrationBuilder.AddColumn<int>(
            //    name: "status",
              //  table: "Equipamentos",
                //type: "integer",
                //nullable: false,
                //defaultValue: 0);

            //migrationBuilder.CreateIndex(
               // name: "IX_Equipamentos_status",
              //  table: "Equipamentos",
              //  column: "status");

           // migrationBuilder.AddForeignKey(
            //    name: "FK_Equipamentos_Emprestimos_status",
             //   table: "Equipamentos",
             //   column: "status",
             //   principalTable: "Emprestimos",
              //  principalColumn: "id",
              //  onDelete: ReferentialAction.Cascade);
        }
    }
}
