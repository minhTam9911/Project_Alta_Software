using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_2_Web_Api.Service;
using Project_2_Web_Api.Service.Impl;
using Project_2_Web_API.Models;
using System.ComponentModel;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectString = builder.Configuration["Connection:DefaultString"];
builder.Services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectString));
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<PositionGroupService, PositionGroupServiceImpl>();
builder.Services.AddScoped<PositionService, PositionServiceImpl>();
builder.Services.AddScoped<GrantPermissionService,GrantPermissionServiceImpl>();
builder.Services.AddScoped<AreaService,AreaServiceImpl>();
builder.Services.AddScoped<StaffUserService, StaffUserServiceImpl>();
builder.Services.AddScoped<UserServiceAccessor, UserServiceAccessorImpl>();
builder.Services.AddScoped<UserService, UserServiceImpl>();

/*builder.Services.AddAuthentication().AddJwtBearer(option =>
{
	option.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		ValidateAudience = false,
		ValidateIssuer = false,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
			builder.Configuration.GetSection("AppSettings:Token").Value!)),
		ClockSkew = TimeSpan.Zero
	};
});*/
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
/*app.UseAuthentication();*/
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.Run();
