using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AndreTurismoApp.CustomersService.Data;
using AndreTurismoApp.AddressesService.Data;
using AndreTurismoApp.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AndreTurismoAppCustomersServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AndreTurismoAppCustomersServiceContext") ?? throw new InvalidOperationException("Connection string 'AndreTurismoAppCustomersServiceContext' not found.")));

builder.Services.AddDbContext<AndreTurismoAppAddressesServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AndreTurismoAppAddressesServiceContext") ?? throw new InvalidOperationException("Connection string 'AndreTurismoAppCustomersServiceContext' not found.")));

builder.Services.AddSingleton<AddressService>();
builder.Services.AddSingleton<CustomerService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
