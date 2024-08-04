using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH29 : Migration
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
                name: "CustomerReferenceID",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"), new DateTime(2024, 8, 4, 14, 24, 38, 212, DateTimeKind.Utc).AddTicks(9650), "Cash", "Active" },
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 8, 4, 14, 24, 38, 212, DateTimeKind.Utc).AddTicks(9646), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 8, 4, 14, 24, 38, 212, DateTimeKind.Utc).AddTicks(9649), "MoMo", "Active" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"));

            migrationBuilder.DropColumn(
                name: "CustomerReferenceID",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 3, 7, 47, 16, 633, DateTimeKind.Utc).AddTicks(7833));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 3, 7, 47, 16, 633, DateTimeKind.Utc).AddTicks(7836));
        }
    }
}
