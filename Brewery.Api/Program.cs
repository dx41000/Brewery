using Brewery.Api.AutoMapper;
using Brewery.Datalayer.Context;
using Brewery.Repositories;
using Brewery.Repositories.Interfaces;
using Brewery.Services;
using Brewery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddDbContext<BreweryContext>(options =>
    options.UseMySql(configuration.GetConnectionString("BreweryContext"),
        ServerVersion.AutoDetect(configuration.GetConnectionString("BreweryContext"))));


builder.Services.AddTransient<IBeerService, BeerService>();
builder.Services.AddTransient<IBarService, BarService>();
builder.Services.AddTransient<IBreweryService, BreweryService>();

builder.Services.AddTransient<IBeerRepository, BeerRepository>();
builder.Services.AddTransient<IBarRepository, BarRepository>();
builder.Services.AddTransient<IBreweryRepository, BreweryRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();