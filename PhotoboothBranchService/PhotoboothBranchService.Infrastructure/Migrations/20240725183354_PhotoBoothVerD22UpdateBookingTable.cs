using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerD22UpdateBookingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_SessionOrders_SessionOrderID",
                table: "PhotoSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicePackages_ServiceTypes_ServiceTypeID",
                table: "ServicePackages");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_SessionOrders_SessionOrderID",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "ServiceSessions");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "SessionOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicePackages",
                table: "ServicePackages");

            migrationBuilder.DropIndex(
                name: "IX_ServicePackages_ServiceTypeID",
                table: "ServicePackages");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"));

            migrationBuilder.DeleteData(
                table: "ServicePackages",
                keyColumn: "ServiceID",
                keyValue: new Guid("13e75fc2-f38b-401e-9cd2-c545b80fd1f0"));

            migrationBuilder.DeleteData(
                table: "ServicePackages",
                keyColumn: "ServiceID",
                keyValue: new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"));

            migrationBuilder.RenameColumn(
                name: "ServiceTypeID",
                table: "ServicePackages",
                newName: "ServicePackageID");

            migrationBuilder.RenameColumn(
                name: "ServiceName",
                table: "ServicePackages",
                newName: "PackageName");

            migrationBuilder.RenameColumn(
                name: "ServiceDescription",
                table: "ServicePackages",
                newName: "PackageDescription");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ServicePackages",
                newName: "PackagePrice");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerHour",
                table: "Booths",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicePackages",
                table: "ServicePackages",
                column: "ServicePackageID");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValidateCode = table.Column<long>(type: "bigint", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingType = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    CancelledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefundAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    BoothID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_Accounts_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Booths_BoothID",
                        column: x => x.BoothID,
                        principalTable: "Booths",
                        principalColumn: "BoothID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceID);
                });

            migrationBuilder.CreateTable(
                name: "BookingServices",
                columns: table => new
                {
                    BookingServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingServices", x => x.BookingServiceID);
                    table.ForeignKey(
                        name: "FK_BookingServices_Bookings_SessionOrderID",
                        column: x => x.SessionOrderID,
                        principalTable: "Bookings",
                        principalColumn: "BookingID");
                    table.ForeignKey(
                        name: "FK_BookingServices_ServicePackages_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "ServicePackages",
                        principalColumn: "ServicePackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("1671ccd8-d367-47c7-9c48-335da54ec34d"),
                column: "PricePerHour",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("28110b4a-bf04-4c04-a19b-1b91d976ee7c"),
                column: "PricePerHour",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("b8b615f3-a04f-4bbf-8ab2-f42dd69a65fd"),
                column: "PricePerHour",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("bc8c737b-9a92-49b2-b9b1-bd8321c7e594"),
                column: "PricePerHour",
                value: 0m);

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 7, 25, 18, 33, 54, 168, DateTimeKind.Utc).AddTicks(2615), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 7, 25, 18, 33, 54, 168, DateTimeKind.Utc).AddTicks(2618), "MoMo", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicePackages_ServiceID",
                table: "ServicePackages",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BoothID",
                table: "Bookings",
                column: "BoothID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerID",
                table: "Bookings",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingServices_ServiceID",
                table: "BookingServices",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingServices_SessionOrderID",
                table: "BookingServices",
                column: "SessionOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Bookings_SessionOrderID",
                table: "PhotoSessions",
                column: "SessionOrderID",
                principalTable: "Bookings",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicePackages_Services_ServiceID",
                table: "ServicePackages",
                column: "ServiceID",
                principalTable: "Services",
                principalColumn: "ServiceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Bookings_SessionOrderID",
                table: "Transactions",
                column: "SessionOrderID",
                principalTable: "Bookings",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Bookings_SessionOrderID",
                table: "PhotoSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicePackages_Services_ServiceID",
                table: "ServicePackages");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Bookings_SessionOrderID",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "BookingServices");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicePackages",
                table: "ServicePackages");

            migrationBuilder.DropIndex(
                name: "IX_ServicePackages_ServiceID",
                table: "ServicePackages");

            migrationBuilder.DropColumn(
                name: "PricePerHour",
                table: "Booths");

            migrationBuilder.RenameColumn(
                name: "PackagePrice",
                table: "ServicePackages",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PackageName",
                table: "ServicePackages",
                newName: "ServiceName");

            migrationBuilder.RenameColumn(
                name: "PackageDescription",
                table: "ServicePackages",
                newName: "ServiceDescription");

            migrationBuilder.RenameColumn(
                name: "ServicePackageID",
                table: "ServicePackages",
                newName: "ServiceTypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicePackages",
                table: "ServicePackages",
                column: "ServiceID");

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    ServiceTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.ServiceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "SessionOrders",
                columns: table => new
                {
                    SessionOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoothID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SessionPackageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValidateCode = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionOrders", x => x.SessionOrderID);
                    table.ForeignKey(
                        name: "FK_SessionOrders_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionOrders_Booths_BoothID",
                        column: x => x.BoothID,
                        principalTable: "Booths",
                        principalColumn: "BoothID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceSessions",
                columns: table => new
                {
                    ServiceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceSessions", x => x.ServiceItemID);
                    table.ForeignKey(
                        name: "FK_ServiceSessions_ServicePackages_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "ServicePackages",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceSessions_SessionOrders_SessionOrderID",
                        column: x => x.SessionOrderID,
                        principalTable: "SessionOrders",
                        principalColumn: "SessionOrderID");
                });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 24, 19, 35, 6, 522, DateTimeKind.Utc).AddTicks(2017));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 24, 19, 35, 6, 522, DateTimeKind.Utc).AddTicks(2025));

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "ServiceTypeID", "ServiceTypeName", "Status", "Unit" },
                values: new object[,]
                {
                    { new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), "Make up", 1, "Set" },
                    { new Guid("13bd9e6d-3092-496b-8025-530f5f9c43de"), "Hire booth", 1, "Minutes" },
                    { new Guid("70a5a1fd-9c0b-4109-9638-5b6e63e71eca"), "Print photo", 1, "Times" },
                    { new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"), "Send email", 1, "Times" }
                });

            migrationBuilder.InsertData(
                table: "ServicePackages",
                columns: new[] { "ServiceID", "Measure", "Price", "ServiceDescription", "ServiceName", "ServiceTypeID", "Status" },
                values: new object[,]
                {
                    { new Guid("13e75fc2-f38b-401e-9cd2-c545b80fd1f0"), 2, 190000m, "Make up with Korean stype for 2 people", "Combo make up with Korean stype for 2 people", new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), 0 },
                    { new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"), 1, 100000m, "Make up with Korean stype for 1 people", "Make up with Korean stype for 1 people", new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicePackages_ServiceTypeID",
                table: "ServicePackages",
                column: "ServiceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSessions_ServiceID",
                table: "ServiceSessions",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSessions_SessionOrderID",
                table: "ServiceSessions",
                column: "SessionOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_SessionOrders_AccountID",
                table: "SessionOrders",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_SessionOrders_BoothID",
                table: "SessionOrders",
                column: "BoothID");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_SessionOrders_SessionOrderID",
                table: "PhotoSessions",
                column: "SessionOrderID",
                principalTable: "SessionOrders",
                principalColumn: "SessionOrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicePackages_ServiceTypes_ServiceTypeID",
                table: "ServicePackages",
                column: "ServiceTypeID",
                principalTable: "ServiceTypes",
                principalColumn: "ServiceTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_SessionOrders_SessionOrderID",
                table: "Transactions",
                column: "SessionOrderID",
                principalTable: "SessionOrders",
                principalColumn: "SessionOrderID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
