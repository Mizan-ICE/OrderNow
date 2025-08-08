using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNow.Domain.Entities;
public class CartItem:IEntity
{
    public int Id { get; set; }
    public Product Product { get; set; } = default!;
    public int Quantity { get; set; }
    public string? CartId { get; set; }
}
