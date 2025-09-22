using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbOperationWithEfCoreApp.Migrations
{
    /// <inheritdoc />
    public partial class changeCurrencyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyTypeId",
                table: "BookPrices",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CurrencyType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "USAD");

            migrationBuilder.UpdateData(
                table: "CurrencyType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Europe");

            migrationBuilder.UpdateData(
                table: "CurrencyType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Dinar");

            migrationBuilder.CreateIndex(
                name: "IX_BookPrices_CurrencyTypeId",
                table: "BookPrices",
                column: "CurrencyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookPrices_CurrencyType_CurrencyTypeId",
                table: "BookPrices",
                column: "CurrencyTypeId",
                principalTable: "CurrencyType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookPrices_CurrencyType_CurrencyTypeId",
                table: "BookPrices");

            migrationBuilder.DropIndex(
                name: "IX_BookPrices_CurrencyTypeId",
                table: "BookPrices");

            migrationBuilder.DropColumn(
                name: "CurrencyTypeId",
                table: "BookPrices");

            migrationBuilder.UpdateData(
                table: "CurrencyType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Indian INR");

            migrationBuilder.UpdateData(
                table: "CurrencyType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Indian INR");

            migrationBuilder.UpdateData(
                table: "CurrencyType",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Indian INR");
        }
    }
}
