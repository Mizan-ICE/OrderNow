using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain.Entities;

namespace OrderNow.Domain.Services;
public interface ICartService
{
    void AddToCart(Product product);
    int RemoveFromCart(Product product);
    List<CartItem> GetCartItems();
    void ClearCart();
    decimal GetCartTotalPrice();
    List<CartItem> CartItems { get; set; }
}
