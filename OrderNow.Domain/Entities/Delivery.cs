using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain.Identity;

namespace OrderNow.Domain.Entities;
public class Delivery : IEntity
{
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }
    public Order? Order { get; set; } = default!;

    [Required]
    public int ProductId { get; set; }
    public Product? Product { get; set; } = default!;

    [Required]
    public int Quantity { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
}
