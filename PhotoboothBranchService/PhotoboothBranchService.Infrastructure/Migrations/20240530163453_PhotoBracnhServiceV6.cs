using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBracnhServiceV6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoBoothBranches_Cameras_CameraID",
                table: "PhotoBoothBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Layouts_LayoutID",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_LayoutID",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_PhotoBoothBranches_CameraID",
                table: "PhotoBoothBranches");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "LayoutID",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PrintPricings");

            migrationBuilder.DropColumn(
                name: "PrintName",
                table: "PrintPricings");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Sessions",
                newName: "CreateDate");

            migrationBuilder.AddColumn<string>(
                name: "ThirdpartyID",
                table: "TransactionHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 443, DateTimeKind.Utc).AddTicks(3796),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 147, DateTimeKind.Utc).AddTicks(8010));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PrintPricings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 442, DateTimeKind.Utc).AddTicks(2741),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 146, DateTimeKind.Utc).AddTicks(5575));

            migrationBuilder.AlterColumn<Guid>(
                name: "CameraID",
                table: "PhotoBoothBranches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 439, DateTimeKind.Utc).AddTicks(1894),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 142, DateTimeKind.Utc).AddTicks(1031));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 438, DateTimeKind.Utc).AddTicks(8053),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 141, DateTimeKind.Utc).AddTicks(7527));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 438, DateTimeKind.Utc).AddTicks(5075),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 141, DateTimeKind.Utc).AddTicks(3090));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "FinalPictures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 438, DateTimeKind.Utc).AddTicks(2173),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 141, DateTimeKind.Utc).AddTicks(7));

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "FinalPictures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Filters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 437, DateTimeKind.Utc).AddTicks(5893),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 140, DateTimeKind.Utc).AddTicks(2044));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 435, DateTimeKind.Utc).AddTicks(1171),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 136, DateTimeKind.Utc).AddTicks(7307));

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("3ff27515-80e9-4ea9-8a60-5e2b02bc338a"), new DateTime(2024, 5, 30, 16, 34, 53, 439, DateTimeKind.Utc).AddTicks(2225), "VNPay", "Active" },
                    { new Guid("e7292894-9c01-40f0-8354-e974d93098b7"), new DateTime(2024, 5, 30, 16, 34, 53, 439, DateTimeKind.Utc).AddTicks(2228), "MoMo", "Active" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { new Guid("80624419-a203-40b7-9dc0-51205b34e1c6"), "Manager" },
                    { new Guid("9bd86f81-ce71-4a5c-98f0-702ac0409470"), "BranchManager" },
                    { new Guid("ba132a6b-5963-4869-94d5-f8736273fe7b"), "Admin" },
                    { new Guid("f74311a5-b646-4db8-9814-e2154b3f402e"), "Customer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoBoothBranches_CameraID",
                table: "PhotoBoothBranches",
                column: "CameraID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoBoothBranches_Cameras_CameraID",
                table: "PhotoBoothBranches",
                column: "CameraID",
                principalTable: "Cameras",
                principalColumn: "CameraID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoBoothBranches_Cameras_CameraID",
                table: "PhotoBoothBranches");

            migrationBuilder.DropIndex(
                name: "IX_PhotoBoothBranches_CameraID",
                table: "PhotoBoothBranches");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("3ff27515-80e9-4ea9-8a60-5e2b02bc338a"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("e7292894-9c01-40f0-8354-e974d93098b7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: new Guid("80624419-a203-40b7-9dc0-51205b34e1c6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: new Guid("9bd86f81-ce71-4a5c-98f0-702ac0409470"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: new Guid("ba132a6b-5963-4869-94d5-f8736273fe7b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleID",
                keyValue: new Guid("f74311a5-b646-4db8-9814-e2154b3f402e"));

            migrationBuilder.DropColumn(
                name: "ThirdpartyID",
                table: "TransactionHistories");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "FinalPictures");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Sessions",
                newName: "StartTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 147, DateTimeKind.Utc).AddTicks(8010),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 443, DateTimeKind.Utc).AddTicks(3796));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LayoutID",
                table: "Sessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PrintPricings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 146, DateTimeKind.Utc).AddTicks(5575),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 442, DateTimeKind.Utc).AddTicks(2741));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PrintPricings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrintName",
                table: "PrintPricings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "CameraID",
                table: "PhotoBoothBranches",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 142, DateTimeKind.Utc).AddTicks(1031),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 439, DateTimeKind.Utc).AddTicks(1894));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 141, DateTimeKind.Utc).AddTicks(7527),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 438, DateTimeKind.Utc).AddTicks(8053));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 141, DateTimeKind.Utc).AddTicks(3090),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 438, DateTimeKind.Utc).AddTicks(5075));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "FinalPictures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 141, DateTimeKind.Utc).AddTicks(7),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 438, DateTimeKind.Utc).AddTicks(2173));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Filters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 140, DateTimeKind.Utc).AddTicks(2044),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 437, DateTimeKind.Utc).AddTicks(5893));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 27, 19, 5, 33, 136, DateTimeKind.Utc).AddTicks(7307),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 30, 16, 34, 53, 435, DateTimeKind.Utc).AddTicks(1171));

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_LayoutID",
                table: "Sessions",
                column: "LayoutID");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoBoothBranches_CameraID",
                table: "PhotoBoothBranches",
                column: "CameraID",
                unique: true,
                filter: "[CameraID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoBoothBranches_Cameras_CameraID",
                table: "PhotoBoothBranches",
                column: "CameraID",
                principalTable: "Cameras",
                principalColumn: "CameraID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Layouts_LayoutID",
                table: "Sessions",
                column: "LayoutID",
                principalTable: "Layouts",
                principalColumn: "LayoutID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
