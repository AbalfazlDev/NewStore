using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewStore.Persistence.Migrations
{
    public partial class addFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2025, 10, 27, 22, 31, 23, 449, DateTimeKind.Local).AddTicks(6677));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2025, 10, 27, 22, 31, 23, 451, DateTimeKind.Local).AddTicks(726));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2025, 10, 27, 22, 31, 23, 451, DateTimeKind.Local).AddTicks(811));
        }
    }
}
