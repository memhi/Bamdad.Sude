using Microsoft.EntityFrameworkCore.Migrations;

namespace Sude.Persistence.Migrations
{
    public partial class ChangeWorkinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoAddress",
                table: "Works");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoAddress",
                table: "Works",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
