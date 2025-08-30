using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Identity;


namespace OrderNow.Infrastructure.Seed;
public class ApplicationSeed
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<OrderNowDbContext>();

            context.Database.EnsureCreated();

            // 1. Seed categories if none exist
            if (!context.Categories.Any())
            {
                var electronicsCategory = new Category
                {
                    CategoryName = "Electronics",
                    CategoryDescription = "Electronics Fields"
                };
                context.Categories.Add(electronicsCategory);
                context.SaveChanges();

                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
             {

              new Product()

                {

                    ProductName = "HP",
                    ProductDescription = "Model: HP 15-dy5131wm \r\nProcessor: 12th Gen Intel Core i3-1215U,Upto 4.4GHz \r\nRam: 8 GB DDR4-3200 MHz RAM (2 x 4 GB)\r\nStorage: 256 GB PCIe® NVMe™ M.2 SSD \r\nGraphic: Integrated Intel® UHD Graphics\r\nDisplay: 15.6\" diagonal,FHD,micro-edge,anti-glare,250 nits,45% NTSC 4 ",
                    Price = 60000,
                    ProductImageUrl = "https://www.applegadgetsbd.com/_next/image?url=https%3A%2F%2Fadminapi.applegadgetsbd.com%2Fstorage%2Fmedia%2Flarge%2FHP-15-dy5131wm-a-4927.jpg&w=1920&q=100",
                    CategoryId = electronicsCategory.Id


                },
               new Product()

                {

                    ProductName = "Acer Aspire 15 A515",
                    ProductDescription = "Intel® Core™ i5-13420H processor\r\n8GB DDR4 3200 Mhz Ram\r\n512GB PCIe NVMe Gen4 SSD\r\nNVIDIA GeForce RTX 2050 with 4GB of dedicated GDDR6 VRAM\r\n15.6″ IPS technology, FHD 1920 x 1080, Acer ComfyView™ LED-backlit TFT LCD, 16:9 aspect ratio 144Hz Display\r\nWindows 11 Home ",
                    Price = 90000,
                    ProductImageUrl = "https://bytecityb.b-cdn.net/wp-content/uploads/2024/12/Acer-ASPIRE-12-1.jpg",
                     CategoryId = electronicsCategory.Id


                },
                new Product()

                {

                    ProductName = "Galaxy S24 Ultra 5G",
                    ProductDescription = "The Samsung Galaxy S24 Ultra redefines the flagship game. " +
                    "It is embodying the epitome of smartphone greatness. Comprising stunning boxy shape," +
                    " crafted from titanium, exudes sophistication and durability. What makes it truly epic? The AI steals the spotlight," +
                    " empowering this device with unparalleled capabilities. The camera, a true virtuoso, captures unbelievable pictures," +
                    " while AI editing takes creativity to new heights. Fueling this powerhouse is a battery that outlasts like 48 hours long." +
                    " In the realm of smartphones, the Galaxy S24 Ultra reigns supreme, " +
                    "an unrivaled titan that effortlessly claims the title of the best flagship in every aspect. ",
                    Price = 96000,
                    ProductImageUrl = "https://www.applegadgetsbd.com/_next/image?url=https%3A%2F%2Fadminapi.applegadgetsbd.com%2Fstorage%2Fmedia%2Flarge%2FGalaxy-S24-Ultra-Titanium-Black-1587.jpg&w=1920&q=100",
                     CategoryId = electronicsCategory.Id


                },
                 new Product()
                 {

                    ProductName = "Galaxy S25 Ultra 5G",
                    ProductDescription = "Display: 6.8 inches Dynamic LTPO AMOLED 2X\r\nProcessor: Qualcomm SM8750-AB Snapdragon 8 Elite\r\nCamera: 200.0 MP + 10.0 MP + 50.0 MP + 12.0 MP\r\nFeatures: Accelerometer, Gyro, Proximity, Compass, Samsung DeX\r\nPromo offer already applied",
                    Price = 199999,
                    ProductImageUrl = "https://assets.gadgetandgear.com/upload/media/samsung-galaxy-s25-ultra-titanium-silver632.jpeg",
                   CategoryId = electronicsCategory.Id


                }

             });
                    context.SaveChanges();

                }
            }

        }
    }

    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {

            //Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //Users
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string adminUserEmail = "admin@gmail.com";

            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null)
            {
                var newAdminUser = new ApplicationUser()
                {
                    FullName = "Admin User",
                    UserName = "admin-user",
                    Email = adminUserEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newAdminUser, "Ordernow@123");
                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
            }


            string appUserEmail = "user@gmail.com";

            var appUser = await userManager.FindByEmailAsync(appUserEmail);
            if (appUser == null)
            {
                var newAppUser = new ApplicationUser()
                {
                    FullName = "Application User",
                    UserName = "app-user",
                    Email = appUserEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newAppUser, "Ordernow@123");
                await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
            }
        }
    }
}

