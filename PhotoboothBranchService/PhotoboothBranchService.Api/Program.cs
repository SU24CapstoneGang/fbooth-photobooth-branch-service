using Microsoft.OpenApi.Models;
using PhotoboothBranchService.Api.Common.MiddleWares;
using PhotoboothBranchService.Application;
using PhotoboothBranchService.Application.Services.ConstantServices;
using PhotoboothBranchService.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Convert enum - option
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.DescribeAllParametersInCamelCase();
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

//http context accessor
builder.Services.AddHttpContextAccessor();

//add service from application layer and infrastructure layer
builder.Services
    .AddApplicaiton(builder.Configuration)
    .AddInfrastructure(builder.Configuration);


var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var constantService = scope.ServiceProvider.GetRequiredService<IConstantService>();
//    await constantService.LoadConstantsAsync();
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.UseJwtMiddleware();
app.MapControllers();

app.Run();
