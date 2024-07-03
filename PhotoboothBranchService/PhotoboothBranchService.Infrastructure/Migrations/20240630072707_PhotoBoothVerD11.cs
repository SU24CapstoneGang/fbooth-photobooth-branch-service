using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerD11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"));

            migrationBuilder.RenameColumn(
                name: "stickerLength",
                table: "Stickers",
                newName: "stickerHeight");

            migrationBuilder.RenameColumn(
                name: "boxLength",
                table: "PhotoBox",
                newName: "boxHeight");

            migrationBuilder.RenameColumn(
                name: "Lenght",
                table: "Layouts",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "Lenght",
                table: "Backgrounds",
                newName: "Height");

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 6, 30, 7, 27, 6, 922, DateTimeKind.Utc).AddTicks(3150), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 6, 30, 7, 27, 6, 922, DateTimeKind.Utc).AddTicks(3152), "MoMo", "Active" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stickerHeight",
                table: "Stickers",
                newName: "stickerLength");

            migrationBuilder.RenameColumn(
                name: "boxHeight",
                table: "PhotoBox",
                newName: "boxLength");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Layouts",
                newName: "Lenght");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Backgrounds",
                newName: "Lenght");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 27, 10, 59, 46, 41, DateTimeKind.Utc).AddTicks(2300));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 27, 10, 59, 46, 41, DateTimeKind.Utc).AddTicks(2302));
        }
    }
}
