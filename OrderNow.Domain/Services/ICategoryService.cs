using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain.Entities;

namespace OrderNow.Domain.Services;
public interface ICategoryService
{
    Task<IEnumerable<Category>> AllCategoriesAsync();
    Task<IEnumerable<Category>> AllCategoriesAsync(params Expression<Func<Category, object>>[] includeProperties);
    Task AdAsync(Category category);
       
}
