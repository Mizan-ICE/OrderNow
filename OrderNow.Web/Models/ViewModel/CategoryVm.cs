using OrderNow.Domain.Entities;

namespace OrderNow.Web.Models.ViewModel;

public class CategoryVm
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? CategoryDescription { get; set; }
    public List<Product>? Products { get; set; }
}
