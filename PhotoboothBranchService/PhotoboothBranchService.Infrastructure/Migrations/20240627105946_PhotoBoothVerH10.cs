using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceItems_PhotoSessions_PhotoSessionID",
                table: "ServiceItems");

            migrationBuilder.DropIndex(
                name: "IX_ServiceItems_PhotoSessionID",
                table: "ServiceItems");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"));

            migrationBuilder.DropColumn(
                name: "PhotoSessionID",
                table: "ServiceItems");

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 6, 27, 10, 59, 46, 41, DateTimeKind.Utc).AddTicks(2300), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 6, 27, 10, 59, 46, 41, DateTimeKind.Utc).AddTicks(2302), "MoMo", "Active" }
                });

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"),
                column: "ServiceTypeName",
                value: "Relate to Take Photo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhotoSessionID",
                table: "ServiceItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 26, 6, 42, 28, 229, DateTimeKind.Utc).AddTicks(1811));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 26, 6, 42, 28, 229, DateTimeKind.Utc).AddTicks(1814));

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"),
                column: "ServiceTypeName",
                value: "Relate to \"Take Photo\"");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_PhotoSessionID",
                table: "ServiceItems",
                column: "PhotoSessionID");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceItems_PhotoSessions_PhotoSessionID",
                table: "ServiceItems",
                column: "PhotoSessionID",
                principalTable: "PhotoSessions",
                principalColumn: "PhotoSessionID");
        }
    }
}
