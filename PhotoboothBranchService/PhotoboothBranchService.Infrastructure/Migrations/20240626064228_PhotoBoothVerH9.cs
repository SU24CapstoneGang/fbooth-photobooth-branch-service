using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Backgrounds_BackgroundID",
                table: "Photos");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"));

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"));

            migrationBuilder.DropColumn(
                name: "ValidateCode",
                table: "PhotoSessions");

            migrationBuilder.AddColumn<int>(
                name: "stickerLength",
                table: "Stickers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "stickerWidth",
                table: "Stickers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ValidateCode",
                table: "SessionOrders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<Guid>(
                name: "BackgroundID",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 6, 26, 6, 42, 28, 229, DateTimeKind.Utc).AddTicks(1811), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 6, 26, 6, 42, 28, 229, DateTimeKind.Utc).AddTicks(1814), "MoMo", "Active" }
                });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("30c2a7fb-9164-4164-9f1b-7c334744e559"),
                column: "ServiceTypeID",
                value: new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("9b0dc075-557c-4b29-b3ba-c8b5841c4c68"),
                column: "ServiceTypeID",
                value: new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("c51a05bb-28af-4315-b888-606376ba061b"),
                column: "ServiceTypeID",
                value: new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"));

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Backgrounds_BackgroundID",
                table: "Photos",
                column: "BackgroundID",
                principalTable: "Backgrounds",
                principalColumn: "BackgroundID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Backgrounds_BackgroundID",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "stickerLength",
                table: "Stickers");

            migrationBuilder.DropColumn(
                name: "stickerWidth",
                table: "Stickers");

            migrationBuilder.DropColumn(
                name: "ValidateCode",
                table: "SessionOrders");

            migrationBuilder.AddColumn<long>(
                name: "ValidateCode",
                table: "PhotoSessions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<Guid>(
                name: "BackgroundID",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 24, 23, 47, 3, 48, DateTimeKind.Utc).AddTicks(7008));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 24, 23, 47, 3, 48, DateTimeKind.Utc).AddTicks(7020));

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "ServiceTypeID", "ServiceTypeName" },
                values: new object[] { new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"), "Take photo" });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("30c2a7fb-9164-4164-9f1b-7c334744e559"),
                column: "ServiceTypeID",
                value: new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("9b0dc075-557c-4b29-b3ba-c8b5841c4c68"),
                column: "ServiceTypeID",
                value: new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("c51a05bb-28af-4315-b888-606376ba061b"),
                column: "ServiceTypeID",
                value: new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"));

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Backgrounds_BackgroundID",
                table: "Photos",
                column: "BackgroundID",
                principalTable: "Backgrounds",
                principalColumn: "BackgroundID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
