using OrderNow.Domain.Entities;

namespace OrderNow.Web.Models.ViewModel;

public class CustomerVM
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string? CustomerPhoto { get; set; }
    public List<Order>? Orders { get; set; }
}
