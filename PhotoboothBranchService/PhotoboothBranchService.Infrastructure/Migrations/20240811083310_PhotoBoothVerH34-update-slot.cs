using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH34updateslot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refunds_Transactions_TransactionID",
                table: "Refunds");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Refunds_TransactionID",
                table: "Refunds");

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

            migrationBuilder.DropColumn(
                name: "GatewayTransactionID",
                table: "Refunds");

            migrationBuilder.DropColumn(
                name: "PricePerSlot",
                table: "Booths");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Slots",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionID",
                table: "Refunds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentID",
                table: "Refunds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PhotoBox",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "PhotoBox",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BookingSlots",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"), new DateTime(2024, 8, 11, 8, 33, 9, 947, DateTimeKind.Utc).AddTicks(237), "Cash", "Active" },
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 8, 11, 8, 33, 9, 947, DateTimeKind.Utc).AddTicks(232), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 8, 11, 8, 33, 9, 947, DateTimeKind.Utc).AddTicks(234), "MoMo", "Active" }
                });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("b3c4d5e6-a3d7-4edf-a3b9-34567890bcde"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 11, 15, 33, 9, 949, DateTimeKind.Local).AddTicks(2566));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("d1f4e0c1-1f62-4c9e-8c4a-123456789abc"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 11, 15, 33, 9, 949, DateTimeKind.Local).AddTicks(2549));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("f2e3d4c5-b2b7-4bcb-93c8-23456789defa"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 11, 15, 33, 9, 949, DateTimeKind.Local).AddTicks(2562));

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_PaymentID",
                table: "Refunds",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingID",
                table: "Payments",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodID",
                table: "Payments",
                column: "PaymentMethodID");

            migrationBuilder.AddForeignKey(
                name: "FK_Refunds_Payments_PaymentID",
                table: "Refunds",
                column: "PaymentID",
                principalTable: "Payments",
                principalColumn: "PaymentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refunds_Payments_PaymentID",
                table: "Refunds");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Refunds_PaymentID",
                table: "Refunds");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "PaymentID",
                table: "Refunds");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PhotoBox");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "PhotoBox");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BookingSlots");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionID",
                table: "Refunds",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "GatewayTransactionID",
                table: "Refunds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerSlot",
                table: "Booths",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GatewayTransactionID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("1671ccd8-d367-47c7-9c48-335da54ec34d"),
                column: "PricePerSlot",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("28110b4a-bf04-4c04-a19b-1b91d976ee7c"),
                column: "PricePerSlot",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("b8b615f3-a04f-4bbf-8ab2-f42dd69a65fd"),
                column: "PricePerSlot",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("bc8c737b-9a92-49b2-b9b1-bd8321c7e594"),
                column: "PricePerSlot",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 10, 10, 41, 41, 265, DateTimeKind.Utc).AddTicks(2659));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 10, 10, 41, 41, 265, DateTimeKind.Utc).AddTicks(2646));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 10, 10, 41, 41, 265, DateTimeKind.Utc).AddTicks(2655));

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_TransactionID",
                table: "Refunds",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BookingID",
                table: "Transactions",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentMethodID",
                table: "Transactions",
                column: "PaymentMethodID");

            migrationBuilder.AddForeignKey(
                name: "FK_Refunds_Transactions_TransactionID",
                table: "Refunds",
                column: "TransactionID",
                principalTable: "Transactions",
                principalColumn: "TransactionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
