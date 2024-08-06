using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Accounts_ManagerID",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_ManagerID",
                table: "Branches");

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
                name: "ManagerID",
                table: "Branches");

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"), new DateTime(2024, 8, 6, 3, 23, 33, 597, DateTimeKind.Utc).AddTicks(2143), "Cash", "Active" },
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 8, 6, 3, 23, 33, 597, DateTimeKind.Utc).AddTicks(2137), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 8, 6, 3, 23, 33, 597, DateTimeKind.Utc).AddTicks(2141), "MoMo", "Active" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ManagerID",
                table: "Branches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("0a1f2e05-f744-4d9b-937c-bfe7bad52a90"),
                column: "ManagerID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("b7fb8774-e3ac-4316-862c-23b81869c381"),
                column: "ManagerID",
                value: null);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 5, 10, 49, 20, 236, DateTimeKind.Utc).AddTicks(179));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 5, 10, 49, 20, 236, DateTimeKind.Utc).AddTicks(174));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 8, 5, 10, 49, 20, 236, DateTimeKind.Utc).AddTicks(178));

            migrationBuilder.CreateIndex(
                name: "IX_Branches_ManagerID",
                table: "Branches",
                column: "ManagerID",
                unique: true,
                filter: "[ManagerID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Accounts_ManagerID",
                table: "Branches",
                column: "ManagerID",
                principalTable: "Accounts",
                principalColumn: "AccountID");
        }
    }
}
