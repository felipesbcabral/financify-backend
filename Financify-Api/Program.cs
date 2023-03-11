using Financify_Api.Data;
using Financify_Api.Repositories.Interfaces;
using Financify_Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Financify_Api.Models.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ChargeProfile));

builder.Services.AddDbContext<FinancifyContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

builder.Services.AddScoped<IChargeRepository, ChargeRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

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