using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Services;

namespace OrderNow.Application.Services;
public class ProductService:IProductService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    public ProductService(IApplicationUnitOfWork applicationUnitOfWork)
    {
        _applicationUnitOfWork = applicationUnitOfWork;
    }

    public async Task AddProductAsync(Product product )
    {
      await _applicationUnitOfWork.ProductRepository.AddAsync( product );
       await _applicationUnitOfWork.SaveAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        await _applicationUnitOfWork.ProductRepository.DeleteAsync( id );   
        await _applicationUnitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProduct()
    {
      return await  _applicationUnitOfWork.ProductRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProduct(params Expression<Func<Product, object>>[] includeProperties)
    {
        return await _applicationUnitOfWork.ProductRepository.GetAllAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
      return await _applicationUnitOfWork.ProductRepository.GetByIdAsync( id );
    }

    public async Task UpdateProductAsync(Product product)
    {
      await _applicationUnitOfWork.ProductRepository.UpdateAsync( product );
      await  _applicationUnitOfWork.SaveAsync();
    }
}
