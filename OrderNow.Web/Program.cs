using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderNow.Application.Services;
using OrderNow.Domain;
using OrderNow.Domain.Repositories;
using OrderNow.Domain.Services;
using OrderNow.Infrastructure;
using OrderNow.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var migrationAssembly = Assembly.GetExecutingAssembly();
#region AutoMappe
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDbContext<OrderNowDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OrdernNowConn"), (x) => x.MigrationsAssembly(migrationAssembly)));
builder.Services.AddScoped<ICartItemRepository, CartRepository>(sp => CartRepository.GetCart(sp));
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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
