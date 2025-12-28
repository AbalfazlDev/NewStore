using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class delete_orderId_form_requestPay_and_add_CartId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestPays_Orders_OrderId",
                table: "RequestPays");

            migrationBuilder.DropIndex(
                name: "IX_RequestPays_OrderId",
                table: "RequestPays");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "RequestPays",
                newName: "CartId");

            migrationBuilder.AddColumn<long>(
                name: "RequestPayId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RequestPayId",
                table: "Orders",
                column: "RequestPayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_RequestPays_RequestPayId",
                table: "Orders",
                column: "RequestPayId",
                principalTable: "RequestPays",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_RequestPays_RequestPayId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_RequestPayId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RequestPayId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "RequestPays",
                newName: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestPays_OrderId",
                table: "RequestPays",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestPays_Orders_OrderId",
                table: "RequestPays",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
