using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Infrastructure.Common.Configuration;
using PhotoboothBranchService.Infrastructure.Common.Extensions;

namespace PhotoboothBranchService.Infrastructure.Common.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Filter> Filters { get; set; }
    public DbSet<Photo> FinalPictures { get; set; }
    public DbSet<Frame> Frames { get; set; }
    public DbSet<Layout> Layouts { get; set; }
    public DbSet<PhotoBoothBranch> PhotoBoothBranches { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<SessionOrder> Sessions { get; set; }
    public DbSet<Sticker> Stickers { get; set; }
    public DbSet<Theme> ThemeFrames { get; set; }
    public DbSet<PhotoSticker> MapStickers { get; set; }

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
        modelBuilder.ApplyConfiguration(new FilterConfigurations());
        modelBuilder.ApplyConfiguration(new FinalPictureConfigurations());
        modelBuilder.ApplyConfiguration(new FrameConfigurations());
        modelBuilder.ApplyConfiguration(new LayoutConfigurations());
        modelBuilder.ApplyConfiguration(new PhotoBoothBranchConfigurations());
        modelBuilder.ApplyConfiguration(new RoleConfigurations());
        modelBuilder.ApplyConfiguration(new SessionOrderConfigurations());
        modelBuilder.ApplyConfiguration(new StickerConfigurations());
        modelBuilder.ApplyConfiguration(new ThemeConfigurations());
        modelBuilder.ApplyConfiguration(new PhotoStickerConfigurations());
    }
}
