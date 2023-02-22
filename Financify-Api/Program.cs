using Financify_Api.Data;
using Financify_Api.Repositories.Interfaces;
using Financify_Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FinancifyContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

builder.Services.AddScoped<IChargeRepository, ChargeRepository>();
builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();

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