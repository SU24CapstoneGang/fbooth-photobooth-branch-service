using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH39boothcodepaymentmethodiamge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CouldID",
                table: "PaymentMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MethodIconUrl",
                table: "PaymentMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ActiveCode",
                table: "Booths",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("1671ccd8-d367-47c7-9c48-335da54ec34d"),
                columns: new[] { "ActiveCode", "CreatedDate" },
                values: new object[] { "Y8K2-Pq3W-X4mL", new DateTime(2024, 8, 26, 3, 0, 0, 661, DateTimeKind.Unspecified).AddTicks(8130) });

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("28110b4a-bf04-4c04-a19b-1b91d976ee7c"),
                columns: new[] { "ActiveCode", "CreatedDate" },
                values: new object[] { "Z7X1-T9gJ-Y8hK", new DateTime(2024, 8, 26, 3, 0, 0, 661, DateTimeKind.Unspecified).AddTicks(8166) });

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("b8b615f3-a04f-4bbf-8ab2-f42dd69a65fd"),
                columns: new[] { "ActiveCode", "CreatedDate" },
                values: new object[] { "D4jH-N7gF-vP6t", new DateTime(2024, 8, 26, 3, 0, 0, 661, DateTimeKind.Unspecified).AddTicks(8157) });

            migrationBuilder.UpdateData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("bc8c737b-9a92-49b2-b9b1-bd8321c7e594"),
                columns: new[] { "ActiveCode", "CreatedDate" },
                values: new object[] { "pQ5R-s3fA-Mn2L", new DateTime(2024, 8, 26, 3, 0, 0, 661, DateTimeKind.Unspecified).AddTicks(8162) });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("0a1f2e05-f744-4d9b-937c-bfe7bad52a90"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 26, 3, 0, 0, 663, DateTimeKind.Unspecified).AddTicks(8306));

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("b7fb8774-e3ac-4316-862c-23b81869c381"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 26, 3, 0, 0, 663, DateTimeKind.Unspecified).AddTicks(8294));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"),
                columns: new[] { "CouldID", "CreatedDate", "MethodIconUrl" },
                values: new object[] { "Logo/Fbooth-Payment-Method-Icon/PngItem_4661926_yocrhe", new DateTime(2024, 8, 25, 20, 0, 0, 663, DateTimeKind.Utc).AddTicks(3215), "https://res.cloudinary.com/dfxvccyje/image/upload/v1724593057/Logo/Fbooth-Payment-Method-Icon/PngItem_4661926_yocrhe.png" });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                columns: new[] { "CouldID", "CreatedDate", "MethodIconUrl" },
                values: new object[] { "Logo/Fbooth-Payment-Method-Icon/vnpay-logo-inkythuatso-01_kipo9q", new DateTime(2024, 8, 25, 20, 0, 0, 663, DateTimeKind.Utc).AddTicks(3210), "https://res.cloudinary.com/dfxvccyje/image/upload/v1724593032/Logo/Fbooth-Payment-Method-Icon/vnpay-logo-inkythuatso-01_kipo9q.jpg" });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                columns: new[] { "CouldID", "CreatedDate", "MethodIconUrl" },
                values: new object[] { "Logo/Fbooth-Payment-Method-Icon/momo_icon_square_pinkbg_RGB_kmsxyu", new DateTime(2024, 8, 25, 20, 0, 0, 663, DateTimeKind.Utc).AddTicks(3213), "https://res.cloudinary.com/dfxvccyje/image/upload/v1724593005/Logo/Fbooth-Payment-Method-Icon/momo_icon_square_pinkbg_RGB_kmsxyu.png" });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("b3c4d5e6-a3d7-4edf-a3b9-34567890bcde"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 26, 3, 0, 0, 666, DateTimeKind.Local).AddTicks(5171));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("d1f4e0c1-1f62-4c9e-8c4a-123456789abc"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 26, 3, 0, 0, 666, DateTimeKind.Local).AddTicks(5157));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("f2e3d4c5-b2b7-4bcb-93c8-23456789defa"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 26, 3, 0, 0, 666, DateTimeKind.Local).AddTicks(5168));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouldID",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "MethodIconUrl",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "ActiveCode",
                table: "Booths");

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
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 6, 14, 40, 527, DateTimeKind.Local).AddTicks(1771));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("f2e3d4c5-b2b7-4bcb-93c8-23456789defa"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 25, 6, 14, 40, 527, DateTimeKind.Local).AddTicks(1780));
        }
    }
}
