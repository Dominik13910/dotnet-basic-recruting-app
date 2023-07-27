using FluentValidation;
using FluentValidation.AspNetCore;
using MatchDataManager.Application.Authentication;
using MatchDataManager.Infrastructure.Interfaces;
using MatchDataManager.Application.MiddleWare;
using MatchDataManager.DataBase.Models;
using MatchDataManager.DataBase.Models.Paination;
using MatchDataManager.DataBase.Seeder;
using MatchDataManager.Infrastructure.Services;
using MatchDataManager.Application.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using MatchDataManager.Infrastructure.Repositories;
using MatchDataManager.Infrastructure.Services;
using MatchDataManager.Application.Mappers;
using Microsoft.AspNetCore.Hosting;
using MatchDataManager.Infrastructure.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddEndpointsApiExplorer();

var authenticationSetting = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSetting);
builder.Services.AddSingleton(authenticationSetting);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSetting.JwtIssuer,
        ValidAudience = authenticationSetting.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSetting.JwtKey)),
    };
});

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("MyBoardsConnectionString")));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ITeamInterface, TeamServices>();
builder.Services.AddScoped<ILocationInterface, LocationsServices>();
builder.Services.AddScoped<IMatchInterface, MatchServices>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddAutoMapper(typeof(Program), typeof(LocationMappingProfile));
builder.Services.AddAutoMapper(typeof(Program), typeof(MatchMappingProfile));
builder.Services.AddAutoMapper(typeof(Program), typeof(TeamMappingProfile));
builder.Services.AddScoped<IValidator<Query>, QueryValidator>();
builder.Services.AddScoped<TeamSeeder>();

var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<TeamSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();