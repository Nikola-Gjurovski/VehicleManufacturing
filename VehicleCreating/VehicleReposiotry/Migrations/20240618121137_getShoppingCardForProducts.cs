using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleReposiotry.Migrations
{
    /// <inheritdoc />
    public partial class getShoppingCardForProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleParts_ShoppingCarts_ShoppingCartId",
                table: "VehicleParts");

            migrationBuilder.DropIndex(
                name: "IX_VehicleParts_ShoppingCartId",
                table: "VehicleParts");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "VehicleParts");

            migrationBuilder.CreateTable(
                name: "ShoppingCartVehicleParts",
                columns: table => new
                {
                    ProductShoppingCartsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductShoppingCartsId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartVehicleParts", x => new { x.ProductShoppingCartsId, x.ProductShoppingCartsId1 });
                    table.ForeignKey(
                        name: "FK_ShoppingCartVehicleParts_ShoppingCarts_ProductShoppingCartsId",
                        column: x => x.ProductShoppingCartsId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartVehicleParts_VehicleParts_ProductShoppingCartsId1",
                        column: x => x.ProductShoppingCartsId1,
                        principalTable: "VehicleParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartVehicleParts_ProductShoppingCartsId1",
                table: "ShoppingCartVehicleParts",
                column: "ProductShoppingCartsId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartVehicleParts");

            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingCartId",
                table: "VehicleParts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleParts_ShoppingCartId",
                table: "VehicleParts",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleParts_ShoppingCarts_ShoppingCartId",
                table: "VehicleParts",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }
    }
}
