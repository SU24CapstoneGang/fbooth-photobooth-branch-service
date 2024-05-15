using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoboothBranchService.Api.MiddleWare;
using PhotoboothBranchService.Application.Common.Utilities;
using PhotoboothBranchService.Application.Interfaces;
using PhotoboothBranchService.Application.Mapping;
using PhotoboothBranchService.Application.Service;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Interfaces;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using PhotoboothBranchService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;
services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FboothPhotoBranchService")));

services.AddScoped<IAccountRepository, AccountRepository>();
services.AddScoped<IPrinterRepository, PrinterRepository>();
services.AddScoped<ICameraRepository, CameraRepository>();
services.AddScoped<IPhotoBoothBranchRepository, PhotoBoothBranchRepository>();
services.AddScoped<IRoleRepository, RoleRepository>();
services.AddScoped<IFrameRepository, FrameRepository>();
services.AddScoped<IFilterRepository, FilterRepository>();
services.AddScoped<IStickerRepository, StickerRepository>();
services.AddScoped<ILayoutRepository, LayoutRepository>();

services.AddScoped<IAccountService, AccountService>();
services.AddScoped<ICameraService, CameraService>();
services.AddScoped<IPhotoBoothBranchService, PhotoBoothBranchService>();
services.AddScoped<IPrinterService, PrinterService>();
services.AddScoped<IRoleService, RoleService>();
services.AddScoped<IFilterService, FilterService>();
services.AddScoped<IFrameService, FrameService>();
services.AddScoped<IStickerService, StickerService>();
services.AddScoped<ILayoutService, LayoutService>();



services.AddScoped<IPasswordHasher, PasswordHasher>();

services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//configure exceptions to return swagger
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
