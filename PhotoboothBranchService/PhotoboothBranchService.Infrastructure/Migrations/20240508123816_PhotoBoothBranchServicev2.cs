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
                name: "FK_PhotoBoothBranches_Accounts_PhotoBoothBranch ID",
                table: "PhotoBoothBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoBoothBranches_Cameras_PhotoBoothBranch ID",
                table: "PhotoBoothBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoBoothBranches_Printers_PhotoBoothBranch ID",
                table: "PhotoBoothBranches");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "PhotoBoothBranches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CameraId",
                table: "PhotoBoothBranches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrinterId",
                table: "PhotoBoothBranches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PhotoBoothBranches_AccountId",
                table: "PhotoBoothBranches",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoBoothBranches_CameraId",
                table: "PhotoBoothBranches",
                column: "CameraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoBoothBranches_PrinterId",
                table: "PhotoBoothBranches",
                column: "PrinterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoBoothBranches_Accounts_AccountId",
                table: "PhotoBoothBranches",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Account ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoBoothBranches_Cameras_CameraId",
                table: "PhotoBoothBranches",
                column: "CameraId",
                principalTable: "Cameras",
                principalColumn: "Camera ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoBoothBranches_Printers_PrinterId",
                table: "PhotoBoothBranches",
                column: "PrinterId",
                principalTable: "Printers",
                principalColumn: "Printer ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoBoothBranches_Accounts_AccountId",
                table: "PhotoBoothBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoBoothBranches_Cameras_CameraId",
                table: "PhotoBoothBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoBoothBranches_Printers_PrinterId",
                table: "PhotoBoothBranches");

            migrationBuilder.DropIndex(
                name: "IX_PhotoBoothBranches_AccountId",
                table: "PhotoBoothBranches");

            migrationBuilder.DropIndex(
                name: "IX_PhotoBoothBranches_CameraId",
                table: "PhotoBoothBranches");

            migrationBuilder.DropIndex(
                name: "IX_PhotoBoothBranches_PrinterId",
                table: "PhotoBoothBranches");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "PhotoBoothBranches");

            migrationBuilder.DropColumn(
                name: "CameraId",
                table: "PhotoBoothBranches");

            migrationBuilder.DropColumn(
                name: "PrinterId",
                table: "PhotoBoothBranches");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoBoothBranches_Accounts_PhotoBoothBranch ID",
                table: "PhotoBoothBranches",
                column: "PhotoBoothBranch ID",
                principalTable: "Accounts",
                principalColumn: "Account ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoBoothBranches_Cameras_PhotoBoothBranch ID",
                table: "PhotoBoothBranches",
                column: "PhotoBoothBranch ID",
                principalTable: "Cameras",
                principalColumn: "Camera ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoBoothBranches_Printers_PhotoBoothBranch ID",
                table: "PhotoBoothBranches",
                column: "PhotoBoothBranch ID",
                principalTable: "Printers",
                principalColumn: "Printer ID");
        }
    }
}
