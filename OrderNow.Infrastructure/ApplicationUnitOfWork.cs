using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain;
using OrderNow.Domain.Repositories;

namespace OrderNow.Infrastructure;
public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
{
    private readonly OrderNowDbContext _context;
    public ApplicationUnitOfWork(OrderNowDbContext context,
      
        ICategoryRepository categoryRepository,
        IProductRepository productRepository,
        IOrderRepository oderRepository,
        ICartRepository cartRepository,
        ISupplierRepository supplierRepository
        ) : base(context)
    {
        _context = context;
      
        CategoryRepository = categoryRepository;
        ProductRepository = productRepository;
        OrderRepository = oderRepository;
        CartRepository = cartRepository;
        SupplierRepository = supplierRepository;
    }

  

    public ICategoryRepository CategoryRepository { get; private set; }

    public IProductRepository ProductRepository { get; private set; }

    public IOrderRepository OrderRepository { get; private set; }

    public ICartRepository CartRepository { get; private set; }

    public ISupplierRepository SupplierRepository { get; private set; }
}
