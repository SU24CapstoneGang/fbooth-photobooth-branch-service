using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerD3 : Migration
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
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 899, DateTimeKind.Utc).AddTicks(8027),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 604, DateTimeKind.Utc).AddTicks(4205));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "SessionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 899, DateTimeKind.Utc).AddTicks(3852),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 603, DateTimeKind.Utc).AddTicks(9934));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 897, DateTimeKind.Utc).AddTicks(8834),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 602, DateTimeKind.Utc).AddTicks(62));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 891, DateTimeKind.Utc).AddTicks(8197),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 594, DateTimeKind.Utc).AddTicks(2686));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 891, DateTimeKind.Utc).AddTicks(2005),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 593, DateTimeKind.Utc).AddTicks(4767));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 890, DateTimeKind.Utc).AddTicks(7177),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 592, DateTimeKind.Utc).AddTicks(8484));

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 6, 13, 22, 46, 25, 891, DateTimeKind.Utc).AddTicks(8778), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 6, 13, 22, 46, 25, 891, DateTimeKind.Utc).AddTicks(8781), "MoMo", "Active" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 604, DateTimeKind.Utc).AddTicks(4205),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 899, DateTimeKind.Utc).AddTicks(8027));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "SessionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 603, DateTimeKind.Utc).AddTicks(9934),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 899, DateTimeKind.Utc).AddTicks(3852));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 602, DateTimeKind.Utc).AddTicks(62),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 897, DateTimeKind.Utc).AddTicks(8834));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 594, DateTimeKind.Utc).AddTicks(2686),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 891, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 593, DateTimeKind.Utc).AddTicks(4767),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 891, DateTimeKind.Utc).AddTicks(2005));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 592, DateTimeKind.Utc).AddTicks(8484),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 890, DateTimeKind.Utc).AddTicks(7177));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 13, 22, 41, 37, 594, DateTimeKind.Utc).AddTicks(3401));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 13, 22, 41, 37, 594, DateTimeKind.Utc).AddTicks(3404));
        }
    }
}
