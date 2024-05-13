﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhotoboothBranchService.Infrastructure.Common.Persistence;

#nullable disable

namespace PhotoboothBranchService.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Accounts", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Account ID");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 5, 12, 13, 44, 9, 254, DateTimeKind.Utc).AddTicks(1275));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid?>("PhotoBoothBranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Cameras", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Camera ID");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 5, 12, 13, 44, 9, 254, DateTimeKind.Utc).AddTicks(5630));

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Lens")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("SensorType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Cameras", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PhotoBoothBranches", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PhotoBoothBranch ID");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BranchAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("CameraId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 5, 12, 13, 44, 9, 255, DateTimeKind.Utc).AddTicks(419));

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PrinterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique()
                        .HasFilter("[AccountId] IS NOT NULL");

                    b.HasIndex("CameraId")
                        .IsUnique()
                        .HasFilter("[CameraId] IS NOT NULL");

                    b.HasIndex("PrinterId")
                        .IsUnique()
                        .HasFilter("[PrinterId] IS NOT NULL");

                    b.ToTable("PhotoBoothBranches", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Printers", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Printer ID");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 5, 12, 13, 44, 9, 267, DateTimeKind.Utc).AddTicks(8648));

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Printers", (string)null);
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.PhotoBoothBranches", b =>
                {
                    b.HasOne("PhotoboothBranchService.Domain.Entities.Accounts", "Account")
                        .WithOne("PhotoBoothBranch")
                        .HasForeignKey("PhotoboothBranchService.Domain.Entities.PhotoBoothBranches", "AccountId");

                    b.HasOne("PhotoboothBranchService.Domain.Entities.Cameras", "Camera")
                        .WithOne("PhotoBoothBranch")
                        .HasForeignKey("PhotoboothBranchService.Domain.Entities.PhotoBoothBranches", "CameraId");

                    b.HasOne("PhotoboothBranchService.Domain.Entities.Printers", "Printer")
                        .WithOne("PhotoBoothBranch")
                        .HasForeignKey("PhotoboothBranchService.Domain.Entities.PhotoBoothBranches", "PrinterId");

                    b.Navigation("Account");

                    b.Navigation("Camera");

                    b.Navigation("Printer");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Accounts", b =>
                {
                    b.Navigation("PhotoBoothBranch");
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Cameras", b =>
                {
                    b.Navigation("PhotoBoothBranch")
                        .IsRequired();
                });

            modelBuilder.Entity("PhotoboothBranchService.Domain.Entities.Printers", b =>
                {
                    b.Navigation("PhotoBoothBranch")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
