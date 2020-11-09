using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sude.Persistence.Migrations
{
    public partial class ChangeCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorkId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_WorkId",
                table: "Customers",
                column: "WorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Works_WorkId",
                table: "Customers",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Works_WorkId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_WorkId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "WorkId",
                table: "Customers");
        }
    }
}
