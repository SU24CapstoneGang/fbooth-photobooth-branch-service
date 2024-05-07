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
            migrationBuilder.DropForeignKey(
                name: "FK_Cameras_PhotoBoothBranches_Camera ID",
                table: "Cameras");

            migrationBuilder.DropForeignKey(
                name: "FK_Printers_PhotoBoothBranches_Printer ID",
                table: "Printers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoBoothBranches_Cameras_PhotoBoothBranch ID",
                table: "PhotoBoothBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoBoothBranches_Printers_PhotoBoothBranch ID",
                table: "PhotoBoothBranches");

            migrationBuilder.AddForeignKey(
                name: "FK_Cameras_PhotoBoothBranches_Camera ID",
                table: "Cameras",
                column: "Camera ID",
                principalTable: "PhotoBoothBranches",
                principalColumn: "PhotoBoothBranch ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_PhotoBoothBranches_Printer ID",
                table: "Printers",
                column: "Printer ID",
                principalTable: "PhotoBoothBranches",
                principalColumn: "PhotoBoothBranch ID");
        }
    }
}
