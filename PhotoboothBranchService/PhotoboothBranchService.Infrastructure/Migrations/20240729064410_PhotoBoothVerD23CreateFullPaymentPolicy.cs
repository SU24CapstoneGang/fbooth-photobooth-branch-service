using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerD23CreateFullPaymentPolicy : Migration
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

            migrationBuilder.AddColumn<Guid>(
                name: "FullPaymentPolicyID",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FullPaymentPolicies",
                columns: table => new
                {
                    FullPaymentPolicyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PolicyDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RefundDaysBefore = table.Column<int>(type: "int", nullable: false),
                    NoCheckInTimeLimit = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullPaymentPolicies", x => x.FullPaymentPolicyID);
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 7, 29, 6, 44, 10, 560, DateTimeKind.Utc).AddTicks(6972), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 7, 29, 6, 44, 10, 560, DateTimeKind.Utc).AddTicks(6975), "MoMo", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FullPaymentPolicyID",
                table: "Bookings",
                column: "FullPaymentPolicyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_FullPaymentPolicies_FullPaymentPolicyID",
                table: "Bookings",
                column: "FullPaymentPolicyID",
                principalTable: "FullPaymentPolicies",
                principalColumn: "FullPaymentPolicyID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_FullPaymentPolicies_FullPaymentPolicyID",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "FullPaymentPolicies");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_FullPaymentPolicyID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FullPaymentPolicyID",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 25, 18, 33, 54, 168, DateTimeKind.Utc).AddTicks(2615));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 25, 18, 33, 54, 168, DateTimeKind.Utc).AddTicks(2618));
        }
    }
}
