using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain.Entities;

namespace OrderNow.Domain.Services;
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProduct();
    Task<IEnumerable<Product>> GetAllProduct(params Expression<Func<Product, object>>[] includeProperties);
    Task<Product> GetProductByIdAsync(int id);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
}
