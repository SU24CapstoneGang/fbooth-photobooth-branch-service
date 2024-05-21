using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Infrastructure.Common.Configuration;
using PhotoboothBranchService.Infrastructure.Common.Extensions;

namespace PhotoboothBranchService.Infrastructure.Common.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Camera> Cameras { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<EffectsPackLog> EffectsPacks { get; set; }
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
    public DbSet<ThemeFilter> ThemeFilters { get; set; }
    public DbSet<ThemeFrame> ThemeFrames { get; set; }
    public DbSet<ThemeSticker> ThemeStickers { get; set; }
    public DbSet<MapSticker> MapStickers { get; set; }

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
        modelBuilder.ApplyConfiguration(new AccountConfigurations());
        modelBuilder.ApplyConfiguration(new CameraConfigurations());
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
        modelBuilder.ApplyConfiguration(new ThemeFilterConfigurations());
        modelBuilder.ApplyConfiguration(new ThemeFrameConfigurations());
        modelBuilder.ApplyConfiguration(new ThemeStickerConfigurations());
        modelBuilder.ApplyConfiguration(new MapStickerConfigurations());
    }
}
