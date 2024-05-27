﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhotoboothBranchService.Infrastructure.Common.Persistence;

#nullable disable

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240527190533_PhotoBoothBranchServicev5")]
    partial class PhotoBoothBranchServicev5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AccountID");

                    b.Property<string>("AccountFBID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<Guid>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleID");

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Camera", b =>
                {
                    b.Property<Guid>("CameraID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CameraID");

                    b.Property<string>("LensType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CameraID");

                    b.ToTable("Cameras", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Discount", b =>
                {
                    b.Property<Guid>("DiscountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("DiscountID");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 5, 27, 19, 5, 33, 136, DateTimeKind.Utc).AddTicks(7307));

                    b.Property<string>("DiscountCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("DiscountRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("RemaniningUsage")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DiscountID");

                    b.ToTable("Discounts", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.EffectsPackLog", b =>
                {
                    b.Property<Guid>("PacklogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PackID");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("FilterID")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FrameID")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LayoutID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PictureID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PacklogID");

                    b.HasIndex("FilterID");

                    b.HasIndex("FrameID");

                    b.HasIndex("LayoutID");

                    b.HasIndex("PictureID")
                        .IsUnique();

                    b.ToTable("EffectsPackLogs", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Filter", b =>
                {
                    b.Property<Guid>("FilterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("FilterID");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 5, 27, 19, 5, 33, 140, DateTimeKind.Utc).AddTicks(2044));

                    b.Property<string>("FilterName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FilterURL")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ThemeFilterID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FilterID");

                    b.HasIndex("ThemeFilterID");

                    b.ToTable("Filters", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.FinalPicture", b =>
                {
                    b.Property<Guid>("PictureID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PictureID");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 5, 27, 19, 5, 33, 141, DateTimeKind.Utc).AddTicks(7));

                    b.Property<DateTime?>("LastModified")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("PicturePrivacy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureURl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("SessionID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PictureID");

                    b.HasIndex("SessionID")
                        .IsUnique();

                    b.ToTable("FinalPictures", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Frame", b =>
                {
                    b.Property<Guid>("FrameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("FrameID");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 5, 27, 19, 5, 33, 141, DateTimeKind.Utc).AddTicks(3090));

                    b.Property<string>("FrameName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FrameURL")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ThemeFrameID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FrameID");

                    b.HasIndex("ThemeFrameID");

                    b.ToTable("Frames", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Layout", b =>
                {
                    b.Property<Guid>("LayoutID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("LayoutID");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 5, 27, 19, 5, 33, 141, DateTimeKind.Utc).AddTicks(7527));

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<float>("LayoutPrice")
                        .HasColumnType("real");

                    b.Property<string>("LayoutURL")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("LayoutID");

                    b.ToTable("Layouts", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.MapSticker", b =>
                {
                    b.Property<Guid>("MapStickerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("MapStickerID");

                    b.Property<Guid>("PackLogID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StickerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MapStickerID");

                    b.HasIndex("PackLogID");

                    b.HasIndex("StickerId");

                    b.ToTable("MapStickers", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PaymentMethod", b =>
                {
                    b.Property<Guid>("PaymentMethodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PaymentMethodID");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 5, 27, 19, 5, 33, 142, DateTimeKind.Utc).AddTicks(1031));

                    b.Property<string>("PaymentMethodName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentMethodID");

                    b.ToTable("PaymentMethods", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PhotoBoothBranch", b =>
                {
                    b.Property<Guid>("PhotoBoothBranchID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PhotoBoothBranchID");

                    b.Property<Guid?>("AccountID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BranchAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("CameraID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PrinterID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PhotoBoothBranchID");

                    b.HasIndex("AccountID");

                    b.HasIndex("CameraID")
                        .IsUnique()
                        .HasFilter("[CameraID] IS NOT NULL");

                    b.HasIndex("PrinterID")
                        .IsUnique()
                        .HasFilter("[PrinterID] IS NOT NULL");

                    b.ToTable("PhotoBoothBranches", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PrintPricing", b =>
                {
                    b.Property<Guid>("PrintPricingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PrintPricingID");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 5, 27, 19, 5, 33, 146, DateTimeKind.Utc).AddTicks(5575));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("MinQuantity")
                        .HasColumnType("int");

                    b.Property<string>("PrintName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("UnitPrice")
                        .HasColumnType("real");

                    b.HasKey("PrintPricingID");

                    b.ToTable("PrintPricings", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Printer", b =>
                {
                    b.Property<Guid>("PrinterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PrinterID");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrinterID");

                    b.ToTable("Printers", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RoleID");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Session", b =>
                {
                    b.Property<Guid>("SessionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("SessionID");

                    b.Property<Guid?>("AccountID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchesID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DiscountID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LayoutID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PhotosTaken")
                        .HasColumnType("int");

                    b.Property<Guid>("PrintPricingID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("SessionID");

                    b.HasIndex("AccountID");

                    b.HasIndex("BranchesID");

                    b.HasIndex("DiscountID");

                    b.HasIndex("LayoutID");

                    b.HasIndex("PrintPricingID");

                    b.ToTable("Sessions", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Sticker", b =>
                {
                    b.Property<Guid>("StickerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StickerID");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 5, 27, 19, 5, 33, 147, DateTimeKind.Utc).AddTicks(8010));

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StickerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StrickerURL")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("ThemeStickerID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StickerID");

                    b.HasIndex("ThemeStickerID");

                    b.ToTable("Stickers", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.ThemeFilter", b =>
                {
                    b.Property<Guid>("ThemeFilterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ThemeFilterID");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThemeFilterDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ThemeFilterName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ThemeFilterID");

                    b.ToTable("ThemeFilters", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.ThemeFrame", b =>
                {
                    b.Property<Guid>("ThemeFrameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ThemeFrameID");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThemeFrameDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ThemeFrameName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ThemeFrameID");

                    b.ToTable("ThemeFrames", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.ThemeSticker", b =>
                {
                    b.Property<Guid>("ThemeStickerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ThemeStickerID");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThemeStickerDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ThemeStickerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ThemeStickerID");

                    b.ToTable("ThemeStickers", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.TransactionHistory", b =>
                {
                    b.Property<Guid>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TransactionID");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PaymentMethodID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SessionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TransactionStatus")
                        .HasColumnType("int");

                    b.HasKey("TransactionID");

                    b.HasIndex("PaymentMethodID");

                    b.HasIndex("SessionID")
                        .IsUnique();

                    b.ToTable("TransactionHistories", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Account", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.EffectsPackLog", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.Filter", "Filter")
                        .WithMany("EffectsPackLogs")
                        .HasForeignKey("FilterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoboothBranchService.Domain.Entities.Frame", "Frame")
                        .WithMany("EffectsPackLogs")
                        .HasForeignKey("FrameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoboothBranchService.Domain.Entities.Layout", "Layout")
                        .WithMany("EffectsPackLogs")
                        .HasForeignKey("LayoutID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoboothBranchService.Domain.Entities.FinalPicture", "FinalPicture")
                        .WithOne("EffectsPackLog")
                        .HasForeignKey("PhotoboothBranchService.Domain.Entities.EffectsPackLog", "PictureID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Filter");

                    b.Navigation("FinalPicture");

                    b.Navigation("Frame");

                    b.Navigation("Layout");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Filter", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.ThemeFilter", "ThemeFilter")
                        .WithMany("Filters")
                        .HasForeignKey("ThemeFilterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThemeFilter");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.FinalPicture", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.Session", "Session")
                        .WithOne("FinalPicture")
                        .HasForeignKey("PhotoboothBranchService.Domain.Entities.FinalPicture", "SessionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Session");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Frame", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.ThemeFrame", "ThemeFrame")
                        .WithMany("Frames")
                        .HasForeignKey("ThemeFrameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThemeFrame");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.MapSticker", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.EffectsPackLog", "EffectsPackLog")
                        .WithMany("MapStickers")
                        .HasForeignKey("PackLogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoboothBranchService.Domain.Entities.Sticker", "Sticker")
                        .WithMany("MapStickers")
                        .HasForeignKey("StickerId");

                    b.Navigation("EffectsPackLog");

                    b.Navigation("Sticker");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PhotoBoothBranch", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.Account", "Account")
                        .WithMany("PhotoBoothBranches")
                        .HasForeignKey("AccountID");

                    b.HasOne("PhotoboothBranchService.Domain.Entities.Camera", "Camera")
                        .WithOne("PhotoBoothBranch")
                        .HasForeignKey("PhotoboothBranchService.Domain.Entities.PhotoBoothBranch", "CameraID");

                    b.HasOne("PhotoboothBranchService.Domain.Entities.Printer", "Printer")
                        .WithOne("PhotoBoothBranch")
                        .HasForeignKey("PhotoboothBranchService.Domain.Entities.PhotoBoothBranch", "PrinterID");

                    b.Navigation("Account");

                    b.Navigation("Camera");

                    b.Navigation("Printer");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Session", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.Account", "Account")
                        .WithMany("Sessions")
                        .HasForeignKey("AccountID");

                    b.HasOne("PhotoboothBranchService.Domain.Entities.PhotoBoothBranch", "PhotoBoothBranch")
                        .WithMany("Sessions")
                        .HasForeignKey("BranchesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoboothBranchService.Domain.Entities.Discount", "Discount")
                        .WithMany("Sessions")
                        .HasForeignKey("DiscountID");

                    b.HasOne("PhotoboothBranchService.Domain.Entities.Layout", "Layout")
                        .WithMany("Sessions")
                        .HasForeignKey("LayoutID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoboothBranchService.Domain.Entities.PrintPricing", "PrintPricing")
                        .WithMany("Sessions")
                        .HasForeignKey("PrintPricingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Discount");

                    b.Navigation("Layout");

                    b.Navigation("PhotoBoothBranch");

                    b.Navigation("PrintPricing");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Sticker", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.ThemeSticker", "ThemeSticker")
                        .WithMany("Stickers")
                        .HasForeignKey("ThemeStickerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThemeSticker");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.TransactionHistory", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.PaymentMethod", "PaymentMethod")
                        .WithMany("TransactionHistories")
                        .HasForeignKey("PaymentMethodID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PhotoboothBranchService.Domain.Entities.Session", "Session")
                        .WithOne("TransactionHistory")
                        .HasForeignKey("PhotoboothBranchService.Domain.Entities.TransactionHistory", "SessionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PaymentMethod");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Account", b =>
                {
                    b.Navigation("PhotoBoothBranches");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Camera", b =>
                {
                    b.Navigation("PhotoBoothBranch");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Discount", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.EffectsPackLog", b =>
                {
                    b.Navigation("MapStickers");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Filter", b =>
                {
                    b.Navigation("EffectsPackLogs");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.FinalPicture", b =>
                {
                    b.Navigation("EffectsPackLog")
                        .IsRequired();
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Frame", b =>
                {
                    b.Navigation("EffectsPackLogs");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Layout", b =>
                {
                    b.Navigation("EffectsPackLogs");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PaymentMethod", b =>
                {
                    b.Navigation("TransactionHistories");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PhotoBoothBranch", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PrintPricing", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Printer", b =>
                {
                    b.Navigation("PhotoBoothBranch");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Session", b =>
                {
                    b.Navigation("FinalPicture")
                        .IsRequired();

                    b.Navigation("TransactionHistory")
                        .IsRequired();
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Sticker", b =>
                {
                    b.Navigation("MapStickers");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.ThemeFilter", b =>
                {
                    b.Navigation("Filters");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.ThemeFrame", b =>
                {
                    b.Navigation("Frames");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.ThemeSticker", b =>
                {
                    b.Navigation("Stickers");
                });
#pragma warning restore 612, 618
        }
    }
}
