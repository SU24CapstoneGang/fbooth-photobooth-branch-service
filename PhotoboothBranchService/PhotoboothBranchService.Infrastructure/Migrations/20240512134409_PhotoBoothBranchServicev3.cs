using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothBranchServicev3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Printers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 12, 13, 44, 9, 267, DateTimeKind.Utc).AddTicks(8648),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "PhotoBoothBranches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 12, 13, 44, 9, 255, DateTimeKind.Utc).AddTicks(419),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Cameras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 12, 13, 44, 9, 254, DateTimeKind.Utc).AddTicks(5630),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 12, 13, 44, 9, 254, DateTimeKind.Utc).AddTicks(1275),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Printers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 12, 13, 44, 9, 267, DateTimeKind.Utc).AddTicks(8648));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "PhotoBoothBranches",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 12, 13, 44, 9, 255, DateTimeKind.Utc).AddTicks(419));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Cameras",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 12, 13, 44, 9, 254, DateTimeKind.Utc).AddTicks(5630));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 12, 13, 44, 9, 254, DateTimeKind.Utc).AddTicks(1275));
        }
    }
}
