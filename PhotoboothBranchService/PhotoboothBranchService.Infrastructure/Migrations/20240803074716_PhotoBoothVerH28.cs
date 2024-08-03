using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH28 : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "ResponseMessage",
                table: "Refunds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 8, 3, 7, 47, 16, 633, DateTimeKind.Utc).AddTicks(7833), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 8, 3, 7, 47, 16, 633, DateTimeKind.Utc).AddTicks(7836), "MoMo", "Active" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceID", "ServiceDescription", "ServiceName", "ServicePrice", "Status", "Unit" },
                values: new object[,]
                {
                    { new Guid("b3c4d5e6-a3d7-4edf-a3b9-34567890bcde"), "High-quality photo printing service", "Photo Printing", 150000m, 1, "Piece" },
                    { new Guid("d1f4e0c1-1f62-4c9e-8c4a-123456789abc"), "Professional makeup kit rental", "Makeup Kit Rental", 300000m, 1, "Set" },
                    { new Guid("f2e3d4c5-b2b7-4bcb-93c8-23456789defa"), "Service for sending photos via email", "Send Photos via Email", 50000m, 1, "Photo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("b3c4d5e6-a3d7-4edf-a3b9-34567890bcde"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("d1f4e0c1-1f62-4c9e-8c4a-123456789abc"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("f2e3d4c5-b2b7-4bcb-93c8-23456789defa"));

            migrationBuilder.DropColumn(
                name: "ResponseMessage",
                table: "Refunds");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 2, 0, 28, 16, 842, DateTimeKind.Utc).AddTicks(4634));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 2, 0, 28, 16, 842, DateTimeKind.Utc).AddTicks(4637));
        }
    }
}
