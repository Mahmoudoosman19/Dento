using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_OTPs_OtpId",
                schema: "users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NameAr",
                schema: "users",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "BaseAvatarId",
                schema: "users",
                table: "Designer");

            migrationBuilder.DropColumn(
                name: "CustomizedAvatarId",
                schema: "users",
                table: "Designer");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                schema: "users",
                table: "Permission",
                newName: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_OTPs_OtpId",
                schema: "users",
                table: "Users",
                column: "OtpId",
                principalSchema: "users",
                principalTable: "OTPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_OTPs_OtpId",
                schema: "users",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "users",
                table: "Permission",
                newName: "NameEn");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                schema: "users",
                table: "Permission",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BaseAvatarId",
                schema: "users",
                table: "Designer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomizedAvatarId",
                schema: "users",
                table: "Designer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_OTPs_OtpId",
                schema: "users",
                table: "Users",
                column: "OtpId",
                principalSchema: "users",
                principalTable: "OTPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
