using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sude.Persistence.Migrations
{
    public partial class AddAttachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AttachmentFileAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentFileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    OrderInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Orders_OrderInfoId",
                        column: x => x.OrderInfoId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachments_Types_AttachmentTypeId",
                        column: x => x.AttachmentTypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachments_Types_EntityTypeId",
                        column: x => x.EntityTypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_AttachmentTypeId",
                table: "Attachments",
                column: "AttachmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_EntityTypeId",
                table: "Attachments",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_OrderInfoId",
                table: "Attachments",
                column: "OrderInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");
        }
    }
}
