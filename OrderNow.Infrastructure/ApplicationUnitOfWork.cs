using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain;
using OrderNow.Domain.Repositories;

namespace OrderNow.Infrastructure;
public class ApplicationUnitOfWork: UnitOfWork,IApplicationUnitOfWork
{
    private readonly OrderNowDbContext _context;
    public ApplicationUnitOfWork(OrderNowDbContext context,
        ICustomerRepository customerRepository,
        ICategoryRepository categoryRepository,
        IProductRepository productRepository) :base(context)
    {
       _context = context; 
        CustomerRepository= customerRepository;
        CategoryRepository= categoryRepository;
        ProductRepository= productRepository;
    }

    public ICustomerRepository CustomerRepository { get; private set; }

    public ICategoryRepository CategoryRepository {  get; private set; }

    public IProductRepository ProductRepository {  get; private set; }
}
