using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdjustEquipamentos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""Equipamentos"" 
                ALTER COLUMN ""estado_equipamento"" TYPE estado_equipamento 
                USING (estado_equipamento::estado_equipamento);
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
