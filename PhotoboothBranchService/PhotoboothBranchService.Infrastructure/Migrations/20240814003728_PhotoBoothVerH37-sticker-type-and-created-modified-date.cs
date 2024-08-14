using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH37stickertypeandcreatedmodifieddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Photos",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "PaymentMethods",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Branches",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Booths",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "RefundAmount",
                table: "Bookings",
                newName: "RefundedAmount");

            migrationBuilder.AddColumn<Guid>(
                name: "StickerTypeID",
                table: "Stickers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Slots",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Slots",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Refunds",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Refunds",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "FullPaymentPolicies",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Devices",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BranchPhotos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "BranchPhotos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Branches",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Booths",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BoothPhotos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "BoothPhotos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.CreateTable(
                name: "StickerTypes",
                columns: table => new
                {
                    StickerTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StickerTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepresentImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CouldID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')"),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StickerTypes", x => x.StickerTypeID);
                });

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("1671ccd8-d367-47c7-9c48-335da54ec34d"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 151, DateTimeKind.Unspecified).AddTicks(9814));

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("28110b4a-bf04-4c04-a19b-1b91d976ee7c"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 151, DateTimeKind.Unspecified).AddTicks(9853));

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("b8b615f3-a04f-4bbf-8ab2-f42dd69a65fd"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 151, DateTimeKind.Unspecified).AddTicks(9844));

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("bc8c737b-9a92-49b2-b9b1-bd8321c7e594"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 151, DateTimeKind.Unspecified).AddTicks(9849));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("0a1f2e05-f744-4d9b-937c-bfe7bad52a90"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 154, DateTimeKind.Unspecified).AddTicks(7845));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("b7fb8774-e3ac-4316-862c-23b81869c381"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 154, DateTimeKind.Unspecified).AddTicks(7800));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 0, 37, 28, 154, DateTimeKind.Utc).AddTicks(79));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 0, 37, 28, 154, DateTimeKind.Utc).AddTicks(66));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 0, 37, 28, 154, DateTimeKind.Utc).AddTicks(69));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("b3c4d5e6-a3d7-4edf-a3b9-34567890bcde"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 157, DateTimeKind.Local).AddTicks(1930));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("d1f4e0c1-1f62-4c9e-8c4a-123456789abc"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 157, DateTimeKind.Local).AddTicks(1909));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("f2e3d4c5-b2b7-4bcb-93c8-23456789defa"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 157, DateTimeKind.Local).AddTicks(1926));

            migrationBuilder.CreateIndex(
                name: "IX_Stickers_StickerTypeID",
                table: "Stickers",
                column: "StickerTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stickers_StickerTypes_StickerTypeID",
                table: "Stickers",
                column: "StickerTypeID",
                principalTable: "StickerTypes",
                principalColumn: "StickerTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stickers_StickerTypes_StickerTypeID",
                table: "Stickers");

            migrationBuilder.DropTable(
                name: "StickerTypes");

            migrationBuilder.DropIndex(
                name: "IX_Stickers_StickerTypeID",
                table: "Stickers");

            migrationBuilder.DropColumn(
                name: "StickerTypeID",
                table: "Stickers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Refunds");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Refunds");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "FullPaymentPolicies");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BranchPhotos");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "BranchPhotos");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Booths");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BoothPhotos");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "BoothPhotos");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Photos",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "PaymentMethods",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Branches",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Booths",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "RefundedAmount",
                table: "Bookings",
                newName: "RefundAmount");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 12, 13, 54, 4, 694, DateTimeKind.Utc).AddTicks(3469));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 12, 13, 54, 4, 694, DateTimeKind.Utc).AddTicks(3465));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 12, 13, 54, 4, 694, DateTimeKind.Utc).AddTicks(3467));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("b3c4d5e6-a3d7-4edf-a3b9-34567890bcde"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 12, 20, 54, 4, 696, DateTimeKind.Local).AddTicks(6849));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("d1f4e0c1-1f62-4c9e-8c4a-123456789abc"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 12, 20, 54, 4, 696, DateTimeKind.Local).AddTicks(6832));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("f2e3d4c5-b2b7-4bcb-93c8-23456789defa"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 12, 20, 54, 4, 696, DateTimeKind.Local).AddTicks(6845));
        }
    }
}
