﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Infrastructure.Common.Configuration;
using PhotoboothBranchService.Infrastructure.Common.Extensions;

namespace PhotoboothBranchService.Infrastructure.Common.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Booth> Booths {get; set; }
    public DbSet<Filter> Filters { get; set; }
    public DbSet<Frame> Frames { get; set; }
    public DbSet<Layout> Layouts { get; set; }
    public DbSet<Payment> Payments {get; set; }
    public DbSet<PaymentMethod> PaymentMethods {get; set; }
    public DbSet<Photo> Photos {get; set; }
    public DbSet<PhotoBoothBranch> PhotoBoothBranches { get; set; }
    public DbSet<PhotoSession> PhotoSessions {get; set; }
    public DbSet<PhotoSticker> PhotoStickers {get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Service> Services {get; set; }
    public DbSet<ServiceItem> ServiceItems {get; set; }
    public DbSet<ServiceType> ServiceTypes {get; set; }
    public DbSet<SessionOrder> SessionOrders { get; set; }
    public DbSet<Sticker> Stickers { get; set; }
    public DbSet<Theme> Themes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Fbooth"));
        }
    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverterExtensions>()
            .HaveColumnType("date");

        base.ConfigureConventions(builder);
    }

    public AppDbContext() { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AccountConfigurations());
        modelBuilder.ApplyConfiguration(new BoothConfiguration());
        modelBuilder.ApplyConfiguration(new FilterConfigurations());
        modelBuilder.ApplyConfiguration(new FrameConfigurations());
        modelBuilder.ApplyConfiguration(new LayoutConfigurations());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentMethodConfigurations());
        modelBuilder.ApplyConfiguration(new PhotoBoothBranchConfigurations());
        modelBuilder.ApplyConfiguration(new PhotoSessionConfiduration());
        modelBuilder.ApplyConfiguration(new PhotoStickerConfigurations());
        modelBuilder.ApplyConfiguration(new RoleConfigurations());
        modelBuilder.ApplyConfiguration(new ServiceConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceItemConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SessionOrderConfigurations());
        modelBuilder.ApplyConfiguration(new StickerConfigurations());
        modelBuilder.ApplyConfiguration(new ThemeConfigurations());
    }
}
