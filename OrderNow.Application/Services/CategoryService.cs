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
public class CategoryService : ICategoryService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    public CategoryService(IApplicationUnitOfWork applicationUnitOfWork)
    {
      _applicationUnitOfWork = applicationUnitOfWork;  
    }
    public async Task AdAsync(Category category)
    {
      await  _applicationUnitOfWork.CategoryRepository.AddAsync(category);
       await _applicationUnitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<Category>> AllCategoriesAsync()
    {
     return await _applicationUnitOfWork.CategoryRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Category>> AllCategoriesAsync(params Expression<Func<Category, object>>[] includeProperties)
    {
     return  await _applicationUnitOfWork.CategoryRepository.GetAllAsync();
    }
}
