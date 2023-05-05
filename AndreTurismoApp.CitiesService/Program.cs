using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AndreTurismoApp.CitiesService.Data;
using AndreTurismoApp.Services.Producers;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AndreTurismoAppCitiesServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AndreTurismoAppCitiesServiceContext") ?? throw new InvalidOperationException("Connection string 'AndreTurismoAppCitiesServiceContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<ProducerCitiesService>();
builder.Services.AddSingleton<ConnectionFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
