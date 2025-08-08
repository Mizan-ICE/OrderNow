using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNow.Domain.Entities;
public class PurchaseProduct : IEntity
{
    public int Id { get; set; }

    [Required]
    public int PurchaseId { get; set; }
    public Purchase Purchase { get; set; }

    [Required]
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    [Required]
    [Display(Name = "Quantity")]
    public int Quantity { get; set; }
}
