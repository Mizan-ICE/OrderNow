using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Services;
using OrderNow.Web.Models.ViewModel;

namespace OrderNow.Web.Controllers;
public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    public CartController(ICartService cartService,
        IProductService productService,
        IMapper mapper
        )
    {
        _cartService = cartService;
        _productService = productService;
        _mapper = mapper;
    }
    public IActionResult Index()
    {
       var items=_cartService.GetCartItems();
       var itemvm=_mapper.Map<List<CartItem>>(items);
        var viewItem = new CartItemVM
        {
            CartItems = itemvm,
            TotalPrice = _cartService.GetCartTotalPrice()
        };
        return View(viewItem);
    }
    [HttpGet]
    public async Task<IActionResult> AddToCart(int id)
    {
        var productVM = await _productService.GetProductByIdAsync(id);
        if (productVM == null) return NotFound();

        var productEntity = _mapper.Map<Product>(productVM);
        _cartService.AddToCart(productEntity);

        return RedirectToAction("Index");
    }
    public async Task< IActionResult> RemoveFromCart(int id)
    {
        var cart = await _productService.GetProductByIdAsync(id);
        if(cart !=null)
        {
            _cartService.RemoveFromCart(cart);
        }
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> AddItemToCart(int id)
    {
        var cart = await _productService.GetProductByIdAsync(id);
        if (cart != null)
        {
            _cartService.AddToCart(cart);
        }
        return RedirectToAction("Index");
    }
}
