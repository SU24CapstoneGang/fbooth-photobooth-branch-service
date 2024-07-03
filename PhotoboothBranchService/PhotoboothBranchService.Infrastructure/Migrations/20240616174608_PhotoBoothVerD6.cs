using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerD6 : Migration
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
                defaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 770, DateTimeKind.Utc).AddTicks(8388),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 255, DateTimeKind.Utc).AddTicks(8024));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "SessionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 769, DateTimeKind.Utc).AddTicks(6553),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 255, DateTimeKind.Utc).AddTicks(3774));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 760, DateTimeKind.Utc).AddTicks(2666),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 251, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 759, DateTimeKind.Utc).AddTicks(9500),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 251, DateTimeKind.Utc).AddTicks(343));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 756, DateTimeKind.Utc).AddTicks(485),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 248, DateTimeKind.Utc).AddTicks(2183));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 755, DateTimeKind.Utc).AddTicks(591),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 247, DateTimeKind.Utc).AddTicks(5925));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 754, DateTimeKind.Utc).AddTicks(6301),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 247, DateTimeKind.Utc).AddTicks(2753));

            migrationBuilder.AlterColumn<string>(
                name: "ResetPasswordToken",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 6, 16, 17, 46, 8, 756, DateTimeKind.Utc).AddTicks(1534), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 6, 16, 17, 46, 8, 756, DateTimeKind.Utc).AddTicks(1537), "MoMo", "Active" }
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
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 255, DateTimeKind.Utc).AddTicks(8024),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 770, DateTimeKind.Utc).AddTicks(8388));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "SessionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 255, DateTimeKind.Utc).AddTicks(3774),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 769, DateTimeKind.Utc).AddTicks(6553));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 251, DateTimeKind.Utc).AddTicks(2372),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 760, DateTimeKind.Utc).AddTicks(2666));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 251, DateTimeKind.Utc).AddTicks(343),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 759, DateTimeKind.Utc).AddTicks(9500));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 248, DateTimeKind.Utc).AddTicks(2183),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 756, DateTimeKind.Utc).AddTicks(485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 247, DateTimeKind.Utc).AddTicks(5925),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 755, DateTimeKind.Utc).AddTicks(591));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 247, DateTimeKind.Utc).AddTicks(2753),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 16, 17, 46, 8, 754, DateTimeKind.Utc).AddTicks(6301));

            migrationBuilder.AlterColumn<string>(
                name: "ResetPasswordToken",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 14, 10, 4, 36, 248, DateTimeKind.Utc).AddTicks(2734));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 14, 10, 4, 36, 248, DateTimeKind.Utc).AddTicks(2736));
        }
    }
}
