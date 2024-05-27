using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migrationvd1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountID = table.Column<Guid>(name: "Discount ID", type: "uniqueidentifier", nullable: false),
                    DiscountCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RemaniningUsage = table.Column<int>(type: "int", nullable: false),
                    DiscountRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 27, 9, 14, 11, 774, DateTimeKind.Utc).AddTicks(6655)),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountID);
                });

            migrationBuilder.CreateTable(
                name: "Layouts",
                columns: table => new
                {
                    LayoutID = table.Column<Guid>(name: "Layout ID", type: "uniqueidentifier", nullable: false),
                    LayoutURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LayoutPrice = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 27, 9, 14, 11, 778, DateTimeKind.Utc).AddTicks(5901)),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layouts", x => x.LayoutID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodID = table.Column<Guid>(name: "Payment Method ID", type: "uniqueidentifier", nullable: false),
                    PaymentMethodName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 27, 9, 14, 11, 778, DateTimeKind.Utc).AddTicks(9564)),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "PrintPricings",
                columns: table => new
                {
                    PrintPricingID = table.Column<Guid>(name: "PrintPricing ID", type: "uniqueidentifier", nullable: false),
                    PrintName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<float>(type: "real", nullable: false),
                    MinQuantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 27, 9, 14, 11, 780, DateTimeKind.Utc).AddTicks(3867)),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintPricings", x => x.PrintPricingID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<Guid>(name: "Role ID", type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "ThemeFilters",
                columns: table => new
                {
                    ThemeFilterID = table.Column<Guid>(name: "ThemeFilter ID", type: "uniqueidentifier", nullable: false),
                    ThemeFilterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThemeFilterDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemeFilters", x => x.ThemeFilterID);
                });

            migrationBuilder.CreateTable(
                name: "ThemeFrames",
                columns: table => new
                {
                    ThemeFrameID = table.Column<Guid>(name: "ThemeFrame ID", type: "uniqueidentifier", nullable: false),
                    ThemeFrameName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThemeFrameDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemeFrames", x => x.ThemeFrameID);
                });

            migrationBuilder.CreateTable(
                name: "ThemeStickers",
                columns: table => new
                {
                    ThemeStickerID = table.Column<Guid>(name: "ThemeSticker ID", type: "uniqueidentifier", nullable: false),
                    ThemeStickerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThemeStickerDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemeStickers", x => x.ThemeStickerID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<Guid>(name: "Account ID", type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "Role ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    FilterID = table.Column<Guid>(name: "Filter ID", type: "uniqueidentifier", nullable: false),
                    FilterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FilterURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 27, 9, 14, 11, 777, DateTimeKind.Utc).AddTicks(1149)),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThemeFilterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.FilterID);
                    table.ForeignKey(
                        name: "FK_Filters_ThemeFilters_ThemeFilterID",
                        column: x => x.ThemeFilterID,
                        principalTable: "ThemeFilters",
                        principalColumn: "ThemeFilter ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Frames",
                columns: table => new
                {
                    FrameID = table.Column<Guid>(name: "Frame ID", type: "uniqueidentifier", nullable: false),
                    FrameName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FrameURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 27, 9, 14, 11, 778, DateTimeKind.Utc).AddTicks(1040)),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThemeFrameID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frames", x => x.FrameID);
                    table.ForeignKey(
                        name: "FK_Frames_ThemeFrames_ThemeFrameID",
                        column: x => x.ThemeFrameID,
                        principalTable: "ThemeFrames",
                        principalColumn: "ThemeFrame ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stickers",
                columns: table => new
                {
                    StickerID = table.Column<Guid>(name: "Sticker ID", type: "uniqueidentifier", nullable: false),
                    StickerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StrickerURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 27, 9, 14, 11, 781, DateTimeKind.Utc).AddTicks(4975)),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThemeStickerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stickers", x => x.StickerID);
                    table.ForeignKey(
                        name: "FK_Stickers_ThemeStickers_ThemeStickerID",
                        column: x => x.ThemeStickerID,
                        principalTable: "ThemeStickers",
                        principalColumn: "ThemeSticker ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoBoothBranches",
                columns: table => new
                {
                    BranchesID = table.Column<Guid>(name: "Branches ID", type: "uniqueidentifier", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BranchAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoBoothBranches", x => x.BranchesID);
                    table.ForeignKey(
                        name: "FK_PhotoBoothBranches_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Account ID");
                });

            migrationBuilder.CreateTable(
                name: "Cameras",
                columns: table => new
                {
                    CameraID = table.Column<Guid>(name: "Camera ID", type: "uniqueidentifier", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LensType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoBoothBranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cameras", x => x.CameraID);
                    table.ForeignKey(
                        name: "FK_Cameras_PhotoBoothBranches_PhotoBoothBranchId",
                        column: x => x.PhotoBoothBranchId,
                        principalTable: "PhotoBoothBranches",
                        principalColumn: "Branches ID");
                });

            migrationBuilder.CreateTable(
                name: "Printers",
                columns: table => new
                {
                    PrinterID = table.Column<Guid>(name: "Printer ID", type: "uniqueidentifier", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PhotoBoothBranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printers", x => x.PrinterID);
                    table.ForeignKey(
                        name: "FK_Printers_PhotoBoothBranches_PhotoBoothBranchId",
                        column: x => x.PhotoBoothBranchId,
                        principalTable: "PhotoBoothBranches",
                        principalColumn: "Branches ID");
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionID = table.Column<Guid>(name: "Session ID", type: "uniqueidentifier", nullable: false),
                    PhotosTaken = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BranchesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrintPricingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LayoutID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_Sessions_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Account ID");
                    table.ForeignKey(
                        name: "FK_Sessions_Discounts_DiscountID",
                        column: x => x.DiscountID,
                        principalTable: "Discounts",
                        principalColumn: "Discount ID");
                    table.ForeignKey(
                        name: "FK_Sessions_Layouts_LayoutID",
                        column: x => x.LayoutID,
                        principalTable: "Layouts",
                        principalColumn: "Layout ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_PhotoBoothBranches_BranchesID",
                        column: x => x.BranchesID,
                        principalTable: "PhotoBoothBranches",
                        principalColumn: "Branches ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_PrintPricings_PrintPricingID",
                        column: x => x.PrintPricingID,
                        principalTable: "PrintPricings",
                        principalColumn: "PrintPricing ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinalPictures",
                columns: table => new
                {
                    PictureID = table.Column<Guid>(name: "Picture ID", type: "uniqueidentifier", nullable: false),
                    PictureURl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 5, 27, 9, 14, 11, 777, DateTimeKind.Utc).AddTicks(7312)),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PicturePrivacy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalPictures", x => x.PictureID);
                    table.ForeignKey(
                        name: "FK_FinalPictures_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Sessions",
                        principalColumn: "Session ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistories",
                columns: table => new
                {
                    TransactionID = table.Column<Guid>(name: "Transaction ID", type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionStatus = table.Column<int>(type: "int", nullable: false),
                    SessionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistories", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_TransactionHistories_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "Payment Method ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionHistories_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Sessions",
                        principalColumn: "Session ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EffectsPackLogs",
                columns: table => new
                {
                    PackID = table.Column<Guid>(name: "Pack ID", type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PictureID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FrameID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LayoutID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EffectsPackLogs_Frames_FrameID",
                        column: x => x.FrameID,
                        principalTable: "Frames",
                        principalColumn: "Frame ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectsPackLogs_Layouts_LayoutID",
                        column: x => x.LayoutID,
                        principalTable: "Layouts",
                        principalColumn: "Layout ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MapStickers",
                columns: table => new
                {
                    MapStickerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackLogID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StickerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapStickers", x => x.MapStickerID);
                    table.ForeignKey(
                        name: "FK_MapStickers_EffectsPackLogs_PackLogID",
                        column: x => x.PackLogID,
                        principalTable: "EffectsPackLogs",
                        principalColumn: "Pack ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapStickers_Stickers_StickerId",
                        column: x => x.StickerId,
                        principalTable: "Stickers",
                        principalColumn: "Sticker ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleID",
                table: "Accounts",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_PhotoBoothBranchId",
                table: "Cameras",
                column: "PhotoBoothBranchId",
                unique: true,
                filter: "[PhotoBoothBranchId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EffectsPackLogs_FilterID",
                table: "EffectsPackLogs",
                column: "FilterID");

            migrationBuilder.CreateIndex(
                name: "IX_EffectsPackLogs_FrameID",
                table: "EffectsPackLogs",
                column: "FrameID");

            migrationBuilder.CreateIndex(
                name: "IX_EffectsPackLogs_LayoutID",
                table: "EffectsPackLogs",
                column: "LayoutID");

            migrationBuilder.CreateIndex(
                name: "IX_EffectsPackLogs_PictureID",
                table: "EffectsPackLogs",
                column: "PictureID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Filters_ThemeFilterID",
                table: "Filters",
                column: "ThemeFilterID");

            migrationBuilder.CreateIndex(
                name: "IX_FinalPictures_SessionID",
                table: "FinalPictures",
                column: "SessionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Frames_ThemeFrameID",
                table: "Frames",
                column: "ThemeFrameID");

            migrationBuilder.CreateIndex(
                name: "IX_MapStickers_PackLogID",
                table: "MapStickers",
                column: "PackLogID");

            migrationBuilder.CreateIndex(
                name: "IX_MapStickers_StickerId",
                table: "MapStickers",
                column: "StickerId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoBoothBranches_AccountID",
                table: "PhotoBoothBranches",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Printers_PhotoBoothBranchId",
                table: "Printers",
                column: "PhotoBoothBranchId",
                unique: true,
                filter: "[PhotoBoothBranchId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_AccountID",
                table: "Sessions",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_BranchesID",
                table: "Sessions",
                column: "BranchesID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_DiscountID",
                table: "Sessions",
                column: "DiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_LayoutID",
                table: "Sessions",
                column: "LayoutID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_PrintPricingID",
                table: "Sessions",
                column: "PrintPricingID");

            migrationBuilder.CreateIndex(
                name: "IX_Stickers_ThemeStickerID",
                table: "Stickers",
                column: "ThemeStickerID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistories_PaymentMethodID",
                table: "TransactionHistories",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistories_SessionID",
                table: "TransactionHistories",
                column: "SessionID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cameras");

            migrationBuilder.DropTable(
                name: "MapStickers");

            migrationBuilder.DropTable(
                name: "Printers");

            migrationBuilder.DropTable(
                name: "TransactionHistories");

            migrationBuilder.DropTable(
                name: "EffectsPackLogs");

            migrationBuilder.DropTable(
                name: "Stickers");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropTable(
                name: "FinalPictures");

            migrationBuilder.DropTable(
                name: "Frames");

            migrationBuilder.DropTable(
                name: "ThemeStickers");

            migrationBuilder.DropTable(
                name: "ThemeFilters");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "ThemeFrames");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Layouts");

            migrationBuilder.DropTable(
                name: "PhotoBoothBranches");

            migrationBuilder.DropTable(
                name: "PrintPricings");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
