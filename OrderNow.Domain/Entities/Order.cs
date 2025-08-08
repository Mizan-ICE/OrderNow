using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNow.Domain.Entities;
public class Order : IEntity
{
    public int Id { get; set; }
    public decimal OrderTotal { get; set; }
    public DateTime OrderPlaced { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
