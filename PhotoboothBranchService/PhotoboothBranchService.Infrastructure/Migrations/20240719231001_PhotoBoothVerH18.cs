using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_BoothBranches_PhotoBoothBranchID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Booths_BoothBranches_PhotoBoothBranchID",
                table: "Booths");

            migrationBuilder.DropTable(
                name: "BoothBranches");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"));

            migrationBuilder.RenameColumn(
                name: "PhotoBoothBranchID",
                table: "Booths",
                newName: "BranchID");

            migrationBuilder.RenameIndex(
                name: "IX_Booths_PhotoBoothBranchID",
                table: "Booths",
                newName: "IX_Booths_BranchID");

            migrationBuilder.RenameColumn(
                name: "PhotoBoothBranchID",
                table: "Accounts",
                newName: "BranchID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_PhotoBoothBranchID",
                table: "Accounts",
                newName: "IX_Accounts_BranchID");

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BranchID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchID);
                    table.ForeignKey(
                        name: "FK_Branches_Accounts_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID");
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 7, 19, 23, 10, 1, 596, DateTimeKind.Utc).AddTicks(4661), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 7, 19, 23, 10, 1, 596, DateTimeKind.Utc).AddTicks(4664), "MoMo", "Active" }
                });

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "ServiceTypeID", "ServiceTypeName" },
                values: new object[,]
                {
                    { new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), "Make up" },
                    { new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"), "Relate to Take Photo" }
                });

            migrationBuilder.InsertData(
                table: "SessionPackages",
                columns: new[] { "SessionPackageID", "Duration", "EmailSendCount", "Price", "PrintCount", "SessionPackageDescription", "SessionPackageName" },
                values: new object[,]
                {
                    { new Guid("041f73b6-7c08-4c63-8a08-aaebae219048"), (short)150, (short)50, 950000m, (short)50, "Package have 150 minutes in take photo, can print 50 photo and seend to email 50 times", "150 minutes special package " },
                    { new Guid("2fbb7633-5796-465a-98a6-f3c484811f24"), (short)60, (short)20, 440000m, (short)20, "Package have 60 minutes in take photo, can print 20 photo and seend to email 20 times", "60 minutes special package " },
                    { new Guid("306c5339-f453-4e2d-839e-4b3aacf17084"), (short)90, (short)10, 310000m, (short)10, "Package have 90 minutes in take photo, can print 10 photo and seend to email 10 times", "90 minutes mornal package" },
                    { new Guid("318dc257-983e-45d7-8dcb-10f348975c38"), (short)90, (short)20, 440000m, (short)20, "Package have 90 minutes in take photo, can print 20 photo and seend to email 20 times", "90 minutes special package" },
                    { new Guid("6c739b97-36d4-4559-8738-de8cc132b705"), (short)90, (short)50, 830000m, (short)50, "Package have 90 minutes in take photo, can print 50 photo and seend to email 50 times", "90 minutes special package PROMAX" },
                    { new Guid("f98b3003-c0ba-4305-a5ce-d8f77dd7310e"), (short)60, (short)10, 310000m, (short)10, "Package have 60 minutes in take photo, can print 10 photo and seend to email 10 times", "60 minutes normal package " }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceID", "Measure", "Price", "ServiceDescription", "ServiceName", "ServiceTypeID", "Unit" },
                values: new object[,]
                {
                    { new Guid("13e75fc2-f38b-401e-9cd2-c545b80fd1f0"), 2, 190000m, "Make up with Korean stype for 2 people", "Combo make up with Korean stype for 2 people", new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), "people" },
                    { new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"), 1, 100000m, "Make up with Korean stype for 1 people", "Make up with Korean stype for 1 people", new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"), "people" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_ManagerID",
                table: "Branches",
                column: "ManagerID",
                unique: true,
                filter: "[ManagerID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Branches_BranchID",
                table: "Accounts",
                column: "BranchID",
                principalTable: "Branches",
                principalColumn: "BranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booths_Branches_BranchID",
                table: "Booths",
                column: "BranchID",
                principalTable: "Branches",
                principalColumn: "BranchID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Branches_BranchID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Booths_Branches_BranchID",
                table: "Booths");

            migrationBuilder.DropTable(
                name: "Branches");

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
                keyValue: new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"));

            migrationBuilder.DeleteData(
                table: "SessionPackages",
                keyColumn: "SessionPackageID",
                keyValue: new Guid("041f73b6-7c08-4c63-8a08-aaebae219048"));

            migrationBuilder.DeleteData(
                table: "SessionPackages",
                keyColumn: "SessionPackageID",
                keyValue: new Guid("2fbb7633-5796-465a-98a6-f3c484811f24"));

            migrationBuilder.DeleteData(
                table: "SessionPackages",
                keyColumn: "SessionPackageID",
                keyValue: new Guid("306c5339-f453-4e2d-839e-4b3aacf17084"));

            migrationBuilder.DeleteData(
                table: "SessionPackages",
                keyColumn: "SessionPackageID",
                keyValue: new Guid("318dc257-983e-45d7-8dcb-10f348975c38"));

            migrationBuilder.DeleteData(
                table: "SessionPackages",
                keyColumn: "SessionPackageID",
                keyValue: new Guid("6c739b97-36d4-4559-8738-de8cc132b705"));

            migrationBuilder.DeleteData(
                table: "SessionPackages",
                keyColumn: "SessionPackageID",
                keyValue: new Guid("f98b3003-c0ba-4305-a5ce-d8f77dd7310e"));

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeID",
                keyValue: new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"));

            migrationBuilder.RenameColumn(
                name: "BranchID",
                table: "Booths",
                newName: "PhotoBoothBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_Booths_BranchID",
                table: "Booths",
                newName: "IX_Booths_PhotoBoothBranchID");

            migrationBuilder.RenameColumn(
                name: "BranchID",
                table: "Accounts",
                newName: "PhotoBoothBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_BranchID",
                table: "Accounts",
                newName: "IX_Accounts_PhotoBoothBranchID");

            migrationBuilder.CreateTable(
                name: "BoothBranches",
                columns: table => new
                {
                    BoothBranchID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManagerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoothBranches", x => x.BoothBranchID);
                    table.ForeignKey(
                        name: "FK_BoothBranches_Accounts_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID");
                });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 17, 4, 15, 43, 817, DateTimeKind.Utc).AddTicks(7931));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 7, 17, 4, 15, 43, 817, DateTimeKind.Utc).AddTicks(7942));

            migrationBuilder.CreateIndex(
                name: "IX_BoothBranches_ManagerID",
                table: "BoothBranches",
                column: "ManagerID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_BoothBranches_PhotoBoothBranchID",
                table: "Accounts",
                column: "PhotoBoothBranchID",
                principalTable: "BoothBranches",
                principalColumn: "BoothBranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booths_BoothBranches_PhotoBoothBranchID",
                table: "Booths",
                column: "PhotoBoothBranchID",
                principalTable: "BoothBranches",
                principalColumn: "BoothBranchID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
