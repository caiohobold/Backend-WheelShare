using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AjustarManuseioToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"Equipamentos\" ALTER COLUMN estado_equipamento TYPE integer USING (CASE WHEN estado_equipamento='Novo' THEN 0 ELSE 1 END);"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "estado_equipamento",
                table: "Equipamentos",
                type: "estado_equipamento",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 10);
        }
    }
}
