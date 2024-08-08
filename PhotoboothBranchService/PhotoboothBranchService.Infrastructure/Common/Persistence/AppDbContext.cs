using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Infrastructure.Common.Configuration;
using PhotoboothBranchService.Infrastructure.Common.Extensions;

namespace PhotoboothBranchService.Infrastructure.Common.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Booth> Booths { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Background> Backgrounds { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Layout> Layouts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<PhotoBox> PhotoBoxes { get; set; }
    public DbSet<Refund> Refunds { get; set; }
    public DbSet<PhotoSession> PhotoSessions { get; set; }
    public DbSet<PhotoSticker> PhotoStickers { get; set; }
    public DbSet<BookingService> BookingServices { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Sticker> Stickers { get; set; }
    public DbSet<FullPaymentPolicy> FullPaymentPolicies { get; set; }
    public DbSet<BookingSlot> BookingSlots { get; set; }
    public DbSet<Slot> Slots { get; set; }

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
        modelBuilder.ApplyConfiguration(new BoothConfigurations());
        modelBuilder.ApplyConfiguration(new BackgroundConfigurations());
        modelBuilder.ApplyConfiguration(new DeviceConfigurations());
        modelBuilder.ApplyConfiguration(new LayoutConfigurations());
        modelBuilder.ApplyConfiguration(new TransactionConfigurations());
        modelBuilder.ApplyConfiguration(new PaymentMethodConfigurations());
        modelBuilder.ApplyConfiguration(new BranchConfigurations());
        modelBuilder.ApplyConfiguration(new PhotoBoxConfigurations());
        modelBuilder.ApplyConfiguration(new RefundConfigurations());
        modelBuilder.ApplyConfiguration(new PhotoConfigurations());
        modelBuilder.ApplyConfiguration(new PhotoSessionConfigurations());
        modelBuilder.ApplyConfiguration(new PhotoStickerConfigurations());
        modelBuilder.ApplyConfiguration(new BookingServiceConfigurations());
        modelBuilder.ApplyConfiguration(new ServiceConfigurations());
        modelBuilder.ApplyConfiguration(new BookingConfigurations());
        modelBuilder.ApplyConfiguration(new StickerConfigurations());
        modelBuilder.ApplyConfiguration(new FullPaymentPolicyConfigurations());
        modelBuilder.ApplyConfiguration(new SlotConfiguration());
        modelBuilder.ApplyConfiguration(new BookingSlotConfiguration());
    }
}
