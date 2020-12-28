using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sude.Persistence.Migrations
{
    public partial class AddWorkUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkUsers_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkUsers_WorkId",
                table: "WorkUsers",
                column: "WorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkUsers");
        }
    }
}
