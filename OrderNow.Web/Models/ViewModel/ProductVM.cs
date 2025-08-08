using OrderNow.Domain.Entities;

namespace OrderNow.Web.Models.ViewModel;

public class ProductVM
{
    public int Id { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string? ProductDescription { get; set; }

    public decimal Price { get; set; }

    public string? ProductImageUrl { get; set; }

    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public Product Product { get; set; } = new Product();

    public IEnumerable<Category> Categories { get; set; } = new List<Category>();


}
