using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sude.Persistence.Migrations
{
    public partial class ChangeRealtionAttachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Orders_OrderInfoId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_OrderInfoId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "OrderInfoId",
                table: "Attachments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderInfoId",
                table: "Attachments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_OrderInfoId",
                table: "Attachments",
                column: "OrderInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Orders_OrderInfoId",
                table: "Attachments",
                column: "OrderInfoId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
