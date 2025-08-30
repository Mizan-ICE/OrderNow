using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain.Entities;

namespace OrderNow.Domain.Services;
public interface IOrderService
{
    List<Order> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    void CreateOrder(List<CartItem> items, string userId, string userEmailAddress);
   
}
