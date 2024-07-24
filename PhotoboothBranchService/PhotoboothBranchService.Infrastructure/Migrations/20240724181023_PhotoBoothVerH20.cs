using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refunds_Payment_PaymentID",
                table: "Refunds");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionOrders_SessionPackages_SessionPackageID",
                table: "SessionOrders");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "ServiceItems");

            migrationBuilder.DropTable(
                name: "SessionPackages");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropIndex(
                name: "IX_SessionOrders_SessionPackageID",
                table: "SessionOrders");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "ServiceTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Backgrounds",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Backgrounds",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateTable(
                name: "Constants",
                columns: table => new
                {
                    ConstantKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ConstantValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ConstantType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CanUpdateValue = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')"),
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
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Measure = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePackages", x => x.ServiceID);
                    table.ForeignKey(
                        name: "FK_ServicePackages_ServiceTypes_ServiceTypeID",
                        column: x => x.ServiceTypeID,
                        principalTable: "ServiceTypes",
                        principalColumn: "ServiceTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GatewayTransactionID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_SessionOrders_SessionOrderID",
                        column: x => x.SessionOrderID,
                        principalTable: "SessionOrders",
                        principalColumn: "SessionOrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceSessions",
                columns: table => new
                {
                    ServiceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 7, 24, 18, 10, 23, 439, DateTimeKind.Utc).AddTicks(8646), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 7, 24, 18, 10, 23, 439, DateTimeKind.Utc).AddTicks(8654), "MoMo", "Active" }
                });

            migrationBuilder.InsertData(
                table: "ServicePackages",
                columns: new[] { "ServiceID", "Measure", "Price", "ServiceDescription", "ServiceName", "ServiceTypeID", "Status" },
                values: new object[,]
                {
                    { new Guid("13e75fc2-f38b-401e-9cd2-c545b80fd1f0"), 2, 190000m, "Make up with Korean stype for 2 people", "Combo make up with Korean stype for 2 people", new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), 0 },
                    { new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"), 1, 100000m, "Make up with Korean stype for 1 people", "Make up with Korean stype for 1 people", new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), 0 }
                });

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"),
                columns: new[] { "Status", "Unit" },
                values: new object[] { 1, "Set" });

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"),
                columns: new[] { "ServiceTypeName", "Status", "Unit" },
                values: new object[] { "Send email", 1, "Times" });

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "ServiceTypeID", "ServiceTypeName", "Status", "Unit" },
                values: new object[,]
                {
                    { new Guid("13bd9e6d-3092-496b-8025-530f5f9c43de"), "Hire booth", 1, "Minutes" },
                    { new Guid("70a5a1fd-9c0b-4109-9638-5b6e63e71eca"), "Print photo", 1, "Times" }
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
                name: "IX_Transactions_PaymentMethodID",
                table: "Transactions",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SessionOrderID",
                table: "Transactions",
                column: "SessionOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Refunds_Transactions_PaymentID",
                table: "Refunds",
                column: "PaymentID",
                principalTable: "Transactions",
                principalColumn: "TransactionID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refunds_Transactions_PaymentID",
                table: "Refunds");

            migrationBuilder.DropTable(
                name: "Constants");

            migrationBuilder.DropTable(
                name: "ServiceSessions");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "ServicePackages");

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("13bd9e6d-3092-496b-8025-530f5f9c43de"));

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("70a5a1fd-9c0b-4109-9638-5b6e63e71eca"));

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "ServiceTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Stickers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Layouts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Backgrounds",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Backgrounds",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payment_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_SessionOrders_SessionOrderID",
                        column: x => x.SessionOrderID,
                        principalTable: "SessionOrders",
                        principalColumn: "SessionOrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Measure = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceID);
                    table.ForeignKey(
                        name: "FK_Services_ServiceTypes_ServiceTypeID",
                        column: x => x.ServiceTypeID,
                        principalTable: "ServiceTypes",
                        principalColumn: "ServiceTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionPackages",
                columns: table => new
                {
                    SessionPackageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duration = table.Column<short>(type: "smallint", nullable: false),
                    EmailSendCount = table.Column<short>(type: "smallint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrintCount = table.Column<short>(type: "smallint", nullable: false),
                    SessionPackageDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SessionPackageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionPackages", x => x.SessionPackageID);
                });

            migrationBuilder.CreateTable(
                name: "ServiceItems",
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
                    table.PrimaryKey("PK_ServiceItems", x => x.ServiceItemID);
                    table.ForeignKey(
                        name: "FK_ServiceItems_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceItems_SessionOrders_SessionOrderID",
                        column: x => x.SessionOrderID,
                        principalTable: "SessionOrders",
                        principalColumn: "SessionOrderID");
                });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 21, 22, 20, 24, 279, DateTimeKind.Utc).AddTicks(6573));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 21, 22, 20, 24, 279, DateTimeKind.Utc).AddTicks(6576));

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"),
                columns: new[] { "ServiceTypeName", "Status" },
                values: new object[] { "Relate to Take Photo", 0 });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceID", "Measure", "Price", "ServiceDescription", "ServiceName", "ServiceTypeID", "Status", "Unit" },
                values: new object[,]
                {
                    { new Guid("13e75fc2-f38b-401e-9cd2-c545b80fd1f0"), 2, 190000m, "Make up with Korean stype for 2 people", "Combo make up with Korean stype for 2 people", new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), 0, "people" },
                    { new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"), 1, 100000m, "Make up with Korean stype for 1 people", "Make up with Korean stype for 1 people", new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), 0, "people" }
                });

            migrationBuilder.InsertData(
                table: "SessionPackages",
                columns: new[] { "SessionPackageID", "Duration", "EmailSendCount", "Price", "PrintCount", "SessionPackageDescription", "SessionPackageName" },
                values: new object[,]
                {
                    { new Guid("041f73b6-7c08-4c63-8a08-aaebae219048"), (short)150, (short)50, 950000m, (short)50, "Package have 150 minutes in take photo, can print 50 photo and seend to email 50 times", "150 minutes special package " },
                    { new Guid("2fbb7633-5796-465a-98a6-f3c484811f24"), (short)60, (short)20, 440000m, (short)20, "Package have 60 minutes in take photo, can print 20 photo and seend to email 20 times", "60 minutes special package " },
                    { new Guid("306c5339-f453-4e2d-839e-4b3aacf17084"), (short)90, (short)10, 310000m, (short)10, "Package have 90 minutes in take photo, can print 10 photo and seend to email 10 times", "90 minutes mornal package" },
                    { new Guid("318dc257-983e-45d7-8dcb-10f348975c38"), (short)90, (short)20, 440000m, (short)20, "Package have 90 minutes in take photo, can print 20 photo and seend to email 20 times", "90 minutes special package" },
                    { new Guid("6c739b97-36d4-4559-8738-de8cc132b705"), (short)90, (short)50, 830000m, (short)50, "Package have 90 minutes in take photo, can print 50 photo and seend to email 50 times", "90 minutes special package PROMAX" },
                    { new Guid("f98b3003-c0ba-4305-a5ce-d8f77dd7310e"), (short)60, (short)10, 310000m, (short)10, "Package have 60 minutes in take photo, can print 10 photo and seend to email 10 times", "60 minutes normal package " }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionOrders_SessionPackageID",
                table: "SessionOrders",
                column: "SessionPackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentMethodID",
                table: "Payment",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_SessionOrderID",
                table: "Payment",
                column: "SessionOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_ServiceID",
                table: "ServiceItems",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_SessionOrderID",
                table: "ServiceItems",
                column: "SessionOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceTypeID",
                table: "Services",
                column: "ServiceTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Refunds_Payment_PaymentID",
                table: "Refunds",
                column: "PaymentID",
                principalTable: "Payment",
                principalColumn: "PaymentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionOrders_SessionPackages_SessionPackageID",
                table: "SessionOrders",
                column: "SessionPackageID",
                principalTable: "SessionPackages",
                principalColumn: "SessionPackageID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
