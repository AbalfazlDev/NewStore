using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_viewCount_Star_To_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Star",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2025, 12, 6, 15, 48, 50, 28, DateTimeKind.Local).AddTicks(1884));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2025, 12, 6, 15, 48, 50, 29, DateTimeKind.Local).AddTicks(3000));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2025, 12, 6, 15, 48, 50, 29, DateTimeKind.Local).AddTicks(3069));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Star",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2025, 11, 13, 13, 45, 36, 449, DateTimeKind.Local).AddTicks(3762));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2025, 11, 13, 13, 45, 36, 450, DateTimeKind.Local).AddTicks(5771));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2025, 11, 13, 13, 45, 36, 450, DateTimeKind.Local).AddTicks(5837));
        }
    }
}
