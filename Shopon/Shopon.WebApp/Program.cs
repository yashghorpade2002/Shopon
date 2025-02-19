using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopon.ADO;
using Shopon.Business;
using Shopon.Business.Contracts;
using Shopon.Data.Contracts;
using Shopon.EF;
using Shopon.EF.Models;

var builder = WebApplication.CreateBuilder(args);

//services
//Product Services
builder.Services.AddScoped<IProductRepository, ProductADORepository>(); //DI-IOC
//builder.Services.AddScoped<IProductRepository, ProductEFRepository>(); //DI-IOC
builder.Services.AddScoped<IProductManager, ProductManager>();

//Order Services
builder.Services.AddScoped<IOrderRepository , OrderADORepository>();
builder.Services.AddScoped<IOrderManager, OrderManager>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//Config Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

// Config HttpContextAccessor
builder.Services.AddHttpContextAccessor();

//Config Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DbShoponContext>();



// config EF
builder.Services.AddDbContext<DbShoponContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
