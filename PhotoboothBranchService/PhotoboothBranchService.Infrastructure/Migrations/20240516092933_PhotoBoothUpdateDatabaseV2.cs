using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothUpdateDatabaseV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Roles_RoleID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalPictures_EffectsPacks_PackID",
                table: "FinalPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalPictures_Orders_OrderID",
                table: "FinalPictures");

            migrationBuilder.DropTable(
                name: "EffectsPacks");

            migrationBuilder.DropIndex(
                name: "IX_FinalPictures_OrderID",
                table: "FinalPictures");

            migrationBuilder.DropIndex(
                name: "IX_FinalPictures_PackID",
                table: "FinalPictures");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "FinalPictures");

            migrationBuilder.RenameColumn(
                name: "PhotoQuantity",
                table: "Orders",
                newName: "QuantityOfPicture");

            migrationBuilder.RenameColumn(
                name: "Privacy",
                table: "FinalPictures",
                newName: "PicturePrivacy");

            migrationBuilder.RenameColumn(
                name: "PackID",
                table: "FinalPictures",
                newName: "LayoutID");

            migrationBuilder.RenameColumn(
                name: "SensorType",
                table: "Cameras",
                newName: "LensType");

            migrationBuilder.AddColumn<Guid>(
                name: "PackID",
                table: "Stickers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Printers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PictureID",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<float>(
                name: "LayoutPrice",
                table: "Layouts",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "FinalPictures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "PictureCost",
                table: "FinalPictures",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "PrintQuantity",
                table: "FinalPictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Lens",
                table: "Cameras",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Cameras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleID",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EffectsPackLogs",
                columns: table => new
                {
                    PackID = table.Column<Guid>(name: "Pack ID", type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FrameID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectsPackLogs", x => x.PackID);
                    table.ForeignKey(
                        name: "FK_EffectsPackLogs_Filters_FilterID",
                        column: x => x.FilterID,
                        principalTable: "Filters",
                        principalColumn: "Filter ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectsPackLogs_FinalPictures_PictureID",
                        column: x => x.PictureID,
                        principalTable: "FinalPictures",
                        principalColumn: "Picture ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectsPackLogs_Frames_FrameID",
                        column: x => x.FrameID,
                        principalTable: "Frames",
                        principalColumn: "Frame ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stickers_PackID",
                table: "Stickers",
                column: "PackID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PictureID",
                table: "Orders",
                column: "PictureID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinalPictures_LayoutID",
                table: "FinalPictures",
                column: "LayoutID");

            migrationBuilder.CreateIndex(
                name: "IX_EffectsPackLogs_FilterID",
                table: "EffectsPackLogs",
                column: "FilterID");

            migrationBuilder.CreateIndex(
                name: "IX_EffectsPackLogs_FrameID",
                table: "EffectsPackLogs",
                column: "FrameID");

            migrationBuilder.CreateIndex(
                name: "IX_EffectsPackLogs_PictureID",
                table: "EffectsPackLogs",
                column: "PictureID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Roles_RoleID",
                table: "Accounts",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "Role ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalPictures_Layouts_LayoutID",
                table: "FinalPictures",
                column: "LayoutID",
                principalTable: "Layouts",
                principalColumn: "Layout ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_FinalPictures_PictureID",
                table: "Orders",
                column: "PictureID",
                principalTable: "FinalPictures",
                principalColumn: "Picture ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stickers_EffectsPackLogs_PackID",
                table: "Stickers",
                column: "PackID",
                principalTable: "EffectsPackLogs",
                principalColumn: "Pack ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Roles_RoleID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalPictures_Layouts_LayoutID",
                table: "FinalPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_FinalPictures_PictureID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Stickers_EffectsPackLogs_PackID",
                table: "Stickers");

            migrationBuilder.DropTable(
                name: "EffectsPackLogs");

            migrationBuilder.DropIndex(
                name: "IX_Stickers_PackID",
                table: "Stickers");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PictureID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_FinalPictures_LayoutID",
                table: "FinalPictures");

            migrationBuilder.DropColumn(
                name: "PackID",
                table: "Stickers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "PictureID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "FinalPictures");

            migrationBuilder.DropColumn(
                name: "PictureCost",
                table: "FinalPictures");

            migrationBuilder.DropColumn(
                name: "PrintQuantity",
                table: "FinalPictures");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cameras");

            migrationBuilder.RenameColumn(
                name: "QuantityOfPicture",
                table: "Orders",
                newName: "PhotoQuantity");

            migrationBuilder.RenameColumn(
                name: "PicturePrivacy",
                table: "FinalPictures",
                newName: "Privacy");

            migrationBuilder.RenameColumn(
                name: "LayoutID",
                table: "FinalPictures",
                newName: "PackID");

            migrationBuilder.RenameColumn(
                name: "LensType",
                table: "Cameras",
                newName: "SensorType");

            migrationBuilder.AlterColumn<double>(
                name: "LayoutPrice",
                table: "Layouts",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderID",
                table: "FinalPictures",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Lens",
                table: "Cameras",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleID",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "EffectsPacks",
                columns: table => new
                {
                    PackID = table.Column<Guid>(name: "Pack ID", type: "uniqueidentifier", nullable: false),
                    FilterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FrameID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LayoutID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StickerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PackagePrice = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectsPacks", x => x.PackID);
                    table.ForeignKey(
                        name: "FK_EffectsPacks_Filters_FilterID",
                        column: x => x.FilterID,
                        principalTable: "Filters",
                        principalColumn: "Filter ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectsPacks_Frames_FrameID",
                        column: x => x.FrameID,
                        principalTable: "Frames",
                        principalColumn: "Frame ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectsPacks_Layouts_LayoutID",
                        column: x => x.LayoutID,
                        principalTable: "Layouts",
                        principalColumn: "Layout ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectsPacks_Stickers_StickerID",
                        column: x => x.StickerID,
                        principalTable: "Stickers",
                        principalColumn: "Sticker ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinalPictures_OrderID",
                table: "FinalPictures",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_FinalPictures_PackID",
                table: "FinalPictures",
                column: "PackID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EffectsPacks_FilterID",
                table: "EffectsPacks",
                column: "FilterID");

            migrationBuilder.CreateIndex(
                name: "IX_EffectsPacks_FrameID",
                table: "EffectsPacks",
                column: "FrameID");

            migrationBuilder.CreateIndex(
                name: "IX_EffectsPacks_LayoutID",
                table: "EffectsPacks",
                column: "LayoutID");

            migrationBuilder.CreateIndex(
                name: "IX_EffectsPacks_StickerID",
                table: "EffectsPacks",
                column: "StickerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Roles_RoleID",
                table: "Accounts",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "Role ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FinalPictures_EffectsPacks_PackID",
                table: "FinalPictures",
                column: "PackID",
                principalTable: "EffectsPacks",
                principalColumn: "Pack ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalPictures_Orders_OrderID",
                table: "FinalPictures",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "Order ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
