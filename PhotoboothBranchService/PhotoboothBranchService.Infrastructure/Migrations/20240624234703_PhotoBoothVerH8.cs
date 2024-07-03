using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Layouts_LayoutID",
                table: "PhotoSessions");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"));

            migrationBuilder.AlterColumn<Guid>(
                name: "LayoutID",
                table: "PhotoSessions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PhotoSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ValidateCode",
                table: "PhotoSessions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 6, 24, 23, 47, 3, 48, DateTimeKind.Utc).AddTicks(7008), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 6, 24, 23, 47, 3, 48, DateTimeKind.Utc).AddTicks(7020), "MoMo", "Active" }
                });

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "ServiceTypeID", "ServiceTypeName" },
                values: new object[] { new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"), "Relate to \"Take Photo\"" });

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Layouts_LayoutID",
                table: "PhotoSessions",
                column: "LayoutID",
                principalTable: "Layouts",
                principalColumn: "LayoutID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Layouts_LayoutID",
                table: "PhotoSessions");

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "ValidateCode",
                table: "PhotoSessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "LayoutID",
                table: "PhotoSessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 23, 16, 3, 9, 65, DateTimeKind.Utc).AddTicks(9377));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 23, 16, 3, 9, 65, DateTimeKind.Utc).AddTicks(9387));

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Layouts_LayoutID",
                table: "PhotoSessions",
                column: "LayoutID",
                principalTable: "Layouts",
                principalColumn: "LayoutID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
