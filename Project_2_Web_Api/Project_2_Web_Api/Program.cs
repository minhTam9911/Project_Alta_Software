using Microsoft.EntityFrameworkCore;
using Project_2_Web_Api.Service;
using Project_2_Web_Api.Service.Impl;
using Project_2_Web_API.Models;
using System.ComponentModel;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectString = builder.Configuration["Connection:DefaultString"];
builder.Services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectString));
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<PositionGroupService, PositionGroupServiceImpl>();
builder.Services.AddScoped<PositionService, PositionServiceImpl>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors(builder => builder
				.AllowAnyHeader()
				.AllowAnyMethod()
				.SetIsOriginAllowed((host) => true)
				.AllowCredentials()
			);
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.Run();
