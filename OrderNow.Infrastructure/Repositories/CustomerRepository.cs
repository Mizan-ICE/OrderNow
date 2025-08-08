using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Repositories;

namespace OrderNow.Infrastructure.Repositories;
public class CustomerRepository:Repository<Customer>, ICustomerRepository
{
    
    public CustomerRepository(OrderNowDbContext context):base(context)
    {
        
    }
}
