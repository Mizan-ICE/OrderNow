using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Repositories;

namespace OrderNow.Infrastructure.Repositories;
public class OrderRepository : IOrderRepository
{
    private readonly OrderNowDbContext _context;

    public OrderRepository(OrderNowDbContext context)
    {
        _context = context;

    }
    public void CreateOrder(List<CartItem> items, string userId, string userEmailAddress)
    {
        var order = new Order()
        {
            UserId = userId,
            Email = userEmailAddress,
            OrderPlaced = DateTime.UtcNow,
            OrderItems = new List<OrderItem>()
        };
        _context.Orders.Add(order);

        foreach (var item in items)
        {
            var orderItem = new OrderItem()
            {
                ProductId = item.Product.Id,
                Quantity = item.Quantity,
              
                Order = order

            };
             _context.OrderItems.Add(orderItem);
        }
    }

    public List<Order> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
    {

        IQueryable<Order> query = _context.Orders
            .Include(n => n.OrderItems)
                .ThenInclude(n => n.Product)
            .Include(n => n.User)
            .AsNoTracking(); // Prevent EF from tracking objects unnecessarily

        if (userRole != "Admin")
        {
            query = query.Where(o => o.UserId == userId);
        }

        return  query.ToList();
    }

   

}
