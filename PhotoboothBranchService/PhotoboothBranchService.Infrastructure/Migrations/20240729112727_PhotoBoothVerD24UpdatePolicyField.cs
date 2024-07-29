using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerD24UpdatePolicyField : Migration
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "FullPaymentPolicies",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "FullPaymentPolicies",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultPolicy",
                table: "FullPaymentPolicies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPermanentPolicy",
                table: "FullPaymentPolicies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 7, 29, 11, 27, 27, 554, DateTimeKind.Utc).AddTicks(4248), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 7, 29, 11, 27, 27, 554, DateTimeKind.Utc).AddTicks(4251), "MoMo", "Active" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefaultPolicy",
                table: "FullPaymentPolicies");

            migrationBuilder.DropColumn(
                name: "IsPermanentPolicy",
                table: "FullPaymentPolicies");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "FullPaymentPolicies",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "FullPaymentPolicies",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 29, 6, 44, 10, 560, DateTimeKind.Utc).AddTicks(6972));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 29, 6, 44, 10, 560, DateTimeKind.Utc).AddTicks(6975));
        }
    }
}
