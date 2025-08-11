using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain.Repositories;

namespace OrderNow.Domain;
public interface IApplicationUnitOfWork:IUnitOfWork
{
    public ICustomerRepository CustomerRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public ICartRepository CartRepository { get; }

}
