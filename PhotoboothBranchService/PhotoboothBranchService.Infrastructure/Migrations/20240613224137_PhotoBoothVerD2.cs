using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerD2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Filters_FilterID",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionOrders_BoothBranches_PhotoBoothBranchID",
                table: "SessionOrders");

            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropIndex(
                name: "IX_Photos_FilterID",
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
                name: "UnitPrice",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "FilterID",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "PhotoBoothBranchID",
                table: "SessionOrders",
                newName: "BoothBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_SessionOrders_PhotoBoothBranchID",
                table: "SessionOrders",
                newName: "IX_SessionOrders_BoothBranchID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 604, DateTimeKind.Utc).AddTicks(4205),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 301, DateTimeKind.Utc).AddTicks(4519));

            migrationBuilder.AddColumn<string>(
                name: "CouldID",
                table: "Stickers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "SessionOrders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "SessionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 603, DateTimeKind.Utc).AddTicks(9934),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 301, DateTimeKind.Utc).AddTicks(1037));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Services",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "ServiceItems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<Guid>(
                name: "SessionOrderID",
                table: "ServiceItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PhotoSessionID",
                table: "ServiceItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "ServiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 602, DateTimeKind.Utc).AddTicks(62),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 286, DateTimeKind.Utc).AddTicks(5316));

            migrationBuilder.AddColumn<int>(
                name: "SessionIndex",
                table: "PhotoSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalPhotoTaken",
                table: "PhotoSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CouldID",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 594, DateTimeKind.Utc).AddTicks(2686),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 283, DateTimeKind.Utc).AddTicks(3871));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Layouts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LayoutURL",
                table: "Layouts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 593, DateTimeKind.Utc).AddTicks(4767),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 282, DateTimeKind.Utc).AddTicks(6470));

            migrationBuilder.AddColumn<string>(
                name: "CouldID",
                table: "Layouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Lenght",
                table: "Layouts",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<short>(
                name: "PhotoSlot",
                table: "Layouts",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "Layouts",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<string>(
                name: "FrameURL",
                table: "Frames",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 592, DateTimeKind.Utc).AddTicks(8484),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 282, DateTimeKind.Utc).AddTicks(1352));

            migrationBuilder.AddColumn<float>(
                name: "Lenght",
                table: "Frames",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "Frames",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 6, 13, 22, 41, 37, 594, DateTimeKind.Utc).AddTicks(3401), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 6, 13, 22, 41, 37, 594, DateTimeKind.Utc).AddTicks(3404), "MoMo", "Active" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SessionOrders_BoothBranches_BoothBranchID",
                table: "SessionOrders",
                column: "BoothBranchID",
                principalTable: "BoothBranches",
                principalColumn: "BoothBranchID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionOrders_BoothBranches_BoothBranchID",
                table: "SessionOrders");

            migrationBuilder.DropColumn(
                name: "CouldID",
                table: "Stickers");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "ServiceItems");

            migrationBuilder.DropColumn(
                name: "SessionIndex",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "TotalPhotoTaken",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "CouldID",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "CouldID",
                table: "Layouts");

            migrationBuilder.DropColumn(
                name: "Lenght",
                table: "Layouts");

            migrationBuilder.DropColumn(
                name: "PhotoSlot",
                table: "Layouts");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Layouts");

            migrationBuilder.DropColumn(
                name: "Lenght",
                table: "Frames");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Frames");

            migrationBuilder.RenameColumn(
                name: "BoothBranchID",
                table: "SessionOrders",
                newName: "PhotoBoothBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_SessionOrders_BoothBranchID",
                table: "SessionOrders",
                newName: "IX_SessionOrders_PhotoBoothBranchID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 301, DateTimeKind.Utc).AddTicks(4519),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 604, DateTimeKind.Utc).AddTicks(4205));

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "SessionOrders",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "SessionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 301, DateTimeKind.Utc).AddTicks(1037),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 603, DateTimeKind.Utc).AddTicks(9934));

            migrationBuilder.AddColumn<double>(
                name: "UnitPrice",
                table: "Services",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "ServiceItems",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "SessionOrderID",
                table: "ServiceItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PhotoSessionID",
                table: "ServiceItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 286, DateTimeKind.Utc).AddTicks(5316),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 602, DateTimeKind.Utc).AddTicks(62));

            migrationBuilder.AddColumn<Guid>(
                name: "FilterID",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 283, DateTimeKind.Utc).AddTicks(3871),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 594, DateTimeKind.Utc).AddTicks(2686));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Layouts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LayoutURL",
                table: "Layouts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 282, DateTimeKind.Utc).AddTicks(6470),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 593, DateTimeKind.Utc).AddTicks(4767));

            migrationBuilder.AlterColumn<string>(
                name: "FrameURL",
                table: "Frames",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 282, DateTimeKind.Utc).AddTicks(1352),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 41, 37, 592, DateTimeKind.Utc).AddTicks(8484));

            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    FilterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 281, DateTimeKind.Utc).AddTicks(8237)),
                    FilterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FilterURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.FilterID);
                });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 9, 20, 42, 25, 283, DateTimeKind.Utc).AddTicks(4613));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 9, 20, 42, 25, 283, DateTimeKind.Utc).AddTicks(4621));

            migrationBuilder.CreateIndex(
                name: "IX_Photos_FilterID",
                table: "Photos",
                column: "FilterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Filters_FilterID",
                table: "Photos",
                column: "FilterID",
                principalTable: "Filters",
                principalColumn: "FilterID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionOrders_BoothBranches_PhotoBoothBranchID",
                table: "SessionOrders",
                column: "PhotoBoothBranchID",
                principalTable: "BoothBranches",
                principalColumn: "BoothBranchID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
