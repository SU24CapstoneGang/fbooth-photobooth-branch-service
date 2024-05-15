using PhotoboothBranchService.Api.MiddleWare;
using PhotoboothBranchService.Application;
using PhotoboothBranchService.Application.Mapping;
using PhotoboothBranchService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;
builder.Services
    .AddApplicaiton()
    .AddInfrastructure(builder.Configuration); 
//services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FboothPhotoBranchService")));

//services.AddScoped<IAccountRepository, AccountRepository>();
//services.AddScoped<IPrinterRepository, PrinterRepository>();
//services.AddScoped<ICameraRepository, CameraRepository>();
//services.AddScoped<IPhotoBoothBranchRepository, PhotoBoothBranchRepository>();

//services.AddScoped<IAccountService, AccountsService>();

//services.AddAutoMapper(typeof(MappingProfile));
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
