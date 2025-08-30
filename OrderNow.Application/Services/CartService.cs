using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Services;

namespace OrderNow.Application.Services;
public class CartService : ICartService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    public CartService(IApplicationUnitOfWork applicationUnitOfWork)
    {
      _applicationUnitOfWork = applicationUnitOfWork;  
    }
    public List<CartItem> CartItems { get; set; } = default!; 

    public void AddToCart(Product product)
    {
       _applicationUnitOfWork.CartRepository.AddToCart(product);
        _applicationUnitOfWork.Save();
    }

    public void ClearCart()
    {
        _applicationUnitOfWork.CartRepository.ClearCart();
        _applicationUnitOfWork.Save();
    }

    public List<CartItem> GetCartItems()
    {
      return _applicationUnitOfWork.CartRepository.GetCartItems();
    }

    public decimal GetCartTotalPrice()
    {
       return _applicationUnitOfWork.CartRepository.GetCartTotalPrice();
    }

    public int RemoveFromCart(Product product)
    {
      var quantityLeft= _applicationUnitOfWork.CartRepository.RemoveFromCart(product);
        _applicationUnitOfWork.Save();
        return quantityLeft;
        
    }
}
