using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderNow.Domain.Entities;

namespace OrderNow.Infrastructure;
public class OrderNowDbContext:IdentityDbContext
{
    public OrderNowDbContext(DbContextOptions<OrderNowDbContext>options):base(options)
    {
        
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<PurchaseProduct> PurchaseProducts { get; set; }
    public DbSet<Delivery> Deliverys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItem>()
               .HasKey(oi => new { oi.OrderId, oi.ProductId });

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId);

        modelBuilder.Entity<PurchaseProduct>()
            .HasKey(ip => ip.Id);

        modelBuilder.Entity<PurchaseProduct>()
            .HasOne(ip => ip.Purchase)
            .WithMany(i => i.PurchaseProducts)
            .HasForeignKey(ip => ip.PurchaseId);

        modelBuilder.Entity<PurchaseProduct>()
            .HasOne(ip => ip.Product)
            .WithMany()
            .HasForeignKey(ip => ip.ProductId);

        modelBuilder.Entity<Delivery>()
  .HasKey(o => o.Id);

        modelBuilder.Entity<Delivery>()
            .HasOne(o => o.Order)
            .WithMany()
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.NoAction); // Modify this line

        modelBuilder.Entity<Delivery>()
            .HasOne(o => o.Product)
            .WithMany()
            .HasForeignKey(o => o.ProductId)
            .OnDelete(DeleteBehavior.NoAction); // Modify this line

        modelBuilder.Entity<Delivery>()
            .HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.NoAction); // Modify this line



        base.OnModelCreating(modelBuilder);
    }
}
