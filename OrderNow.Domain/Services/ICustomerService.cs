using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain.Entities;

namespace OrderNow.Domain.Services;
public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<IEnumerable<Customer>> GetAllAsync(params Expression<Func<Customer, object>>[] includeProperties);
    Task<Customer> GetByIdAsync(int id);
    Task AddAsync(Customer entity);
    Task UpdateAsync( Customer entity);
    Task DeleteAsync(int id);
}
