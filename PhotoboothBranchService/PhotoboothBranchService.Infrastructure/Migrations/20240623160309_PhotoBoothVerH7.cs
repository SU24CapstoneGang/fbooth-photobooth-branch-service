using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Frames_FrameID",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "Frames");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"));

            migrationBuilder.RenameColumn(
                name: "StickerName",
                table: "Stickers",
                newName: "StickerCode");

            migrationBuilder.RenameColumn(
                name: "FrameID",
                table: "Photos",
                newName: "BackgroundID");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_FrameID",
                table: "Photos",
                newName: "IX_Photos_BackgroundID");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionID",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Signature",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "Amount",
                table: "Payment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ClientIpAddress",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LayoutCode",
                table: "Layouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "BoothBranches",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Backgrounds",
                columns: table => new
                {
                    BackgroundID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BackgroundCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BackgroundURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CouldID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lenght = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LayoutID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backgrounds", x => x.BackgroundID);
                    table.ForeignKey(
                        name: "FK_Backgrounds_Layouts_LayoutID",
                        column: x => x.LayoutID,
                        principalTable: "Layouts",
                        principalColumn: "LayoutID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 6, 23, 16, 3, 9, 65, DateTimeKind.Utc).AddTicks(9377), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 6, 23, 16, 3, 9, 65, DateTimeKind.Utc).AddTicks(9387), "MoMo", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Backgrounds_LayoutID",
                table: "Backgrounds",
                column: "LayoutID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Backgrounds_BackgroundID",
                table: "Photos",
                column: "BackgroundID",
                principalTable: "Backgrounds",
                principalColumn: "BackgroundID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Backgrounds_BackgroundID",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "Backgrounds");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "ClientIpAddress",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "LayoutCode",
                table: "Layouts");

            migrationBuilder.RenameColumn(
                name: "StickerCode",
                table: "Stickers",
                newName: "StickerName");

            migrationBuilder.RenameColumn(
                name: "BackgroundID",
                table: "Photos",
                newName: "FrameID");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_BackgroundID",
                table: "Photos",
                newName: "IX_Photos_FrameID");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionID",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Signature",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "BoothBranches",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    ThemeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThemeDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ThemeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.ThemeID);
                });

            migrationBuilder.CreateTable(
                name: "Frames",
                columns: table => new
                {
                    FrameID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThemeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CouldID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    FrameName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FrameURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Lenght = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frames", x => x.FrameID);
                    table.ForeignKey(
                        name: "FK_Frames_Themes_ThemeID",
                        column: x => x.ThemeID,
                        principalTable: "Themes",
                        principalColumn: "ThemeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 19, 3, 33, 29, 771, DateTimeKind.Utc).AddTicks(2419));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 19, 3, 33, 29, 771, DateTimeKind.Utc).AddTicks(2421));

            migrationBuilder.CreateIndex(
                name: "IX_Frames_ThemeID",
                table: "Frames",
                column: "ThemeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Frames_FrameID",
                table: "Photos",
                column: "FrameID",
                principalTable: "Frames",
                principalColumn: "FrameID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
