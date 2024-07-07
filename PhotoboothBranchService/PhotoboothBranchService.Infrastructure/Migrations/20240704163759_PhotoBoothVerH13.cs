using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH13 : Migration
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

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"));

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
                keyValue: new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"));

            migrationBuilder.RenameColumn(
                name: "BranchAddress",
                table: "BoothBranches",
                newName: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "SessionPackageDescription",
                table: "SessionPackages",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "Booths",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Concept",
                table: "Booths",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "PeopleInBooth",
                table: "Booths",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "BoothBranches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "BoothBranches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 7, 4, 16, 37, 59, 476, DateTimeKind.Utc).AddTicks(3150), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 7, 4, 16, 37, 59, 476, DateTimeKind.Utc).AddTicks(3158), "MoMo", "Active" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "Booths");

            migrationBuilder.DropColumn(
                name: "Concept",
                table: "Booths");

            migrationBuilder.DropColumn(
                name: "PeopleInBooth",
                table: "Booths");

            migrationBuilder.DropColumn(
                name: "City",
                table: "BoothBranches");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "BoothBranches");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "BoothBranches",
                newName: "BranchAddress");

            migrationBuilder.AlterColumn<string>(
                name: "SessionPackageDescription",
                table: "SessionPackages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 2, 15, 22, 8, 99, DateTimeKind.Utc).AddTicks(5799));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 2, 15, 22, 8, 99, DateTimeKind.Utc).AddTicks(5802));

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "ServiceTypeID", "ServiceTypeName" },
                values: new object[,]
                {
                    { new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), "Make up" },
                    { new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"), "Hire booth" },
                    { new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"), "Relate to Take Photo" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceID", "Measure", "Price", "ServiceDescription", "ServiceName", "ServiceTypeID", "Unit" },
                values: new object[,]
                {
                    { new Guid("13e75fc2-f38b-401e-9cd2-c545b80fd1f0"), 2, 190000m, "Make up with Korean stype for 2 people", "Combo make up with Korean stype for 2 people", new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), "people" },
                    { new Guid("30c2a7fb-9164-4164-9f1b-7c334744e559"), 15, 50000m, "Take Photo in 15 minutes", "Take Photo in 15 minutes", new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"), "minutes" },
                    { new Guid("5d568e18-8883-409b-bc48-6456aeefb4f9"), 120, 190000m, "Hire this booth for 120 minutes", "Hire this booth for 120 minutes", new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"), "minutes" },
                    { new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"), 1, 100000m, "Make up with Korean stype for 1 people", "Make up with Korean stype for 1 people", new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), "people" },
                    { new Guid("9b0dc075-557c-4b29-b3ba-c8b5841c4c68"), 60, 180000m, "Take Photo in 60 minutes", "Take Photo in 60 minutes", new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"), "minutes" },
                    { new Guid("c51a05bb-28af-4315-b888-606376ba061b"), 30, 90000m, "Take Photo in 30 minutes", "Take Photo in 30 minutes", new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"), "minutes" }
                });
        }
    }
}
