using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH36addserviceimageurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"));

            migrationBuilder.AddColumn<string>(
                name: "CouldID",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceIamgeURL",
                table: "Services",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"), new DateTime(2024, 8, 12, 13, 54, 4, 694, DateTimeKind.Utc).AddTicks(3469), "Cash", "Active" },
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 8, 12, 13, 54, 4, 694, DateTimeKind.Utc).AddTicks(3465), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 8, 12, 13, 54, 4, 694, DateTimeKind.Utc).AddTicks(3467), "MoMo", "Active" }
                });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("b3c4d5e6-a3d7-4edf-a3b9-34567890bcde"),
                columns: new[] { "CouldID", "CreatedDate", "ServiceIamgeURL" },
                values: new object[] { "", new DateTime(2024, 8, 12, 20, 54, 4, 696, DateTimeKind.Local).AddTicks(6849), "" });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("d1f4e0c1-1f62-4c9e-8c4a-123456789abc"),
                columns: new[] { "CouldID", "CreatedDate", "ServiceIamgeURL" },
                values: new object[] { "", new DateTime(2024, 8, 12, 20, 54, 4, 696, DateTimeKind.Local).AddTicks(6832), "" });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("f2e3d4c5-b2b7-4bcb-93c8-23456789defa"),
                columns: new[] { "CouldID", "CreatedDate", "ServiceIamgeURL" },
                values: new object[] { "", new DateTime(2024, 8, 12, 20, 54, 4, 696, DateTimeKind.Local).AddTicks(6845), "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouldID",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceIamgeURL",
                table: "Services");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 12, 10, 33, 48, 880, DateTimeKind.Utc).AddTicks(5168));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 12, 10, 33, 48, 880, DateTimeKind.Utc).AddTicks(5163));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 12, 10, 33, 48, 880, DateTimeKind.Utc).AddTicks(5166));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("b3c4d5e6-a3d7-4edf-a3b9-34567890bcde"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 12, 17, 33, 48, 882, DateTimeKind.Local).AddTicks(6167));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("d1f4e0c1-1f62-4c9e-8c4a-123456789abc"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 12, 17, 33, 48, 882, DateTimeKind.Local).AddTicks(6151));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("f2e3d4c5-b2b7-4bcb-93c8-23456789defa"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 12, 17, 33, 48, 882, DateTimeKind.Local).AddTicks(6164));
        }
    }
}
