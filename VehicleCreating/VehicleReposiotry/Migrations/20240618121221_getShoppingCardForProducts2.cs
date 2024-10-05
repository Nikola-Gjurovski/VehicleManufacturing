using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleReposiotry.Migrations
{
    /// <inheritdoc />
    public partial class getShoppingCardForProducts2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartVehicleParts_ShoppingCarts_ProductShoppingCartsId",
                table: "ShoppingCartVehicleParts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartVehicleParts_VehicleParts_ProductShoppingCartsId1",
                table: "ShoppingCartVehicleParts");

            migrationBuilder.RenameColumn(
                name: "ProductShoppingCartsId1",
                table: "ShoppingCartVehicleParts",
                newName: "ShoppingCartsId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartVehicleParts_ProductShoppingCartsId1",
                table: "ShoppingCartVehicleParts",
                newName: "IX_ShoppingCartVehicleParts_ShoppingCartsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartVehicleParts_ShoppingCarts_ShoppingCartsId",
                table: "ShoppingCartVehicleParts",
                column: "ShoppingCartsId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartVehicleParts_VehicleParts_ProductShoppingCartsId",
                table: "ShoppingCartVehicleParts",
                column: "ProductShoppingCartsId",
                principalTable: "VehicleParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartVehicleParts_ShoppingCarts_ShoppingCartsId",
                table: "ShoppingCartVehicleParts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartVehicleParts_VehicleParts_ProductShoppingCartsId",
                table: "ShoppingCartVehicleParts");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartsId",
                table: "ShoppingCartVehicleParts",
                newName: "ProductShoppingCartsId1");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartVehicleParts_ShoppingCartsId",
                table: "ShoppingCartVehicleParts",
                newName: "IX_ShoppingCartVehicleParts_ProductShoppingCartsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartVehicleParts_ShoppingCarts_ProductShoppingCartsId",
                table: "ShoppingCartVehicleParts",
                column: "ProductShoppingCartsId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartVehicleParts_VehicleParts_ProductShoppingCartsId1",
                table: "ShoppingCartVehicleParts",
                column: "ProductShoppingCartsId1",
                principalTable: "VehicleParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
