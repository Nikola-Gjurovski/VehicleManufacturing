using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleReposiotry.Migrations
{
    /// <inheritdoc />
    public partial class etlsas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersETL",
                table: "UsersETL");

            migrationBuilder.RenameTable(
                name: "UsersETL",
                newName: "UsersETLSS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersETLSS",
                table: "UsersETLSS",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersETLSS",
                table: "UsersETLSS");

            migrationBuilder.RenameTable(
                name: "UsersETLSS",
                newName: "UsersETL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersETL",
                table: "UsersETL",
                column: "Id");
        }
    }
}
