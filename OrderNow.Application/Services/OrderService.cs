using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Services;

namespace OrderNow.Application.Services;
public class OrderService : IOrderService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    public OrderService(IApplicationUnitOfWork applicationUnitOfWork)
    {
        _applicationUnitOfWork = applicationUnitOfWork;
    }
    public void CreateOrder(List<CartItem> items, string userId, string userEmailAddress)
    {
       _applicationUnitOfWork.OrderRepository.CreateOrder(items, userId, userEmailAddress);
        _applicationUnitOfWork.Save();
    }

    public List<Order> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
    {
       _applicationUnitOfWork.OrderRepository.GetOrdersByUserIdAndRoleAsync(userId, userRole);
        return _applicationUnitOfWork.OrderRepository.GetOrdersByUserIdAndRoleAsync(userId, userRole);
    }
}
