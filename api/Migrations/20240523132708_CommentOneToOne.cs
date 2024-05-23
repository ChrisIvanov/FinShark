using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CommentOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b037705-6783-488e-be3d-7fc0b38e258f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "531b6094-8c1a-45d0-bc02-b9d2f83580e6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "95d3a61e-3e77-4de0-8f2d-04b90d36c29a", null, "User", "USER" },
                    { "bf3009f5-c5d7-4ded-bff8-f63867fbad15", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95d3a61e-3e77-4de0-8f2d-04b90d36c29a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf3009f5-c5d7-4ded-bff8-f63867fbad15");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4b037705-6783-488e-be3d-7fc0b38e258f", null, "User", "USER" },
                    { "531b6094-8c1a-45d0-bc02-b9d2f83580e6", null, "Admin", "ADMIN" }
                });
        }
    }
}
