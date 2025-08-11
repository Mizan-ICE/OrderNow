using Microsoft.AspNetCore.Mvc;
using OrderNow.Domain.Services;

namespace OrderNow.Web.Models.ViewComponents;

public class CartSummary : ViewComponent
{
    public ICartService _cartService;
    public CartSummary(ICartService cartService)
    {
        _cartService = cartService;

    }
    public IViewComponentResult Invoke()
    {
        var items = _cartService.GetCartItems();
        //var totalPrice = _cartService.GetCartTotalPrice();
        return View(items.Count);
    }
}
