using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingServices_Bookings_SessionOrderID",
                table: "BookingServices");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingServices_ServicePackages_ServiceID",
                table: "BookingServices");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Bookings_SessionOrderID",
                table: "PhotoSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Bookings_SessionOrderID",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Constants");

            migrationBuilder.DropTable(
                name: "ServicePackages");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"));

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "SessionOrderID",
                table: "Transactions",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SessionOrderID",
                table: "Transactions",
                newName: "IX_Transactions_BookingID");

            migrationBuilder.RenameColumn(
                name: "SessionOrderID",
                table: "PhotoSessions",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoSessions_SessionOrderID",
                table: "PhotoSessions",
                newName: "IX_PhotoSessions_BookingID");

            migrationBuilder.RenameColumn(
                name: "SessionOrderID",
                table: "BookingServices",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingServices_SessionOrderID",
                table: "BookingServices",
                newName: "IX_BookingServices_BookingID");

            migrationBuilder.AddColumn<int>(
                name: "TransactionStatus",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Layouts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Booths",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "isBooked",
                table: "Booths",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("1671ccd8-d367-47c7-9c48-335da54ec34d"),
                columns: new[] { "Status", "isBooked" },
                values: new object[] { 1, false });

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("28110b4a-bf04-4c04-a19b-1b91d976ee7c"),
                columns: new[] { "Status", "isBooked" },
                values: new object[] { 1, false });

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("b8b615f3-a04f-4bbf-8ab2-f42dd69a65fd"),
                columns: new[] { "Status", "isBooked" },
                values: new object[] { 1, false });

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("bc8c737b-9a92-49b2-b9b1-bd8321c7e594"),
                columns: new[] { "Status", "isBooked" },
                values: new object[] { 1, false });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("0a1f2e05-f744-4d9b-937c-bfe7bad52a90"),
                columns: new[] { "ClosingTime", "OpeningTime" },
                values: new object[] { new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("b7fb8774-e3ac-4316-862c-23b81869c381"),
                columns: new[] { "ClosingTime", "OpeningTime" },
                values: new object[] { new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Layouts",
                columns: new[] { "LayoutID", "CouldID", "CreatedDate", "Height", "LayoutCode", "LayoutURL", "PhotoSlot", "Status", "Width" },
                values: new object[,]
                {
                    { new Guid("1306f740-c718-41bb-d91c-08dca62b7b83"), "FBooth-Layout/fwuzc2ba67ysibm5gccj", new DateTime(2024, 7, 17, 13, 41, 26, 770, DateTimeKind.Unspecified), 7100, "7-01", "https://res.cloudinary.com/dfxvccyje/image/upload/v1721198486/FBooth-Layout/fwuzc2ba67ysibm5gccj.png", (short)3, 1, 4730 },
                    { new Guid("1799860e-e239-47c6-c5a1-08dca65d7432"), "FBooth-Layout/d2rcoxdzla53ngpzarv9", new DateTime(2024, 7, 17, 19, 39, 9, 340, DateTimeKind.Unspecified), 4730, "4-01", "https://res.cloudinary.com/dfxvccyje/image/upload/v1721219948/FBooth-Layout/d2rcoxdzla53ngpzarv9.png", (short)1, 1, 7090 },
                    { new Guid("2920f5b2-2cbf-4ed3-7ff3-08dca62a5d8b"), "FBooth-Layout/frtwksfwbszafz8c7h3b", new DateTime(2024, 7, 17, 13, 37, 40, 750, DateTimeKind.Unspecified), 4730, "11-01", "https://res.cloudinary.com/dfxvccyje/image/upload/v1721198261/FBooth-Layout/frtwksfwbszafz8c7h3b.png", (short)4, 1, 7090 },
                    { new Guid("4a071101-a8fd-42dd-d91d-08dca62b7b83"), "FBooth-Layout/gjes09uywe0uv3veuz8s", new DateTime(2024, 7, 17, 13, 41, 42, 850, DateTimeKind.Unspecified), 4730, "9-01", "https://res.cloudinary.com/dfxvccyje/image/upload/v1721198502/FBooth-Layout/gjes09uywe0uv3veuz8s.png", (short)3, 1, 7090 },
                    { new Guid("e1bb7b30-909b-491a-7f64-08dcaa6cbb13"), "FBooth-Layout/hucotkisjh7y94sirnza", new DateTime(2024, 7, 22, 23, 38, 35, 183, DateTimeKind.Unspecified).AddTicks(3333), 1920, "Black Vintage Photo Film Your Story (3)", "https://res.cloudinary.com/dfxvccyje/image/upload/v1721666315/FBooth-Layout/hucotkisjh7y94sirnza.png", (short)1, 1, 1080 }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 7, 30, 8, 37, 44, 288, DateTimeKind.Utc).AddTicks(3732), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 7, 30, 8, 37, 44, 288, DateTimeKind.Utc).AddTicks(3734), "MoMo", "Active" }
                });

            migrationBuilder.InsertData(
                table: "Backgrounds",
                columns: new[] { "BackgroundID", "BackgroundCode", "BackgroundURL", "CouldID", "CreatedDate", "Height", "LayoutID", "Status", "Width" },
                values: new object[,]
                {
                    { new Guid("7df5c376-e2c7-47e5-bd86-558b7c7b1fd0"), "7-01.png", "https://res.cloudinary.com/dfxvccyje/image/upload/v1721666189/FBooth-Background/hughd2ixfiieefwniuwk.png", "FBooth-Background/hughd2ixfiieefwniuwk", new DateTime(2024, 7, 22, 23, 36, 22, 316, DateTimeKind.Unspecified).AddTicks(6667), 710, new Guid("1306f740-c718-41bb-d91c-08dca62b7b83"), "Available", 473 },
                    { new Guid("867f6d24-6e2e-4fbd-a718-4c2c891fd826"), "4-01.png", "https://res.cloudinary.com/dfxvccyje/image/upload/v1721666311/FBooth-Background/c1jbpjf0xdw4lysrnrbm.png", "FBooth-Background/c1jbpjf0xdw4lysrnrbm", new DateTime(2024, 7, 22, 23, 36, 38, 846, DateTimeKind.Unspecified).AddTicks(6667), 473, new Guid("1799860e-e239-47c6-c5a1-08dca65d7432"), "Available", 709 },
                    { new Guid("b15f7e3c-ec3f-4f37-9a4d-4693bd0fa05f"), "11-01.png", "https://res.cloudinary.com/dfxvccyje/image/upload/v1721666183/FBooth-Background/jqdwh9a2y5vct3qunu7a.png", "FBooth-Background/jqdwh9a2y5vct3qunu7a", new DateTime(2024, 7, 22, 23, 36, 28, 216, DateTimeKind.Unspecified).AddTicks(6667), 473, new Guid("2920f5b2-2cbf-4ed3-7ff3-08dca62a5d8b"), "Available", 709 },
                    { new Guid("fd3d39c7-62c6-4e4e-92a8-4e22a911b511"), "9-01.png", "https://res.cloudinary.com/dfxvccyje/image/upload/v1721666194/FBooth-Background/wnnwcfvfdqaqiih3rikm.png", "FBooth-Background/wnnwcfvfdqaqiih3rikm", new DateTime(2024, 7, 22, 23, 36, 34, 296, DateTimeKind.Unspecified).AddTicks(6667), 473, new Guid("4a071101-a8fd-42dd-d91d-08dca62b7b83"), "Available", 709 }
                });

            migrationBuilder.InsertData(
                table: "PhotoBox",
                columns: new[] { "PhotoBoxID", "BoxHeight", "BoxIndex", "BoxWidth", "CoordinatesX", "CoordinatesY", "IsLandscape", "LayoutID" },
                values: new object[,]
                {
                    { new Guid("170db2dc-bfa8-4148-b85e-75b52a72b5d4"), 1641, 3, 2101, 4780, 2940, false, new Guid("2920f5b2-2cbf-4ed3-7ff3-08dca62a5d8b") },
                    { new Guid("1e1c3d0b-b7d3-4dc8-9ebd-758f44fef53a"), 1641, 2, 2111, 2480, 2940, false, new Guid("2920f5b2-2cbf-4ed3-7ff3-08dca62a5d8b") },
                    { new Guid("23cb58a4-d569-414d-93f6-96c3e6c60614"), 3170, 0, 4430, 1830, 1630, false, new Guid("4a071101-a8fd-42dd-d91d-08dca62b7b83") },
                    { new Guid("54e510a7-d789-4cf5-ae64-025c38a2ff58"), 1370, 1, 4430, 1830, 5100, false, new Guid("4a071101-a8fd-42dd-d91d-08dca62b7b83") },
                    { new Guid("56d05b8f-6cf4-42f2-a9aa-7d96065b4c94"), 2370, 2, 3460, 3480, 5680, false, new Guid("1306f740-c718-41bb-d91c-08dca62b7b83") },
                    { new Guid("5fc689f6-8890-43a6-9d7d-59a9c5e10bc3"), 5080, 0, 3460, 160, 340, false, new Guid("1306f740-c718-41bb-d91c-08dca62b7b83") },
                    { new Guid("74f5f643-3a94-402d-b139-3b2543255f7c"), 2631, 0, 3921, 190, 170, false, new Guid("2920f5b2-2cbf-4ed3-7ff3-08dca62a5d8b") },
                    { new Guid("7cb6bbf5-2fef-47e1-9c28-82b9c599fdd0"), 1370, 2, 4430, 1830, 6810, false, new Guid("4a071101-a8fd-42dd-d91d-08dca62b7b83") },
                    { new Guid("832d5af1-9897-4a6d-a98a-77443df1b4e2"), 3686, 0, 5529, 1000, 1022, false, new Guid("1799860e-e239-47c6-c5a1-08dca65d7432") },
                    { new Guid("8e69c79f-09ee-495e-861e-0c60207144d7"), 1641, 1, 2111, 190, 2940, false, new Guid("2920f5b2-2cbf-4ed3-7ff3-08dca62a5d8b") },
                    { new Guid("c98c89af-738e-4034-88a2-9a8982fc22d5"), 2370, 1, 3460, 160, 5680, false, new Guid("1306f740-c718-41bb-d91c-08dca62b7b83") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingServices_Bookings_BookingID",
                table: "BookingServices",
                column: "BookingID",
                principalTable: "Bookings",
                principalColumn: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingServices_Services_ServiceID",
                table: "BookingServices",
                column: "ServiceID",
                principalTable: "Services",
                principalColumn: "ServiceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Bookings_BookingID",
                table: "PhotoSessions",
                column: "BookingID",
                principalTable: "Bookings",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Bookings_BookingID",
                table: "Transactions",
                column: "BookingID",
                principalTable: "Bookings",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingServices_Bookings_BookingID",
                table: "BookingServices");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingServices_Services_ServiceID",
                table: "BookingServices");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Bookings_BookingID",
                table: "PhotoSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Bookings_BookingID",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Backgrounds",
                keyColumn: "BackgroundID",
                keyValue: new Guid("7df5c376-e2c7-47e5-bd86-558b7c7b1fd0"));

            migrationBuilder.DeleteData(
                table: "Backgrounds",
                keyColumn: "BackgroundID",
                keyValue: new Guid("867f6d24-6e2e-4fbd-a718-4c2c891fd826"));

            migrationBuilder.DeleteData(
                table: "Backgrounds",
                keyColumn: "BackgroundID",
                keyValue: new Guid("b15f7e3c-ec3f-4f37-9a4d-4693bd0fa05f"));

            migrationBuilder.DeleteData(
                table: "Backgrounds",
                keyColumn: "BackgroundID",
                keyValue: new Guid("fd3d39c7-62c6-4e4e-92a8-4e22a911b511"));

            migrationBuilder.DeleteData(
                table: "Layouts",
                keyColumn: "LayoutID",
                keyValue: new Guid("e1bb7b30-909b-491a-7f64-08dcaa6cbb13"));

            migrationBuilder.DeleteData(
                table: "PhotoBox",
                keyColumn: "PhotoBoxID",
                keyValue: new Guid("170db2dc-bfa8-4148-b85e-75b52a72b5d4"));

            migrationBuilder.DeleteData(
                table: "PhotoBox",
                keyColumn: "PhotoBoxID",
                keyValue: new Guid("1e1c3d0b-b7d3-4dc8-9ebd-758f44fef53a"));

            migrationBuilder.DeleteData(
                table: "PhotoBox",
                keyColumn: "PhotoBoxID",
                keyValue: new Guid("23cb58a4-d569-414d-93f6-96c3e6c60614"));

            migrationBuilder.DeleteData(
                table: "PhotoBox",
                keyColumn: "PhotoBoxID",
                keyValue: new Guid("54e510a7-d789-4cf5-ae64-025c38a2ff58"));

            migrationBuilder.DeleteData(
                table: "PhotoBox",
                keyColumn: "PhotoBoxID",
                keyValue: new Guid("56d05b8f-6cf4-42f2-a9aa-7d96065b4c94"));

            migrationBuilder.DeleteData(
                table: "PhotoBox",
                keyColumn: "PhotoBoxID",
                keyValue: new Guid("5fc689f6-8890-43a6-9d7d-59a9c5e10bc3"));

            migrationBuilder.DeleteData(
                table: "PhotoBox",
                keyColumn: "PhotoBoxID",
                keyValue: new Guid("74f5f643-3a94-402d-b139-3b2543255f7c"));

            migrationBuilder.DeleteData(
                table: "PhotoBox",
                keyColumn: "PhotoBoxID",
                keyValue: new Guid("7cb6bbf5-2fef-47e1-9c28-82b9c599fdd0"));

            migrationBuilder.DeleteData(
                table: "PhotoBox",
                keyColumn: "PhotoBoxID",
                keyValue: new Guid("832d5af1-9897-4a6d-a98a-77443df1b4e2"));

            migrationBuilder.DeleteData(
                table: "PhotoBox",
                keyColumn: "PhotoBoxID",
                keyValue: new Guid("8e69c79f-09ee-495e-861e-0c60207144d7"));

            migrationBuilder.DeleteData(
                table: "PhotoBox",
                keyColumn: "PhotoBoxID",
                keyValue: new Guid("c98c89af-738e-4034-88a2-9a8982fc22d5"));

            migrationBuilder.DeleteData(
                table: "Layouts",
                keyColumn: "LayoutID",
                keyValue: new Guid("1306f740-c718-41bb-d91c-08dca62b7b83"));

            migrationBuilder.DeleteData(
                table: "Layouts",
                keyColumn: "LayoutID",
                keyValue: new Guid("1799860e-e239-47c6-c5a1-08dca65d7432"));

            migrationBuilder.DeleteData(
                table: "Layouts",
                keyColumn: "LayoutID",
                keyValue: new Guid("2920f5b2-2cbf-4ed3-7ff3-08dca62a5d8b"));

            migrationBuilder.DeleteData(
                table: "Layouts",
                keyColumn: "LayoutID",
                keyValue: new Guid("4a071101-a8fd-42dd-d91d-08dca62b7b83"));

            migrationBuilder.DropColumn(
                name: "TransactionStatus",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "isBooked",
                table: "Booths");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                table: "Transactions",
                newName: "SessionOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_BookingID",
                table: "Transactions",
                newName: "IX_Transactions_SessionOrderID");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                table: "PhotoSessions",
                newName: "SessionOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoSessions_BookingID",
                table: "PhotoSessions",
                newName: "IX_PhotoSessions_SessionOrderID");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                table: "BookingServices",
                newName: "SessionOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingServices_BookingID",
                table: "BookingServices",
                newName: "IX_BookingServices_SessionOrderID");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Layouts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Booths",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Constants",
                columns: table => new
                {
                    ConstantKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CanUpdateValue = table.Column<bool>(type: "bit", nullable: false),
                    ConstantType = table.Column<int>(type: "int", nullable: false),
                    ConstantValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')"),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constants", x => x.ConstantKey);
                });

            migrationBuilder.CreateTable(
                name: "ServicePackages",
                columns: table => new
                {
                    ServicePackageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Measure = table.Column<int>(type: "int", nullable: false),
                    PackageDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackagePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePackages", x => x.ServicePackageID);
                    table.ForeignKey(
                        name: "FK_ServicePackages_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("1671ccd8-d367-47c7-9c48-335da54ec34d"),
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("28110b4a-bf04-4c04-a19b-1b91d976ee7c"),
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("b8b615f3-a04f-4bbf-8ab2-f42dd69a65fd"),
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("bc8c737b-9a92-49b2-b9b1-bd8321c7e594"),
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("0a1f2e05-f744-4d9b-937c-bfe7bad52a90"),
                columns: new[] { "ClosingTime", "OpeningTime" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("b7fb8774-e3ac-4316-862c-23b81869c381"),
                columns: new[] { "ClosingTime", "OpeningTime" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Constants",
                columns: new[] { "ConstantKey", "CanUpdateValue", "ConstantType", "ConstantValue", "Description", "DisplayName" },
                values: new object[,]
                {
                    { "BookingDeadline", true, 1, "45", "The start time for the session order must be at least \"value\" minutes after now. The value is measured in minutes.", "Booking deadline" },
                    { "BoothReservationHold", true, 1, "15", "Time to hold the booth if customer not checkin and pay the rest of bill. Then the order will change to Cancel status.", "Booth reservation hold time" },
                    { "CancelDeadlineRefund", true, 1, "30", "The time to cancel the session order must before \"value\" minutes start time. The value is measured in minutes. If not meet condition, the cancel will not refund transactions.", "Cancel deadline to refund" },
                    { "CloseTime", false, 6, "23:00", "Open time in a day of system, using 24-hour format. With form hh:mm.", "Close time" },
                    { "DepositPercent", true, 1, "20", "Percent value of total price when customer choose deposit the session order.", "Deposit percent" },
                    { "OpenTime", false, 6, "8:00", "Open time in a day of system, using 24-hour format. With form hh:mm.", "Open time" },
                    { "RefundPercent", true, 1, "50", "Percent value of total price when customer cancel the session order.", "Refund percent" }
                });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 29, 17, 54, 25, 24, DateTimeKind.Utc).AddTicks(794));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 29, 17, 54, 25, 24, DateTimeKind.Utc).AddTicks(796));

            migrationBuilder.CreateIndex(
                name: "IX_ServicePackages_ServiceID",
                table: "ServicePackages",
                column: "ServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingServices_Bookings_SessionOrderID",
                table: "BookingServices",
                column: "SessionOrderID",
                principalTable: "Bookings",
                principalColumn: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingServices_ServicePackages_ServiceID",
                table: "BookingServices",
                column: "ServiceID",
                principalTable: "ServicePackages",
                principalColumn: "ServicePackageID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Bookings_SessionOrderID",
                table: "PhotoSessions",
                column: "SessionOrderID",
                principalTable: "Bookings",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Bookings_SessionOrderID",
                table: "Transactions",
                column: "SessionOrderID",
                principalTable: "Bookings",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
