using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sude.Persistence.Migrations
{
    public partial class ModifyServingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasInventoryTracking",
                table: "Servings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Servings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "InventoryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServingInventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentInventory = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServingInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServingInventories_InventoryTypes_InventoryTypeId",
                        column: x => x.InventoryTypeId,
                        principalTable: "InventoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServingInventories_Servings_ServingId",
                        column: x => x.ServingId,
                        principalTable: "Servings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServingInventoryTrackings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCost = table.Column<bool>(type: "bit", nullable: false),
                    ServingInventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServingInventoryTrackings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServingInventoryTrackings_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServingInventoryTrackings_ServingInventories_ServingInventoryId",
                        column: x => x.ServingInventoryId,
                        principalTable: "ServingInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServingInventories_InventoryTypeId",
                table: "ServingInventories",
                column: "InventoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServingInventories_ServingId",
                table: "ServingInventories",
                column: "ServingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServingInventoryTrackings_OrderDetailId",
                table: "ServingInventoryTrackings",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ServingInventoryTrackings_ServingInventoryId",
                table: "ServingInventoryTrackings",
                column: "ServingInventoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServingInventoryTrackings");

            migrationBuilder.DropTable(
                name: "ServingInventories");

            migrationBuilder.DropTable(
                name: "InventoryTypes");

            migrationBuilder.DropColumn(
                name: "HasInventoryTracking",
                table: "Servings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Servings");
        }
    }
}
