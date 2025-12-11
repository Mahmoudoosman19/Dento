using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TaskInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tasks");

            migrationBuilder.CreateTable(
                name: "CaseTasks",
                schema: "tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseTasks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseTasks_CaseId",
                schema: "tasks",
                table: "CaseTasks",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTasks_DesignerId",
                schema: "tasks",
                table: "CaseTasks",
                column: "DesignerId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTasks_Status",
                schema: "tasks",
                table: "CaseTasks",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseTasks",
                schema: "tasks");
        }
    }
}
