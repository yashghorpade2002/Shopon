using Shopon.ADO;
using Shopon.Business.Contracts;
using Shopon.Business;
using Shopon.Data.Contracts;
using Shopon.EF;
using Microsoft.EntityFrameworkCore;
using Shopon.EF.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Services
//Product Service

builder.Services.AddScoped<IProductRepository, ProductADORepository>();
//builder.Services.AddScoped<IProductRepository, ProductEFRepository>();
builder.Services.AddScoped<IProductManager, ProductManager>();

//Product Async Service
builder.Services.AddScoped<IProductAsyncRepository, ProductAsyncEFRepository>();
builder.Services.AddScoped<IProductAsyncManager, ProductAsyncManager>();


// Company async Service
builder.Services.AddScoped<ICompanyAsyncRepository, CompanyAsyncEFRepository>();
builder.Services.AddScoped<ICompanyAsyncManager, CompanyAsyncManager>();

// config EF
builder.Services.AddDbContext<DbShoponContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//Config Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DbShoponContext>();

//Config Swagger
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// Enable serving static files
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopon API v1.0");
    options.RoutePrefix = string.Empty;
});

// Register CORS policy
app.UseCors(options =>
{
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}
);


// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();