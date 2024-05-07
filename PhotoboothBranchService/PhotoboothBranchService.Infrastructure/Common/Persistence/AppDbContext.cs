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
    public DbSet<Accounts> Accounts { get; set; } = null;
    public DbSet<Cameras> Cameras { get; set; } = null;
    public DbSet<PhotoBoothBranches> PhotoBoothBranches { get; set; } = null;
    public DbSet<Printers> Printers { get; set; } = null;

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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfigurations());
        modelBuilder.ApplyConfiguration(new CameraConfigurations());
        modelBuilder.ApplyConfiguration(new PhotoBoothBranchesConfiguration());
        modelBuilder.ApplyConfiguration(new PrintersConfiguration());
    }
}
