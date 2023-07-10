using FluentValidation;
using FluentValidation.AspNetCore;
using MatchDataManager.Api.Interfaces;
using MatchDataManager.Api.MiddleWare;
using MatchDataManager.Api.Models;
using MatchDataManager.Api.Models.Paination;
using MatchDataManager.Api.Repositories;
using MatchDataManager.Api.Seeder;
using MatchDataManager.Api.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("MyBoardsConnectionString")));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ITeamInterface, TeamRepository>();
builder.Services.AddScoped<ILocationInterface, LocationsServices>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IValidator<Query>,QueryValidator>();
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