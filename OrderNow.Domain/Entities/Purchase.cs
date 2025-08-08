using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNow.Domain.Entities;
public class Purchase : IEntity
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Supplier")]
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    [Required]
    [Display(Name = "Date")]
    public DateTime Date { get; set; }

    public List<PurchaseProduct> PurchaseProducts { get; set; } = new List<PurchaseProduct>();
}
