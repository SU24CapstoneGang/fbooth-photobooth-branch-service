using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothBranchService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<Guid>(name: "Account ID", type: "uniqueidentifier", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Cameras",
                columns: table => new
                {
                    CameraID = table.Column<Guid>(name: "Camera ID", type: "uniqueidentifier", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SensorType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lens = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cameras", x => x.CameraID);
                });

            migrationBuilder.CreateTable(
                name: "Printers",
                columns: table => new
                {
                    PrinterID = table.Column<Guid>(name: "Printer ID", type: "uniqueidentifier", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printers", x => x.PrinterID);
                });

            migrationBuilder.CreateTable(
                name: "PhotoBoothBranches",
                columns: table => new
                {
                    PhotoBoothBranchID = table.Column<Guid>(name: "PhotoBoothBranch ID", type: "uniqueidentifier", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BranchAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoBoothBranches", x => x.PhotoBoothBranchID);
                    table.ForeignKey(
                        name: "FK_PhotoBoothBranches_Accounts_PhotoBoothBranch ID",
                        column: x => x.PhotoBoothBranchID,
                        principalTable: "Accounts",
                        principalColumn: "Account ID");
                    table.ForeignKey(
                        name: "FK_PhotoBoothBranches_Cameras_PhotoBoothBranch ID",
                        column: x => x.PhotoBoothBranchID,
                        principalTable: "Cameras",
                        principalColumn: "Camera ID");
                    table.ForeignKey(
                        name: "FK_PhotoBoothBranches_Printers_PhotoBoothBranch ID",
                        column: x => x.PhotoBoothBranchID,
                        principalTable: "Printers",
                        principalColumn: "Printer ID");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoBoothBranches");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Cameras");

            migrationBuilder.DropTable(
                name: "Printers");
        }
    }
}
