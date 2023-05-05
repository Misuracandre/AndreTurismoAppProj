using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AndreTurismoApp.AddressesService.Data;
using AndreTurismoApp.Services;
using AndreTurismoApp.Services.Producers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AndreTurismoAppAddressesServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AndreTurismoAppAddressesServiceContext") ?? throw new InvalidOperationException("Connection string 'AndreTurismoAppAddressesServiceContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<PostOfficesService>();
builder.Services.AddScoped<ProducerAddressesService>();

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
