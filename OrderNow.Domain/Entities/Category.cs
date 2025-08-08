using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNow.Domain.Entities;
public class Category:IEntity
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter category name")]
    public string CategoryName { get; set; } = string.Empty;
    public string? CategoryDescription { get; set; }
    public List<Product>? Products { get; set; }

}
