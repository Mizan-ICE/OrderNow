using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNow.Domain.Entities;
public class Product : IEntity
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductDescription { get; set; }
    public decimal Price { get; set; }
    public string? ProductImageUrl { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; } = default!;

    public List<OrderItem>? OrderItems { get; set; }
}
