using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewStore.Persistence.Migrations
{
    public partial class editCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ParentCategoryId",
                table: "Categories",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2025, 11, 3, 0, 45, 23, 587, DateTimeKind.Local).AddTicks(4866));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2025, 11, 3, 0, 45, 23, 588, DateTimeKind.Local).AddTicks(8136));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2025, 11, 3, 0, 45, 23, 588, DateTimeKind.Local).AddTicks(8208));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ParentCategoryId",
                table: "Categories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2025, 11, 3, 0, 40, 3, 358, DateTimeKind.Local).AddTicks(9631));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2025, 11, 3, 0, 40, 3, 360, DateTimeKind.Local).AddTicks(2428));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2025, 11, 3, 0, 40, 3, 360, DateTimeKind.Local).AddTicks(2506));
        }
    }
}
