using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Case.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class filePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Model3DPath",
                schema: "cases",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model3DPath",
                schema: "cases",
                table: "Cases");
        }
    }
}
