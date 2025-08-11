using AutoMapper;
using OrderNow.Domain.Entities;
using OrderNow.Web.Models.ViewModel;

namespace OrderNow.Web.WebProfile;

public class OrderProfile:Profile
{
    public OrderProfile()
    {
        CreateMap<Customer,CustomerVM>().ReverseMap();
        CreateMap<Category,CategoryVm>().ReverseMap();
        CreateMap<Product,ProductVM>().ReverseMap();
        CreateMap<CartItem,CartItemVM>().ReverseMap();

    }
}
