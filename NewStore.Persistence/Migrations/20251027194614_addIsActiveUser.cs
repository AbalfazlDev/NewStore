using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewStore.Persistence.Migrations
{
    public partial class addIsActiveUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2025, 10, 27, 23, 16, 14, 177, DateTimeKind.Local).AddTicks(2194));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2025, 10, 27, 23, 16, 14, 178, DateTimeKind.Local).AddTicks(7911));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2025, 10, 27, 23, 16, 14, 178, DateTimeKind.Local).AddTicks(7992));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2025, 10, 27, 22, 55, 52, 855, DateTimeKind.Local).AddTicks(2039));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2025, 10, 27, 22, 55, 52, 856, DateTimeKind.Local).AddTicks(5979));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2025, 10, 27, 22, 55, 52, 856, DateTimeKind.Local).AddTicks(6064));
        }
    }
}
