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
    [Migration("20240624234703_PhotoBoothVerH8")]
    partial class PhotoBoothVerH8
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

                    b.Property<Guid?>("PhotoBoothBranchID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhotoBoothBranchID");

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Background", b =>
                {
                    b.Property<Guid>("BackgroundID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BackgroundID");

                    b.Property<string>("BackgroundCode")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("BackgroundURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CouldID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LayoutID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Lenght")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("BackgroundID");

                    b.HasIndex("LayoutID");

                    b.ToTable("Backgrounds", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Booth", b =>
                {
                    b.Property<Guid>("BoothID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BoothID");

                    b.Property<string>("BoothName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("PhotoBoothBranchID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BoothID");

                    b.HasIndex("PhotoBoothBranchID");

                    b.ToTable("Booths", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.BoothBranch", b =>
                {
                    b.Property<Guid>("BoothBranchID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BoothBranchID");

                    b.Property<string>("BranchAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<Guid>("ManagerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BoothBranchID");

                    b.HasIndex("ManagerID")
                        .IsUnique();

                    b.ToTable("BoothBranches", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Layout", b =>
                {
                    b.Property<Guid>("LayoutID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("LayoutID");

                    b.Property<string>("CouldID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LayoutCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LayoutURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lenght")
                        .HasColumnType("int");

                    b.Property<short>("PhotoSlot")
                        .HasColumnType("smallint");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("LayoutID");

                    b.ToTable("Layouts", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PaymentID");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<string>("ClientIpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PaymentDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PaymentMethodID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SessionOrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Signature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentID");

                    b.HasIndex("PaymentMethodID");

                    b.HasIndex("SessionOrderID");

                    b.ToTable("Payment", (string)null);
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
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("PaymentMethodName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentMethodID");

                    b.ToTable("PaymentMethods", (string)null);

                    b.HasData(
                        new
                        {
                            PaymentMethodID = new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                            CreateDate = new DateTime(2024, 6, 24, 23, 47, 3, 48, DateTimeKind.Utc).AddTicks(7008),
                            PaymentMethodName = "VNPay",
                            Status = "Active"
                        },
                        new
                        {
                            PaymentMethodID = new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                            CreateDate = new DateTime(2024, 6, 24, 23, 47, 3, 48, DateTimeKind.Utc).AddTicks(7020),
                            PaymentMethodName = "MoMo",
                            Status = "Active"
                        });
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Photo", b =>
                {
                    b.Property<Guid>("PhotoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PhotoID");

                    b.Property<Guid>("BackgroundID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CouldID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<Guid>("PhotoSessionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhotoURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PhotoID");

                    b.HasIndex("BackgroundID");

                    b.HasIndex("PhotoSessionID");

                    b.ToTable("Photos", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PhotoBox", b =>
                {
                    b.Property<Guid>("PhotoBoxID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PhotoBoxID");

                    b.Property<int>("CoordinatesX")
                        .HasColumnType("int");

                    b.Property<int>("CoordinatesY")
                        .HasColumnType("int");

                    b.Property<Guid>("LayoutID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("boxLength")
                        .HasColumnType("int");

                    b.Property<int>("boxWidth")
                        .HasColumnType("int");

                    b.HasKey("PhotoBoxID");

                    b.HasIndex("LayoutID");

                    b.ToTable("PhotoBox", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PhotoSession", b =>
                {
                    b.Property<Guid>("PhotoSessionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PhotoSessionID");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LayoutID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SessionIndex")
                        .HasColumnType("int");

                    b.Property<Guid>("SessionOrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TotalPhotoTaken")
                        .HasColumnType("int");

                    b.Property<long>("ValidateCode")
                        .HasColumnType("bigint");

                    b.HasKey("PhotoSessionID");

                    b.HasIndex("LayoutID");

                    b.HasIndex("SessionOrderID");

                    b.ToTable("PhotoSessions", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PhotoSticker", b =>
                {
                    b.Property<Guid>("PhotoStickerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PhotoStickerID");

                    b.Property<Guid>("PhotoID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint");

                    b.Property<Guid?>("StickerID")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PhotoStickerID");

                    b.HasIndex("StickerID");

                    b.ToTable("PhotoStickers", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Service", b =>
                {
                    b.Property<Guid>("ServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ServiceID");

                    b.Property<int>("Measure")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ServiceDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ServiceTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceID");

                    b.HasIndex("ServiceTypeID");

                    b.ToTable("Services", (string)null);

                    b.HasData(
                        new
                        {
                            ServiceID = new Guid("c51a05bb-28af-4315-b888-606376ba061b"),
                            Measure = 30,
                            Price = 90000m,
                            ServiceDescription = "Take Photo in 30 minutes",
                            ServiceName = "Take Photo in 30 minutes",
                            ServiceTypeID = new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"),
                            Unit = "minutes"
                        },
                        new
                        {
                            ServiceID = new Guid("30c2a7fb-9164-4164-9f1b-7c334744e559"),
                            Measure = 15,
                            Price = 50000m,
                            ServiceDescription = "Take Photo in 15 minutes",
                            ServiceName = "Take Photo in 15 minutes",
                            ServiceTypeID = new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"),
                            Unit = "minutes"
                        },
                        new
                        {
                            ServiceID = new Guid("9b0dc075-557c-4b29-b3ba-c8b5841c4c68"),
                            Measure = 60,
                            Price = 180000m,
                            ServiceDescription = "Take Photo in 60 minutes",
                            ServiceName = "Take Photo in 60 minutes",
                            ServiceTypeID = new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"),
                            Unit = "minutes"
                        },
                        new
                        {
                            ServiceID = new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"),
                            Measure = 1,
                            Price = 100000m,
                            ServiceDescription = "Make up with Korean stype for 1 people",
                            ServiceName = "Make up with Korean stype for 1 people",
                            ServiceTypeID = new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"),
                            Unit = "people"
                        },
                        new
                        {
                            ServiceID = new Guid("13e75fc2-f38b-401e-9cd2-c545b80fd1f0"),
                            Measure = 2,
                            Price = 190000m,
                            ServiceDescription = "Make up with Korean stype for 2 people",
                            ServiceName = "Combo make up with Korean stype for 2 people",
                            ServiceTypeID = new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"),
                            Unit = "people"
                        },
                        new
                        {
                            ServiceID = new Guid("5d568e18-8883-409b-bc48-6456aeefb4f9"),
                            Measure = 120,
                            Price = 190000m,
                            ServiceDescription = "Hire this booth for 120 minutes",
                            ServiceName = "Hire this booth for 120 minutes",
                            ServiceTypeID = new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"),
                            Unit = "minutes"
                        });
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.ServiceItem", b =>
                {
                    b.Property<Guid>("ServiceItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ServiceItemID");

                    b.Property<Guid?>("PhotoSessionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint");

                    b.Property<Guid>("ServiceID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SessionOrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("ServiceItemID");

                    b.HasIndex("PhotoSessionID");

                    b.HasIndex("ServiceID");

                    b.HasIndex("SessionOrderID");

                    b.ToTable("ServiceItems", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.ServiceType", b =>
                {
                    b.Property<Guid>("ServiceTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ServiceTypeID");

                    b.Property<string>("ServiceTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceTypeID");

                    b.ToTable("ServiceTypes", (string)null);

                    b.HasData(
                        new
                        {
                            ServiceTypeID = new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671"),
                            ServiceTypeName = "Take photo"
                        },
                        new
                        {
                            ServiceTypeID = new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"),
                            ServiceTypeName = "Make up"
                        },
                        new
                        {
                            ServiceTypeID = new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"),
                            ServiceTypeName = "Hire booth"
                        },
                        new
                        {
                            ServiceTypeID = new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"),
                            ServiceTypeName = "Relate to \"Take Photo\""
                        });
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.SessionOrder", b =>
                {
                    b.Property<Guid>("SessionOrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("SessionOrderID");

                    b.Property<Guid?>("AccountID")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BoothID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("SessionOrderID");

                    b.HasIndex("AccountID");

                    b.HasIndex("BoothID");

                    b.ToTable("SessionOrders", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Sticker", b =>
                {
                    b.Property<Guid>("StickerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StickerID");

                    b.Property<string>("CouldID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StickerCode")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StrickerURL")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("StickerID");

                    b.ToTable("Stickers", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Account", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.BoothBranch", "BoothBranchBelong")
                        .WithMany("Staffs")
                        .HasForeignKey("PhotoBoothBranchID");

                    b.Navigation("BoothBranchBelong");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Background", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.Layout", "Layout")
                        .WithMany("Backgrounds")
                        .HasForeignKey("LayoutID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Layout");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Booth", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.BoothBranch", "PhotoBoothBranch")
                        .WithMany("Booths")
                        .HasForeignKey("PhotoBoothBranchID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhotoBoothBranch");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.BoothBranch", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.Account", "Manager")
                        .WithOne("BoothBranchManage")
                        .HasForeignKey("PhotoboothBranchService.Domain.Entities.BoothBranch", "ManagerID");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Payment", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.PaymentMethod", "PaymentMethod")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentMethodID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoboothBranchService.Domain.Entities.SessionOrder", "SessionOrder")
                        .WithMany("Payments")
                        .HasForeignKey("SessionOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentMethod");

                    b.Navigation("SessionOrder");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Photo", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.Background", "Background")
                        .WithMany("Photos")
                        .HasForeignKey("BackgroundID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoboothBranchService.Domain.Entities.PhotoSession", "PhotoSession")
                        .WithMany("Photos")
                        .HasForeignKey("PhotoSessionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Background");

                    b.Navigation("PhotoSession");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PhotoBox", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.Layout", "Layout")
                        .WithMany("PhotoBoxes")
                        .HasForeignKey("LayoutID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Layout");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PhotoSession", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.Layout", "Layout")
                        .WithMany("PhotoSessions")
                        .HasForeignKey("LayoutID");

                    b.HasOne("PhotoboothBranchService.Domain.Entities.SessionOrder", "SessionOrder")
                        .WithMany("PhotoSessions")
                        .HasForeignKey("SessionOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Layout");

                    b.Navigation("SessionOrder");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PhotoSticker", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.Photo", "Photo")
                        .WithMany("PhotoStickers")
                        .HasForeignKey("StickerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoboothBranchService.Domain.Entities.Sticker", "Sticker")
                        .WithMany("PhotoSticker")
                        .HasForeignKey("StickerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photo");

                    b.Navigation("Sticker");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Service", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.ServiceType", "ServiceType")
                        .WithMany("Services")
                        .HasForeignKey("ServiceTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceType");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.ServiceItem", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.PhotoSession", "PhotoSession")
                        .WithMany("ServiceItems")
                        .HasForeignKey("PhotoSessionID");

                    b.HasOne("PhotoboothBranchService.Domain.Entities.Service", "Service")
                        .WithMany("ServiceItems")
                        .HasForeignKey("ServiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoboothBranchService.Domain.Entities.SessionOrder", "SessionOrder")
                        .WithMany("ServiceItems")
                        .HasForeignKey("SessionOrderID");

                    b.Navigation("PhotoSession");

                    b.Navigation("Service");

                    b.Navigation("SessionOrder");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.SessionOrder", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.Account", "Account")
                        .WithMany("SessionOrder")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoboothBranchService.Domain.Entities.Booth", "Booth")
                        .WithMany("SessionOrders")
                        .HasForeignKey("BoothID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Booth");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Account", b =>
                {
                    b.Navigation("BoothBranchManage")
                        .IsRequired();

                    b.Navigation("SessionOrder");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Background", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Booth", b =>
                {
                    b.Navigation("SessionOrders");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.BoothBranch", b =>
                {
                    b.Navigation("Booths");

                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Layout", b =>
                {
                    b.Navigation("Backgrounds");

                    b.Navigation("PhotoBoxes");

                    b.Navigation("PhotoSessions");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PaymentMethod", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Photo", b =>
                {
                    b.Navigation("PhotoStickers");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PhotoSession", b =>
                {
                    b.Navigation("Photos");

                    b.Navigation("ServiceItems");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Service", b =>
                {
                    b.Navigation("ServiceItems");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.ServiceType", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.SessionOrder", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("PhotoSessions");

                    b.Navigation("ServiceItems");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Sticker", b =>
                {
                    b.Navigation("PhotoSticker");
                });
#pragma warning restore 612, 618
        }
    }
}
