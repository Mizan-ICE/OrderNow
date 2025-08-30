using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderNow.Application.Services;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Services;
using OrderNow.Web.Models.ViewModel;

namespace OrderNow.Web.Controllers;
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;
    public OrderController(IOrderService orderService
        ,ICartService cartService,
        IMapper mapper
        )
    {
        _orderService = orderService;
        
        _cartService = cartService;
        _mapper = mapper;
    }
    [Authorize(Roles ="Admin")]
    public IActionResult Index()
    {
       var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
         var userRole = User.FindFirstValue(ClaimTypes.Role);
        var orders = _orderService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
        return View(orders);
    }

    public  IActionResult CheckOut()
{
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var items = _cartService.GetCartItems();
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    var userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
     _orderService.CreateOrder(items, userId, userEmailAddress);
    _cartService.ClearCart();

    return RedirectToAction("OrderConfirmation");
}
    public IActionResult OrderConfirmation()
    {
        return View();
    }


}
