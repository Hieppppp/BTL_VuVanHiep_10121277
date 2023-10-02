using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<LINHKIENContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("connect")));
builder.Services.AddDbContext<LINHKIENContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connect")));

//Khai báo ?? ch?y ??i t??ng
builder.Services.AddScoped<IKhachHangBL,KhachHangBL>();
builder.Services.AddScoped<IKhachHangDA,KhachHangDA>();

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
