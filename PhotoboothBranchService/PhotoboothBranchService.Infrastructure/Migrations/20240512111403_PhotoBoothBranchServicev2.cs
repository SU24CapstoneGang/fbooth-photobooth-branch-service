using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothBranchServicev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_PhotoBoothBranches_PhotoBoothBranchId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Cameras_PhotoBoothBranches_PhotoBoothBranchId",
                table: "Cameras");

            migrationBuilder.DropForeignKey(
                name: "FK_Printers_PhotoBoothBranches_PhotoBoothBranchId",
                table: "Printers");

            migrationBuilder.DropIndex(
                name: "IX_Printers_PhotoBoothBranchId",
                table: "Printers");

            migrationBuilder.DropIndex(
                name: "IX_Cameras_PhotoBoothBranchId",
                table: "Cameras");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_PhotoBoothBranchId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PhotoBoothBranchId",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "PhotoBoothBranchId",
                table: "Cameras");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhotoBoothBranchId",
                table: "Printers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoBoothBranchId",
                table: "Cameras",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Printers_PhotoBoothBranchId",
                table: "Printers",
                column: "PhotoBoothBranchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_PhotoBoothBranchId",
                table: "Cameras",
                column: "PhotoBoothBranchId",
                unique: true,
                filter: "[PhotoBoothBranchId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PhotoBoothBranchId",
                table: "Accounts",
                column: "PhotoBoothBranchId",
                unique: true,
                filter: "[PhotoBoothBranchId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_PhotoBoothBranches_PhotoBoothBranchId",
                table: "Accounts",
                column: "PhotoBoothBranchId",
                principalTable: "PhotoBoothBranches",
                principalColumn: "PhotoBoothBranch ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cameras_PhotoBoothBranches_PhotoBoothBranchId",
                table: "Cameras",
                column: "PhotoBoothBranchId",
                principalTable: "PhotoBoothBranches",
                principalColumn: "PhotoBoothBranch ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_PhotoBoothBranches_PhotoBoothBranchId",
                table: "Printers",
                column: "PhotoBoothBranchId",
                principalTable: "PhotoBoothBranches",
                principalColumn: "PhotoBoothBranch ID");
        }
    }
}
