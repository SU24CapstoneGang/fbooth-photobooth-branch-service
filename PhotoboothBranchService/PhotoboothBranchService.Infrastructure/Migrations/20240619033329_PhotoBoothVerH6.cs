using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH6 : Migration
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
                name: "ThemeFrameDescription",
                table: "Themes",
                newName: "ThemeDescription");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 255, DateTimeKind.Utc).AddTicks(8024));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "SessionOrders",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 255, DateTimeKind.Utc).AddTicks(3774));

            migrationBuilder.AddColumn<int>(
                name: "Measure",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 251, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 251, DateTimeKind.Utc).AddTicks(343));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 248, DateTimeKind.Utc).AddTicks(2183));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 247, DateTimeKind.Utc).AddTicks(5925));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 247, DateTimeKind.Utc).AddTicks(2753));

            migrationBuilder.AddColumn<string>(
                name: "CouldID",
                table: "Frames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ResetPasswordToken",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 6, 19, 3, 33, 29, 771, DateTimeKind.Utc).AddTicks(2419), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 6, 19, 3, 33, 29, 771, DateTimeKind.Utc).AddTicks(2421), "MoMo", "Active" }
                });

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "ServiceTypeID", "ServiceTypeName" },
                values: new object[,]
                {
                    { new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), "Make up" },
                    { new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"), "Take photo" },
                    { new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"), "Hire booth" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceID", "Measure", "Price", "ServiceDescription", "ServiceName", "ServiceTypeID", "Unit" },
                values: new object[,]
                {
                    { new Guid("13e75fc2-f38b-401e-9cd2-c545b80fd1f0"), 2, 190000m, "Make up with Korean stype for 2 people", "Combo make up with Korean stype for 2 people", new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), "people" },
                    { new Guid("30c2a7fb-9164-4164-9f1b-7c334744e559"), 15, 50000m, "Take Photo in 15 minutes", "Take Photo in 15 minutes", new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"), "minutes" },
                    { new Guid("5d568e18-8883-409b-bc48-6456aeefb4f9"), 120, 190000m, "Hire this booth for 120 minutes", "Hire this booth for 120 minutes", new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"), "minutes" },
                    { new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"), 1, 100000m, "Make up with Korean stype for 1 people", "Make up with Korean stype for 1 people", new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), "people" },
                    { new Guid("9b0dc075-557c-4b29-b3ba-c8b5841c4c68"), 60, 180000m, "Take Photo in 60 minutes", "Take Photo in 60 minutes", new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"), "minutes" },
                    { new Guid("c51a05bb-28af-4315-b888-606376ba061b"), 30, 90000m, "Take Photo in 30 minutes", "Take Photo in 30 minutes", new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"), "minutes" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("13e75fc2-f38b-401e-9cd2-c545b80fd1f0"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("30c2a7fb-9164-4164-9f1b-7c334744e559"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("5d568e18-8883-409b-bc48-6456aeefb4f9"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("9b0dc075-557c-4b29-b3ba-c8b5841c4c68"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("c51a05bb-28af-4315-b888-606376ba061b"));

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"));

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"));

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"));

            migrationBuilder.DropColumn(
                name: "Measure",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CouldID",
                table: "Frames");

            migrationBuilder.RenameColumn(
                name: "ThemeDescription",
                table: "Themes",
                newName: "ThemeFrameDescription");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 255, DateTimeKind.Utc).AddTicks(8024),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "SessionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 255, DateTimeKind.Utc).AddTicks(3774),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 251, DateTimeKind.Utc).AddTicks(2372),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 251, DateTimeKind.Utc).AddTicks(343),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 248, DateTimeKind.Utc).AddTicks(2183),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 247, DateTimeKind.Utc).AddTicks(5925),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 10, 4, 36, 247, DateTimeKind.Utc).AddTicks(2753),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "ResetPasswordToken",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 14, 10, 4, 36, 248, DateTimeKind.Utc).AddTicks(2734));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 14, 10, 4, 36, 248, DateTimeKind.Utc).AddTicks(2736));
        }
    }
}
