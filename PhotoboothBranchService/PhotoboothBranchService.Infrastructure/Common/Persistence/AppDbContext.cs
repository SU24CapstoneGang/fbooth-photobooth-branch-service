using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Common.Persistence;

public class AppDbContext: DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Camera> Cameras { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<EffectsPack> EffectsPacks { get; set; }
    public DbSet<Filter> Filters { get; set; }
    public DbSet<FinalPicture> FinalPictures { get; set; }
    public DbSet<Frame> Frames { get; set; }
    public DbSet<Layout> Layouts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<PhotoBoothBranch> PhotoBoothBranches { get; set; }
    public DbSet<Printer> Printers { get; set; }
    public DbSet<PrintPricing> PrintPricings { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Sticker> Stickers { get; set; }
    public DbSet<TransactionHistory> TransactionHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("FboothPhotoBranchService"));
    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public AppDbContext() { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfigurations());
        modelBuilder.ApplyConfiguration(new CameraConfigurations());
        modelBuilder.ApplyConfiguration(new CustomerConfigurations());
        modelBuilder.ApplyConfiguration(new DiscountConfigurations());
        modelBuilder.ApplyConfiguration(new EffectPackConfigurations());
        modelBuilder.ApplyConfiguration(new FilterConfigurations());
        modelBuilder.ApplyConfiguration(new FinalPictureConfigurations());
        modelBuilder.ApplyConfiguration(new FrameConfigurations());
        modelBuilder.ApplyConfiguration(new LayoutConfigurations());
        modelBuilder.ApplyConfiguration(new OrderConfigurations());
        modelBuilder.ApplyConfiguration(new PaymentMethodConfigurations());
        modelBuilder.ApplyConfiguration(new PhotoBoothBranchConfigurations());
        modelBuilder.ApplyConfiguration(new PrinterConfigurations());
        modelBuilder.ApplyConfiguration(new PrintPricingConfigurations());
        modelBuilder.ApplyConfiguration(new RoleConfigurations());
        modelBuilder.ApplyConfiguration(new SessionConfigurations());
        modelBuilder.ApplyConfiguration(new StickerConfigurations());
        modelBuilder.ApplyConfiguration(new TransactionHistoryConfigurations());
    }
}
