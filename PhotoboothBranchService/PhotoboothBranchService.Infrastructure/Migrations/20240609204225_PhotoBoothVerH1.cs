using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoBoothVerH1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    FilterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FilterURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 281, DateTimeKind.Utc).AddTicks(8237)),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.FilterID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentMethodName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 283, DateTimeKind.Utc).AddTicks(3871)),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    ServiceTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.ServiceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Stickers",
                columns: table => new
                {
                    StickerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StickerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StrickerURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 301, DateTimeKind.Utc).AddTicks(4519)),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stickers", x => x.StickerID);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    ThemeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThemeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThemeFrameDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.ThemeID);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    ServiceTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceID);
                    table.ForeignKey(
                        name: "FK_Services_ServiceTypes_ServiceTypeID",
                        column: x => x.ServiceTypeID,
                        principalTable: "ServiceTypes",
                        principalColumn: "ServiceTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Frames",
                columns: table => new
                {
                    FrameID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FrameName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FrameURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 282, DateTimeKind.Utc).AddTicks(1352)),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThemeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frames", x => x.FrameID);
                    table.ForeignKey(
                        name: "FK_Frames_Themes_ThemeID",
                        column: x => x.ThemeID,
                        principalTable: "Themes",
                        principalColumn: "ThemeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Layouts",
                columns: table => new
                {
                    LayoutID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LayoutURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 282, DateTimeKind.Utc).AddTicks(6470)),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FrameID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layouts", x => x.LayoutID);
                    table.ForeignKey(
                        name: "FK_Layouts_Frames_FrameID",
                        column: x => x.FrameID,
                        principalTable: "Frames",
                        principalColumn: "FrameID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountFBID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoBoothBranchID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "BoothBranches",
                columns: table => new
                {
                    BoothBranchID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BranchAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoothBranches", x => x.BoothBranchID);
                    table.ForeignKey(
                        name: "FK_BoothBranches_Accounts_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Booths",
                columns: table => new
                {
                    BoothID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoothName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoBoothBranchID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booths", x => x.BoothID);
                    table.ForeignKey(
                        name: "FK_Booths_BoothBranches_PhotoBoothBranchID",
                        column: x => x.PhotoBoothBranchID,
                        principalTable: "BoothBranches",
                        principalColumn: "BoothBranchID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoSessions",
                columns: table => new
                {
                    PhotoSessionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 286, DateTimeKind.Utc).AddTicks(5316)),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BoothID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoSessions", x => x.PhotoSessionID);
                    table.ForeignKey(
                        name: "FK_PhotoSessions_Booths_BoothID",
                        column: x => x.BoothID,
                        principalTable: "Booths",
                        principalColumn: "BoothID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionOrders",
                columns: table => new
                {
                    SessionOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 6, 9, 20, 42, 25, 301, DateTimeKind.Utc).AddTicks(1037)),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoBoothBranchID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoothID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionOrders", x => x.SessionOrderID);
                    table.ForeignKey(
                        name: "FK_SessionOrders_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionOrders_BoothBranches_PhotoBoothBranchID",
                        column: x => x.PhotoBoothBranchID,
                        principalTable: "BoothBranches",
                        principalColumn: "BoothBranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionOrders_Booths_BoothID",
                        column: x => x.BoothID,
                        principalTable: "Booths",
                        principalColumn: "BoothID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    PhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhotoSessionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FrameID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LayoutID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.PhotoID);
                    table.ForeignKey(
                        name: "FK_Photos_Filters_FilterID",
                        column: x => x.FilterID,
                        principalTable: "Filters",
                        principalColumn: "FilterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Photos_Frames_FrameID",
                        column: x => x.FrameID,
                        principalTable: "Frames",
                        principalColumn: "FrameID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Photos_Layouts_LayoutID",
                        column: x => x.LayoutID,
                        principalTable: "Layouts",
                        principalColumn: "LayoutID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_PhotoSessions_PhotoSessionID",
                        column: x => x.PhotoSessionID,
                        principalTable: "PhotoSessions",
                        principalColumn: "PhotoSessionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payment_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_SessionOrders_SessionOrderID",
                        column: x => x.SessionOrderID,
                        principalTable: "SessionOrders",
                        principalColumn: "SessionOrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceItems",
                columns: table => new
                {
                    ServiceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    PhotoSessionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceItems", x => x.ServiceItemID);
                    table.ForeignKey(
                        name: "FK_ServiceItems_PhotoSessions_PhotoSessionID",
                        column: x => x.PhotoSessionID,
                        principalTable: "PhotoSessions",
                        principalColumn: "PhotoSessionID");
                    table.ForeignKey(
                        name: "FK_ServiceItems_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceItems_SessionOrders_SessionOrderID",
                        column: x => x.SessionOrderID,
                        principalTable: "SessionOrders",
                        principalColumn: "SessionOrderID");
                });

            migrationBuilder.CreateTable(
                name: "PhotoStickers",
                columns: table => new
                {
                    PhotoStickerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    StickerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoStickers", x => x.PhotoStickerID);
                    table.ForeignKey(
                        name: "FK_PhotoStickers_Photos_StickerID",
                        column: x => x.StickerID,
                        principalTable: "Photos",
                        principalColumn: "PhotoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotoStickers_Stickers_StickerID",
                        column: x => x.StickerID,
                        principalTable: "Stickers",
                        principalColumn: "StickerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodID", "CreateDate", "PaymentMethodName", "Status" },
                values: new object[,]
                {
                    { new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"), new DateTime(2024, 6, 9, 20, 42, 25, 283, DateTimeKind.Utc).AddTicks(4613), "VNPay", "Active" },
                    { new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"), new DateTime(2024, 6, 9, 20, 42, 25, 283, DateTimeKind.Utc).AddTicks(4621), "MoMo", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PhotoBoothBranchID",
                table: "Accounts",
                column: "PhotoBoothBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_BoothBranches_ManagerID",
                table: "BoothBranches",
                column: "ManagerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booths_PhotoBoothBranchID",
                table: "Booths",
                column: "PhotoBoothBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Frames_ThemeID",
                table: "Frames",
                column: "ThemeID");

            migrationBuilder.CreateIndex(
                name: "IX_Layouts_FrameID",
                table: "Layouts",
                column: "FrameID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentMethodID",
                table: "Payment",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_SessionOrderID",
                table: "Payment",
                column: "SessionOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_FilterID",
                table: "Photos",
                column: "FilterID");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_FrameID",
                table: "Photos",
                column: "FrameID");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_LayoutID",
                table: "Photos",
                column: "LayoutID");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PhotoSessionID",
                table: "Photos",
                column: "PhotoSessionID");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoSessions_BoothID",
                table: "PhotoSessions",
                column: "BoothID");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoStickers_StickerID",
                table: "PhotoStickers",
                column: "StickerID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_PhotoSessionID",
                table: "ServiceItems",
                column: "PhotoSessionID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_ServiceID",
                table: "ServiceItems",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_SessionOrderID",
                table: "ServiceItems",
                column: "SessionOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceTypeID",
                table: "Services",
                column: "ServiceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_SessionOrders_AccountID",
                table: "SessionOrders",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_SessionOrders_BoothID",
                table: "SessionOrders",
                column: "BoothID");

            migrationBuilder.CreateIndex(
                name: "IX_SessionOrders_PhotoBoothBranchID",
                table: "SessionOrders",
                column: "PhotoBoothBranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_BoothBranches_PhotoBoothBranchID",
                table: "Accounts",
                column: "PhotoBoothBranchID",
                principalTable: "BoothBranches",
                principalColumn: "BoothBranchID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_BoothBranches_PhotoBoothBranchID",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "PhotoStickers");

            migrationBuilder.DropTable(
                name: "ServiceItems");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Stickers");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "SessionOrders");

            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropTable(
                name: "Layouts");

            migrationBuilder.DropTable(
                name: "PhotoSessions");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "Frames");

            migrationBuilder.DropTable(
                name: "Booths");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropTable(
                name: "BoothBranches");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
