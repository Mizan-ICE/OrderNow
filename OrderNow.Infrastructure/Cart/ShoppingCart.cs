using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrderNow.Domain.Entities;
using OrderNow.Infrastructure.Repositories;
using OrderNow.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace OrderNow.Application.Cart;
public class ShoppingCart
{
    private readonly OrderNowDbContext _context;

    public List<CartItem> CartItems { get; set; } = default!;

    public string? CartId { get; set; }
    public ShoppingCart(OrderNowDbContext context)
    {
        _context = context;
    }
    public static ShoppingCart GetCart(IServiceProvider services)
    {
        ISession? session = services.GetRequiredService<IHttpContextAccessor>
            ()?.HttpContext?.Session;

        OrderNowDbContext context = services.GetRequiredService<OrderNowDbContext>();

        string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

        session?.SetString("CartId", cartId);

        return new ShoppingCart(context) { CartId = cartId };
    }
    public void AddToCart(Product product)
    {
        var cartItem =
            _context.CartItems.FirstOrDefault(
                s => s.Product.Id == product.Id && s.CartId == CartId);

        if (cartItem == null)
        {

            cartItem = new CartItem
            {
                CartId = CartId,
                Product = product,
                Quantity = 1
            };
            _context.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity++;
        }

    }

    public void ClearCart()
    {
        var cartItems = _context
            .CartItems
            .Where(cart => cart.CartId == CartId);

        _context.CartItems.RemoveRange(cartItems);

    }

    public List<CartItem> GetCartItems()
    {
        return CartItems ??= _context.CartItems.Where(c =>
        c.CartId == CartId).
        Include(s => s.Product).
        ToList();
    }

    public decimal GetCartTotalPrice()
    {
        var total = _context.CartItems.Where(c =>
        c.CartId == CartId).
        Select(c => c.Product.Price * c.Quantity).Sum();

        return total;
    }

    public int RemoveFromCart(Product product)
    {
        var CartItem =
             _context.CartItems.FirstOrDefault(
                 s => s.Product.Id == product.Id && s.CartId == CartId);
        var localAmount = 0;
        if (CartItem != null)
        {
            if (CartItem.Quantity > 1)
            {
                CartItem.Quantity--;
                localAmount = CartItem.Quantity;
            }
            else
            {
                _context.CartItems.Remove(CartItem);

            }
        }

        return localAmount;
    }
}
