using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderNow.Application.Services;
using OrderNow.Domain;
using OrderNow.Domain.Repositories;
using OrderNow.Domain.Services;
using OrderNow.Infrastructure;
using OrderNow.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Serilog.Events;
using OrderNow.Infrastructure.Seed;
using OrderNow.Domain.Identity;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateBootstrapLogger();

try { 
    Log.Information("Starting OrderNow.Web application...");
    var builder = WebApplication.CreateBuilder(args);

var migrationAssembly = Assembly.GetExecutingAssembly();
#region Serilog
builder.Host.UseSerilog((context, lc) => lc
.MinimumLevel.Debug()
.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
.Enrich.FromLogContext()
.ReadFrom.Configuration(builder.Configuration));
#endregion

#region AutoMappe
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    #endregion


 //#region Identity
 //builder.Services.AddIdentity();
 //   #endregion

    // Add services to the container.
    builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService,CartService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();


builder.Services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDbContext<OrderNowDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OrderNowConn"), (x) => x.MigrationsAssembly(migrationAssembly)));
    builder.Services.AddRazorPages();

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<OrderNowDbContext>().AddDefaultTokenProviders(); ;
   builder.Services.AddMemoryCache();
   builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    });



    builder.Services.AddScoped<ICartRepository, CartRepository>(sp => CartRepository.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

    app.UseAuthentication();

    app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=ProductList}/{id?}")
    .WithStaticAssets();

ApplicationSeed.Seed(app);
ApplicationSeed.SeedUsersAndRolesAsync(app).Wait();
    app.MapRazorPages()
   .WithStaticAssets();



    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
