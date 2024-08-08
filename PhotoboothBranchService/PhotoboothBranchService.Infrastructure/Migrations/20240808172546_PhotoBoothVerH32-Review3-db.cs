using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH32Review3db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"));

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
                name: "IsPermanentPolicy",
                table: "FullPaymentPolicies");

            migrationBuilder.DropColumn(
                name: "NoCheckInTimeLimit",
                table: "FullPaymentPolicies");

            migrationBuilder.DropColumn(
                name: "isBooked",
                table: "Booths");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "PricePerHour",
                table: "Booths",
                newName: "PricePerSlot");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Bookings",
                newName: "BookingStatus");

            migrationBuilder.RenameColumn(
                name: "CustomerReferenceID",
                table: "Bookings",
                newName: "CustomerBusinessID");

            migrationBuilder.AddColumn<int>(
                name: "CheckInTimeLimit",
                table: "FullPaymentPolicies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BoothPhotos",
                columns: table => new
                {
                    BoothPhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoothPhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CouldID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoothID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoothPhotos", x => x.BoothPhotoId);
                    table.ForeignKey(
                        name: "FK_BoothPhotos_Booths_BoothID",
                        column: x => x.BoothID,
                        principalTable: "Booths",
                        principalColumn: "BoothID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchPhotos",
                columns: table => new
                {
                    BranchPhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchPhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CouldID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchPhotos", x => x.BranchPhotoId);
                    table.ForeignKey(
                        name: "FK_BranchPhotos_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    SlotID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SlotStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    SlotEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BoothID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.SlotID);
                    table.ForeignKey(
                        name: "FK_Slots_Booths_BoothID",
                        column: x => x.BoothID,
                        principalTable: "Booths",
                        principalColumn: "BoothID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingSlots",
                columns: table => new
                {
                    BookingSlotID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "date", nullable: false),
                    BookingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SlotID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingSlots", x => x.BookingSlotID);
                    table.ForeignKey(
                        name: "FK_BookingSlots_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingSlots_Slots_SlotID",
                        column: x => x.SlotID,
                        principalTable: "Slots",
                        principalColumn: "SlotID");
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"), new DateTime(2024, 8, 8, 17, 25, 46, 59, DateTimeKind.Utc).AddTicks(3227), "Cash", "Active" },
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 8, 8, 17, 25, 46, 59, DateTimeKind.Utc).AddTicks(3223), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 8, 8, 17, 25, 46, 59, DateTimeKind.Utc).AddTicks(3225), "MoMo", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingSlots_BookingID",
                table: "BookingSlots",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSlots_SlotID",
                table: "BookingSlots",
                column: "SlotID");

            migrationBuilder.CreateIndex(
                name: "IX_BoothPhotos_BoothID",
                table: "BoothPhotos",
                column: "BoothID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchPhotos_BranchID",
                table: "BranchPhotos",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_BoothID",
                table: "Slots",
                column: "BoothID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingSlots");

            migrationBuilder.DropTable(
                name: "BoothPhotos");

            migrationBuilder.DropTable(
                name: "BranchPhotos");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropColumn(
                name: "CheckInTimeLimit",
                table: "FullPaymentPolicies");

            migrationBuilder.RenameColumn(
                name: "PricePerSlot",
                table: "Booths",
                newName: "PricePerHour");

            migrationBuilder.RenameColumn(
                name: "CustomerBusinessID",
                table: "Bookings",
                newName: "CustomerReferenceID");

            migrationBuilder.RenameColumn(
                name: "BookingStatus",
                table: "Bookings",
                newName: "Status");

            migrationBuilder.AddColumn<bool>(
                name: "IsPermanentPolicy",
                table: "FullPaymentPolicies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoCheckInTimeLimit",
                table: "FullPaymentPolicies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isBooked",
                table: "Booths",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("1671ccd8-d367-47c7-9c48-335da54ec34d"),
                column: "isBooked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("28110b4a-bf04-4c04-a19b-1b91d976ee7c"),
                column: "isBooked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("b8b615f3-a04f-4bbf-8ab2-f42dd69a65fd"),
                column: "isBooked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("bc8c737b-9a92-49b2-b9b1-bd8321c7e594"),
                column: "isBooked",
                value: false);

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

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 6, 3, 23, 33, 597, DateTimeKind.Utc).AddTicks(2143));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 6, 3, 23, 33, 597, DateTimeKind.Utc).AddTicks(2137));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 6, 3, 23, 33, 597, DateTimeKind.Utc).AddTicks(2141));

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
        }
    }
}
