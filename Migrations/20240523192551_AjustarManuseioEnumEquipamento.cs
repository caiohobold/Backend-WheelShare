using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AjustarManuseioEnumEquipamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql(
                //@"ALTER TABLE ""Equipamentos"" 
                  //ALTER COLUMN estado_equipamento 
                 // TYPE integer;"
            //);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
               // name: "estado_equipamento",
                //table: "Equipamentos",
               // type: "character varying(10)",
                //maxLength: 10,
               // nullable: false,
               // oldClrType: typeof(string),
               // oldType: "integer",
               // oldMaxLength: 10);
        }
    }
}
