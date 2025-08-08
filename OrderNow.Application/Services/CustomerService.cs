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
public class CustomerService : ICustomerService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    public CustomerService(IApplicationUnitOfWork applicationUnitOfWork)
    {
     _applicationUnitOfWork = applicationUnitOfWork;   
    }
    public async Task AddAsync(Customer entity)
    {
      await _applicationUnitOfWork.CustomerRepository.AddAsync(entity);
       await _applicationUnitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
       await _applicationUnitOfWork.CustomerRepository.DeleteAsync(id);
       await _applicationUnitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
      return await _applicationUnitOfWork.CustomerRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(params Expression<Func<Customer, object>>[] includeProperties)
    {
    return  await _applicationUnitOfWork.CustomerRepository.GetAllAsync(includeProperties);
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
     return await _applicationUnitOfWork.CustomerRepository.GetByIdAsync(id);
    }

    public async Task UpdateAsync( Customer entity)
    {
       await _applicationUnitOfWork.CustomerRepository.UpdateAsync(entity);
        await _applicationUnitOfWork.SaveAsync();
    }
}
