using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH19 : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ServiceTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "LayoutID",
                table: "PhotoSessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Booths",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "BranchID", "Address", "BranchName", "City", "ManagerID", "Status", "Town" },
                values: new object[,]
                {
                    { new Guid("0a1f2e05-f744-4d9b-937c-bfe7bad52a90"), "Mega Mall Pham Van Dong", "Mega Mall Pham Van Dong", "Thanh pho Thu Duc", null, "Active", "Thu Duc" },
                    { new Guid("b7fb8774-e3ac-4316-862c-23b81869c381"), "Vincom Le Van Viet q9", "Vincom Le Van Viet q9", "HCMC", null, "Active", "district 9" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 7, 21, 22, 20, 24, 279, DateTimeKind.Utc).AddTicks(6573), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 7, 21, 22, 20, 24, 279, DateTimeKind.Utc).AddTicks(6576), "MoMo", "Active" }
                });

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
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("13e75fc2-f38b-401e-9cd2-c545b80fd1f0"),
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceID",
                keyValue: new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"),
                column: "Status",
                value: 0);

            migrationBuilder.InsertData(
                table: "Booths",
                columns: new[] { "BoothID", "BackgroundColor", "BoothName", "BranchID", "Concept", "PeopleInBooth", "Status" },
                values: new object[,]
                {
                    { new Guid("1671ccd8-d367-47c7-9c48-335da54ec34d"), "yellow", "Booth 01", new Guid("b7fb8774-e3ac-4316-862c-23b81869c381"), "Hallucination", (short)5, "Active" },
                    { new Guid("28110b4a-bf04-4c04-a19b-1b91d976ee7c"), "yellow", "Booth 04", new Guid("0a1f2e05-f744-4d9b-937c-bfe7bad52a90"), "Hallucination", (short)3, "Active" },
                    { new Guid("b8b615f3-a04f-4bbf-8ab2-f42dd69a65fd"), "yellow", "Booth 02", new Guid("b7fb8774-e3ac-4316-862c-23b81869c381"), "Nightmare", (short)6, "Active" },
                    { new Guid("bc8c737b-9a92-49b2-b9b1-bd8321c7e594"), "yellow", "Booth 03", new Guid("0a1f2e05-f744-4d9b-937c-bfe7bad52a90"), "Nightmare", (short)4, "Active" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("1671ccd8-d367-47c7-9c48-335da54ec34d"));

            migrationBuilder.DeleteData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("28110b4a-bf04-4c04-a19b-1b91d976ee7c"));

            migrationBuilder.DeleteData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("b8b615f3-a04f-4bbf-8ab2-f42dd69a65fd"));

            migrationBuilder.DeleteData(
                table: "Booths",
                keyColumn: "BoothID",
                keyValue: new Guid("bc8c737b-9a92-49b2-b9b1-bd8321c7e594"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("0a1f2e05-f744-4d9b-937c-bfe7bad52a90"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "BranchID",
                keyValue: new Guid("b7fb8774-e3ac-4316-862c-23b81869c381"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Booths");

            migrationBuilder.AlterColumn<Guid>(
                name: "LayoutID",
                table: "PhotoSessions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 19, 23, 10, 1, 596, DateTimeKind.Utc).AddTicks(4661));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 19, 23, 10, 1, 596, DateTimeKind.Utc).AddTicks(4664));
        }
    }
}
