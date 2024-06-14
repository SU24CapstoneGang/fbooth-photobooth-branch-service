using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Layouts_LayoutID",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionOrders_BoothBranches_BoothBranchID",
                table: "SessionOrders");

            migrationBuilder.DropIndex(
                name: "IX_SessionOrders_BoothBranchID",
                table: "SessionOrders");

            migrationBuilder.DropIndex(
                name: "IX_Photos_LayoutID",
                table: "Photos");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"));

            migrationBuilder.DropColumn(
                name: "BoothBranchID",
                table: "SessionOrders");

            migrationBuilder.DropColumn(
                name: "LayoutID",
                table: "Photos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 255, DateTimeKind.Utc).AddTicks(8024),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 515, DateTimeKind.Utc).AddTicks(2417));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "SessionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 255, DateTimeKind.Utc).AddTicks(3774),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 514, DateTimeKind.Utc).AddTicks(8316));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 251, DateTimeKind.Utc).AddTicks(2372),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 513, DateTimeKind.Utc).AddTicks(4960));

            migrationBuilder.AddColumn<Guid>(
                name: "LayoutID",
                table: "PhotoSessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 251, DateTimeKind.Utc).AddTicks(343),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 513, DateTimeKind.Utc).AddTicks(2767));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 248, DateTimeKind.Utc).AddTicks(2183),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 510, DateTimeKind.Utc).AddTicks(2134));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 247, DateTimeKind.Utc).AddTicks(5925),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 509, DateTimeKind.Utc).AddTicks(4776));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 247, DateTimeKind.Utc).AddTicks(2753),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 509, DateTimeKind.Utc).AddTicks(1463));

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 6, 14, 10, 4, 36, 248, DateTimeKind.Utc).AddTicks(2734), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 6, 14, 10, 4, 36, 248, DateTimeKind.Utc).AddTicks(2736), "MoMo", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoSessions_LayoutID",
                table: "PhotoSessions",
                column: "LayoutID");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Layouts_LayoutID",
                table: "PhotoSessions",
                column: "LayoutID",
                principalTable: "Layouts",
                principalColumn: "LayoutID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Layouts_LayoutID",
                table: "PhotoSessions");

            migrationBuilder.DropIndex(
                name: "IX_PhotoSessions_LayoutID",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "LayoutID",
                table: "PhotoSessions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 515, DateTimeKind.Utc).AddTicks(2417),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 255, DateTimeKind.Utc).AddTicks(8024));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "SessionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 514, DateTimeKind.Utc).AddTicks(8316),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 255, DateTimeKind.Utc).AddTicks(3774));

            migrationBuilder.AddColumn<Guid>(
                name: "BoothBranchID",
                table: "SessionOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 513, DateTimeKind.Utc).AddTicks(4960),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 251, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 513, DateTimeKind.Utc).AddTicks(2767),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 251, DateTimeKind.Utc).AddTicks(343));

            migrationBuilder.AddColumn<Guid>(
                name: "LayoutID",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 510, DateTimeKind.Utc).AddTicks(2134),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 248, DateTimeKind.Utc).AddTicks(2183));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 509, DateTimeKind.Utc).AddTicks(4776),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 247, DateTimeKind.Utc).AddTicks(5925));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 509, DateTimeKind.Utc).AddTicks(1463),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 247, DateTimeKind.Utc).AddTicks(2753));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 14, 9, 42, 11, 510, DateTimeKind.Utc).AddTicks(2756));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 14, 9, 42, 11, 510, DateTimeKind.Utc).AddTicks(2759));

            migrationBuilder.CreateIndex(
                name: "IX_SessionOrders_BoothBranchID",
                table: "SessionOrders",
                column: "BoothBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_LayoutID",
                table: "Photos",
                column: "LayoutID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Layouts_LayoutID",
                table: "Photos",
                column: "LayoutID",
                principalTable: "Layouts",
                principalColumn: "LayoutID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionOrders_BoothBranches_BoothBranchID",
                table: "SessionOrders",
                column: "BoothBranchID",
                principalTable: "BoothBranches",
                principalColumn: "BoothBranchID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
