using Microsoft.EntityFrameworkCore;
using RPi_backend.Infrastructure;
using RPi_backend.Repository;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Inject database
builder.Services.AddDbContext<SensorDbContext>();

// Inject repository
builder.Services.AddScoped<ITemperatureRepository, TemperatureRepository>();
builder.Services.AddScoped<IHumidityRepository, HumidityRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseCors(cors => cors
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
