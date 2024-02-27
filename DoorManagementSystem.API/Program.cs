using DoorManagementSystem.API.Middleware;
using DoorManagementSystem.Application.Interfaces.IRepositories;
using DoorManagementSystem.Application.Interfaces.IServices;
using DoorManagementSystem.Application.Mappers;
using DoorManagementSystem.Application.Services;
using DoorManagementSystem.Infrastructure;
using DoorManagementSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json",
                optional: true, reloadOnChange: true);
var configuration = builder.Configuration;

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers();
builder.Services.AddCors();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DoorManagementContext>(
        options => options.UseNpgsql(configuration.GetConnectionString("DoorManagementDatabase")));
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IDoorsRepository, DoorsRepository>();
builder.Services.AddScoped<IAccessControlRepository, AccessControlRepository>();
builder.Services.AddScoped<IDoorLogsRepository, DoorLogsRepository>();


builder.Services.AddScoped<IUsersService, UserService>();
builder.Services.AddScoped<IAccessControlService, AccessControlService>();
builder.Services.AddScoped<IDoorLogsService, DoorLogsService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration.GetValue<string>("JwtSettings:Issuer"),
                ValidAudience = configuration.GetValue<string>("JwtSettings:Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSettings:Key")))
            };
        });
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
