using OrderNow.Domain.Entities;

namespace OrderNow.Web.Models.ViewModel;

public class CartItemVM
{
   public List<CartItem> CartItems { get; set; }
    public decimal TotalPrice { get; set; }
}
