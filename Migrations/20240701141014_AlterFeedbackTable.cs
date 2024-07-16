using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlterFeedbackTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Feedbacks",
                newName: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Feedbacks",
                newName: "UserId");
        }
    }
}
