using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothV1 : Migration
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
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountID);
                });

            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    FilterID = table.Column<Guid>(name: "Filter ID", type: "uniqueidentifier", nullable: false),
                    FilterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FilterURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.FilterID);
                });

            migrationBuilder.CreateTable(
                name: "Frames",
                columns: table => new
                {
                    FrameID = table.Column<Guid>(name: "Frame ID", type: "uniqueidentifier", nullable: false),
                    FrameName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FrameURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frames", x => x.FrameID);
                });

            migrationBuilder.CreateTable(
                name: "Layouts",
                columns: table => new
                {
                    LayoutID = table.Column<Guid>(name: "Layout ID", type: "uniqueidentifier", nullable: false),
                    LayoutURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LayoutPrice = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layouts", x => x.LayoutID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentID = table.Column<Guid>(name: "Payment ID", type: "uniqueidentifier", nullable: false),
                    PaymentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentID);
                });

            migrationBuilder.CreateTable(
                name: "PrintPricings",
                columns: table => new
                {
                    PrintPricingID = table.Column<Guid>(name: "PrintPricing ID", type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "FinalPictures",
                columns: table => new
                {
                    PictureID = table.Column<Guid>(name: "Picture ID", type: "uniqueidentifier", nullable: false),
                    PictureURl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PicturePrivacy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintQuantity = table.Column<int>(type: "int", nullable: false),
                    PictureCost = table.Column<float>(type: "real", nullable: false),
                    LayoutID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrintPricingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalPictures", x => x.PictureID);
                    table.ForeignKey(
                        name: "FK_FinalPictures_Layouts_LayoutID",
                        column: x => x.LayoutID,
                        principalTable: "Layouts",
                        principalColumn: "Layout ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinalPictures_PrintPricings_PrintPricingID",
                        column: x => x.PrintPricingID,
                        principalTable: "PrintPricings",
                        principalColumn: "PrintPricing ID",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(name: "Order ID", type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantityOfPicture = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    SessionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PictureID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Account ID");
                    table.ForeignKey(
                        name: "FK_Orders_Discounts_DiscountID",
                        column: x => x.DiscountID,
                        principalTable: "Discounts",
                        principalColumn: "Discount ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_FinalPictures_PictureID",
                        column: x => x.PictureID,
                        principalTable: "FinalPictures",
                        principalColumn: "Picture ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentMethods_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "PaymentMethods",
                        principalColumn: "Payment ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoBoothBranches",
                columns: table => new
                {
                    BranchesID = table.Column<Guid>(name: "Branches ID", type: "uniqueidentifier", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BranchAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoBoothBranches", x => x.BranchesID);
                    table.ForeignKey(
                        name: "FK_PhotoBoothBranches_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Account ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistories",
                columns: table => new
                {
                    TransactionID = table.Column<Guid>(name: "Transaction ID", type: "uniqueidentifier", nullable: false),
                    FinalPictureNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistories", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_TransactionHistories_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "Account ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stickers",
                columns: table => new
                {
                    StickerID = table.Column<Guid>(name: "Sticker ID", type: "uniqueidentifier", nullable: false),
                    StickerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StrickerURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PackID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stickers", x => x.StickerID);
                    table.ForeignKey(
                        name: "FK_Stickers_EffectsPackLogs_PackID",
                        column: x => x.PackID,
                        principalTable: "EffectsPackLogs",
                        principalColumn: "Pack ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cameras",
                columns: table => new
                {
                    CameraID = table.Column<Guid>(name: "Camera ID", type: "uniqueidentifier", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LensType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lens = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoBoothBranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    PhotoBoothBranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printers", x => x.PrinterID);
                    table.ForeignKey(
                        name: "FK_Printers_PhotoBoothBranches_PhotoBoothBranchId",
                        column: x => x.PhotoBoothBranchId,
                        principalTable: "PhotoBoothBranches",
                        principalColumn: "Branches ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionID = table.Column<Guid>(name: "Session ID", type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BranchesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_Sessions_Orders_Session ID",
                        column: x => x.SessionID,
                        principalTable: "Orders",
                        principalColumn: "Order ID");
                    table.ForeignKey(
                        name: "FK_Sessions_PhotoBoothBranches_BranchesID",
                        column: x => x.BranchesID,
                        principalTable: "PhotoBoothBranches",
                        principalColumn: "Branches ID",
                        onDelete: ReferentialAction.Cascade);
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
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_FinalPictures_LayoutID",
                table: "FinalPictures",
                column: "LayoutID");

            migrationBuilder.CreateIndex(
                name: "IX_FinalPictures_PrintPricingID",
                table: "FinalPictures",
                column: "PrintPricingID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountID",
                table: "Orders",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DiscountID",
                table: "Orders",
                column: "DiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentID",
                table: "Orders",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PictureID",
                table: "Orders",
                column: "PictureID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoBoothBranches_AccountID",
                table: "PhotoBoothBranches",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Printers_PhotoBoothBranchId",
                table: "Printers",
                column: "PhotoBoothBranchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_BranchesID",
                table: "Sessions",
                column: "BranchesID");

            migrationBuilder.CreateIndex(
                name: "IX_Stickers_PackID",
                table: "Stickers",
                column: "PackID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistories_AccountID",
                table: "TransactionHistories",
                column: "AccountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cameras");

            migrationBuilder.DropTable(
                name: "Printers");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Stickers");

            migrationBuilder.DropTable(
                name: "TransactionHistories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PhotoBoothBranches");

            migrationBuilder.DropTable(
                name: "EffectsPackLogs");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropTable(
                name: "FinalPictures");

            migrationBuilder.DropTable(
                name: "Frames");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Layouts");

            migrationBuilder.DropTable(
                name: "PrintPricings");
        }
    }
}
