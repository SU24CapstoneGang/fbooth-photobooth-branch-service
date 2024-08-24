using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH38removepolicy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_FullPaymentPolicies_FullPaymentPolicyID",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "FullPaymentPolicies");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_FullPaymentPolicyID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FullPaymentPolicyID",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("1671ccd8-d367-47c7-9c48-335da54ec34d"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 6, 14, 40, 523, DateTimeKind.Unspecified).AddTicks(5636));

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("28110b4a-bf04-4c04-a19b-1b91d976ee7c"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 6, 14, 40, 523, DateTimeKind.Unspecified).AddTicks(5680));

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("b8b615f3-a04f-4bbf-8ab2-f42dd69a65fd"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 6, 14, 40, 523, DateTimeKind.Unspecified).AddTicks(5666));

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("bc8c737b-9a92-49b2-b9b1-bd8321c7e594"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 6, 14, 40, 523, DateTimeKind.Unspecified).AddTicks(5677));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("0a1f2e05-f744-4d9b-937c-bfe7bad52a90"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 6, 14, 40, 525, DateTimeKind.Unspecified).AddTicks(5744));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("b7fb8774-e3ac-4316-862c-23b81869c381"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 6, 14, 40, 525, DateTimeKind.Unspecified).AddTicks(5733));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 24, 23, 14, 40, 525, DateTimeKind.Utc).AddTicks(521));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 24, 23, 14, 40, 525, DateTimeKind.Utc).AddTicks(514));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 24, 23, 14, 40, 525, DateTimeKind.Utc).AddTicks(519));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("b3c4d5e6-a3d7-4edf-a3b9-34567890bcde"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 6, 14, 40, 527, DateTimeKind.Local).AddTicks(1783));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("d1f4e0c1-1f62-4c9e-8c4a-123456789abc"),
                columns: new[] { "CreatedDate", "ServiceType" },
                values: new object[] { new DateTime(2024, 8, 25, 6, 14, 40, 527, DateTimeKind.Local).AddTicks(1771), 6 });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("f2e3d4c5-b2b7-4bcb-93c8-23456789defa"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 6, 14, 40, 527, DateTimeKind.Local).AddTicks(1780));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FullPaymentPolicyID",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoothID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')"),
                    DeviceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
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
                name: "FullPaymentPolicies",
                columns: table => new
                {
                    FullPaymentPolicyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckInTimeLimit = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')"),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefaultPolicy = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')"),
                    PolicyDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PolicyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RefundDaysBefore = table.Column<int>(type: "int", nullable: false),
                    RefundPercent = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullPaymentPolicies", x => x.FullPaymentPolicyID);
                });

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("1671ccd8-d367-47c7-9c48-335da54ec34d"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 151, DateTimeKind.Unspecified).AddTicks(9814));

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("28110b4a-bf04-4c04-a19b-1b91d976ee7c"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 151, DateTimeKind.Unspecified).AddTicks(9853));

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("b8b615f3-a04f-4bbf-8ab2-f42dd69a65fd"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 151, DateTimeKind.Unspecified).AddTicks(9844));

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("bc8c737b-9a92-49b2-b9b1-bd8321c7e594"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 151, DateTimeKind.Unspecified).AddTicks(9849));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("0a1f2e05-f744-4d9b-937c-bfe7bad52a90"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 154, DateTimeKind.Unspecified).AddTicks(7845));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("b7fb8774-e3ac-4316-862c-23b81869c381"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 154, DateTimeKind.Unspecified).AddTicks(7800));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 0, 37, 28, 154, DateTimeKind.Utc).AddTicks(79));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 0, 37, 28, 154, DateTimeKind.Utc).AddTicks(66));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 0, 37, 28, 154, DateTimeKind.Utc).AddTicks(69));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("b3c4d5e6-a3d7-4edf-a3b9-34567890bcde"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 157, DateTimeKind.Local).AddTicks(1930));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("d1f4e0c1-1f62-4c9e-8c4a-123456789abc"),
                columns: new[] { "CreatedDate", "ServiceType" },
                values: new object[] { new DateTime(2024, 8, 14, 7, 37, 28, 157, DateTimeKind.Local).AddTicks(1909), 3 });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("f2e3d4c5-b2b7-4bcb-93c8-23456789defa"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 14, 7, 37, 28, 157, DateTimeKind.Local).AddTicks(1926));

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FullPaymentPolicyID",
                table: "Bookings",
                column: "FullPaymentPolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_BoothID",
                table: "Devices",
                column: "BoothID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_FullPaymentPolicies_FullPaymentPolicyID",
                table: "Bookings",
                column: "FullPaymentPolicyID",
                principalTable: "FullPaymentPolicies",
                principalColumn: "FullPaymentPolicyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
