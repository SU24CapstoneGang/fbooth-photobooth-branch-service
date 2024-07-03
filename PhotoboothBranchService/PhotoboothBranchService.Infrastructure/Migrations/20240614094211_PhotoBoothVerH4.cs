using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Layouts_Frames_FrameID",
                table: "Layouts");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Booths_BoothID",
                table: "PhotoSessions");

            migrationBuilder.DropIndex(
                name: "IX_Layouts_FrameID",
                table: "Layouts");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"));

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"));

            migrationBuilder.DropColumn(
                name: "FrameID",
                table: "Layouts");

            migrationBuilder.RenameColumn(
                name: "BoothID",
                table: "PhotoSessions",
                newName: "SessionOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoSessions_BoothID",
                table: "PhotoSessions",
                newName: "IX_PhotoSessions_SessionOrderID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 515, DateTimeKind.Utc).AddTicks(2417),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 899, DateTimeKind.Utc).AddTicks(8027));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "SessionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 514, DateTimeKind.Utc).AddTicks(8316),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 899, DateTimeKind.Utc).AddTicks(3852));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 513, DateTimeKind.Utc).AddTicks(4960),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 897, DateTimeKind.Utc).AddTicks(8834));

            migrationBuilder.AlterColumn<string>(
                name: "Version",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 513, DateTimeKind.Utc).AddTicks(2767),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 510, DateTimeKind.Utc).AddTicks(2134),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 891, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.AlterColumn<int>(
                name: "Width",
                table: "Layouts",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "Lenght",
                table: "Layouts",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 509, DateTimeKind.Utc).AddTicks(4776),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 891, DateTimeKind.Utc).AddTicks(2005));

            migrationBuilder.AlterColumn<int>(
                name: "Width",
                table: "Frames",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "Lenght",
                table: "Frames",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 509, DateTimeKind.Utc).AddTicks(1463),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 890, DateTimeKind.Utc).AddTicks(7177));

            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PhotoBox",
                columns: table => new
                {
                    PhotoBoxID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    boxLength = table.Column<int>(type: "int", nullable: false),
                    boxWidth = table.Column<int>(type: "int", nullable: false),
                    CoordinatesX = table.Column<int>(type: "int", nullable: false),
                    CoordinatesY = table.Column<int>(type: "int", nullable: false),
                    LayoutID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoBox", x => x.PhotoBoxID);
                    table.ForeignKey(
                        name: "FK_PhotoBox_Layouts_LayoutID",
                        column: x => x.LayoutID,
                        principalTable: "Layouts",
                        principalColumn: "LayoutID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 6, 14, 9, 42, 11, 510, DateTimeKind.Utc).AddTicks(2756), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 6, 14, 9, 42, 11, 510, DateTimeKind.Utc).AddTicks(2759), "MoMo", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoBox_LayoutID",
                table: "PhotoBox",
                column: "LayoutID");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_SessionOrders_SessionOrderID",
                table: "PhotoSessions",
                column: "SessionOrderID",
                principalTable: "SessionOrders",
                principalColumn: "SessionOrderID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_SessionOrders_SessionOrderID",
                table: "PhotoSessions");

            migrationBuilder.DropTable(
                name: "PhotoBox");

            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "SessionOrderID",
                table: "PhotoSessions",
                newName: "BoothID");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoSessions_SessionOrderID",
                table: "PhotoSessions",
                newName: "IX_PhotoSessions_BoothID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Stickers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 899, DateTimeKind.Utc).AddTicks(8027),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 515, DateTimeKind.Utc).AddTicks(2417));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "SessionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 899, DateTimeKind.Utc).AddTicks(3852),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 514, DateTimeKind.Utc).AddTicks(8316));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "PhotoSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 897, DateTimeKind.Utc).AddTicks(8834),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 513, DateTimeKind.Utc).AddTicks(4960));

            migrationBuilder.AlterColumn<int>(
                name: "Version",
                table: "Photos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 513, DateTimeKind.Utc).AddTicks(2767));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 891, DateTimeKind.Utc).AddTicks(8197),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 510, DateTimeKind.Utc).AddTicks(2134));

            migrationBuilder.AlterColumn<float>(
                name: "Width",
                table: "Layouts",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Lenght",
                table: "Layouts",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Layouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 891, DateTimeKind.Utc).AddTicks(2005),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 509, DateTimeKind.Utc).AddTicks(4776));

            migrationBuilder.AddColumn<Guid>(
                name: "FrameID",
                table: "Layouts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<float>(
                name: "Width",
                table: "Frames",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Lenght",
                table: "Frames",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Frames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 13, 22, 46, 25, 890, DateTimeKind.Utc).AddTicks(7177),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 14, 9, 42, 11, 509, DateTimeKind.Utc).AddTicks(1463));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 13, 22, 46, 25, 891, DateTimeKind.Utc).AddTicks(8778));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodID",
                keyValue: new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                column: "CreateDate",
                value: new DateTime(2024, 6, 13, 22, 46, 25, 891, DateTimeKind.Utc).AddTicks(8781));

            migrationBuilder.CreateIndex(
                name: "IX_Layouts_FrameID",
                table: "Layouts",
                column: "FrameID");

            migrationBuilder.AddForeignKey(
                name: "FK_Layouts_Frames_FrameID",
                table: "Layouts",
                column: "FrameID",
                principalTable: "Frames",
                principalColumn: "FrameID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Booths_BoothID",
                table: "PhotoSessions",
                column: "BoothID",
                principalTable: "Booths",
                principalColumn: "BoothID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
