using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH12 : Migration
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

            migrationBuilder.RenameColumn(
                name: "StrickerURL",
                table: "Stickers",
                newName: "StickerURL");

            migrationBuilder.AddColumn<Guid>(
                name: "SessionPackageID",
                table: "SessionOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    BoothID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceID);
                    table.ForeignKey(
                        name: "FK_Devices_Booths_BoothID",
                        column: x => x.BoothID,
                        principalTable: "Booths",
                        principalColumn: "BoothID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionPackages",
                columns: table => new
                {
                    SessionPackageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionPackageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SessionPackageDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmailSendCount = table.Column<short>(type: "smallint", nullable: false),
                    PrintCount = table.Column<short>(type: "smallint", nullable: false),
                    Duration = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionPackages", x => x.SessionPackageID);
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 7, 2, 15, 22, 8, 99, DateTimeKind.Utc).AddTicks(5799), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 7, 2, 15, 22, 8, 99, DateTimeKind.Utc).AddTicks(5802), "MoMo", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionOrders_SessionPackageID",
                table: "SessionOrders",
                column: "SessionPackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_BoothID",
                table: "Devices",
                column: "BoothID");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionOrders_SessionPackages_SessionPackageID",
                table: "SessionOrders",
                column: "SessionPackageID",
                principalTable: "SessionPackages",
                principalColumn: "SessionPackageID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionOrders_SessionPackages_SessionPackageID",
                table: "SessionOrders");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "SessionPackages");

            migrationBuilder.DropIndex(
                name: "IX_SessionOrders_SessionPackageID",
                table: "SessionOrders");

            migrationBuilder.DropColumn(
                name: "SessionPackageID",
                table: "SessionOrders");

            migrationBuilder.RenameColumn(
                name: "StickerURL",
                table: "Stickers",
                newName: "StrickerURL");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 30, 7, 27, 6, 922, DateTimeKind.Utc).AddTicks(3150));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 30, 7, 27, 6, 922, DateTimeKind.Utc).AddTicks(3152));
        }
    }
}
