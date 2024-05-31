using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migrationVd2 : Migration
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

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "PrintPricings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 652, DateTimeKind.Utc).AddTicks(1796),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 41, DateTimeKind.Utc).AddTicks(5301));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PrintPricings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 651, DateTimeKind.Utc).AddTicks(4134),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 40, DateTimeKind.Utc).AddTicks(7406));

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPerPrintNumber",
                table: "PrintPricings",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 649, DateTimeKind.Utc).AddTicks(1386),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 38, DateTimeKind.Utc).AddTicks(3412));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 648, DateTimeKind.Utc).AddTicks(8866),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 38, DateTimeKind.Utc).AddTicks(767));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 648, DateTimeKind.Utc).AddTicks(6741),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 37, DateTimeKind.Utc).AddTicks(8558));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "FinalPictures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 648, DateTimeKind.Utc).AddTicks(4602),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 37, DateTimeKind.Utc).AddTicks(6200));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Filters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 648, DateTimeKind.Utc).AddTicks(271),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 37, DateTimeKind.Utc).AddTicks(1368));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 646, DateTimeKind.Utc).AddTicks(1115),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 34, DateTimeKind.Utc).AddTicks(8694));

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 5, 31, 13, 18, 27, 649, DateTimeKind.Utc).AddTicks(1616), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 5, 31, 13, 18, 27, 649, DateTimeKind.Utc).AddTicks(1620), "MoMo", "Active" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPerPrintNumber",
                table: "PrintPricings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 41, DateTimeKind.Utc).AddTicks(5301),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 652, DateTimeKind.Utc).AddTicks(1796));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PrintPricings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 40, DateTimeKind.Utc).AddTicks(7406),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 651, DateTimeKind.Utc).AddTicks(4134));

            migrationBuilder.AddColumn<float>(
                name: "UnitPrice",
                table: "PrintPricings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 38, DateTimeKind.Utc).AddTicks(3412),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 649, DateTimeKind.Utc).AddTicks(1386));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 38, DateTimeKind.Utc).AddTicks(767),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 648, DateTimeKind.Utc).AddTicks(8866));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 37, DateTimeKind.Utc).AddTicks(8558),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 648, DateTimeKind.Utc).AddTicks(6741));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "FinalPictures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 37, DateTimeKind.Utc).AddTicks(6200),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 648, DateTimeKind.Utc).AddTicks(4602));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Filters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 37, DateTimeKind.Utc).AddTicks(1368),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 648, DateTimeKind.Utc).AddTicks(271));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 31, 5, 58, 10, 34, DateTimeKind.Utc).AddTicks(8694),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 31, 13, 18, 27, 646, DateTimeKind.Utc).AddTicks(1115));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 5, 31, 5, 58, 10, 38, DateTimeKind.Utc).AddTicks(3654));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 5, 31, 5, 58, 10, 38, DateTimeKind.Utc).AddTicks(3657));
        }
    }
}
