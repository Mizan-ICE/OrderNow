using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain.Identity;

namespace OrderNow.Domain.Entities;
public class Order : IEntity
{
    public int Id { get; set; }
    public string Email { get; set; }
   
    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
    public decimal OrderTotal { get; set; }
    public DateTime OrderPlaced { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
