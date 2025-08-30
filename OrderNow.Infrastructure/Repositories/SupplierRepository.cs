using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Repositories;

namespace OrderNow.Infrastructure.Repositories;
public class SupplierRepository:Repository<Supplier>,ISupplierRepository
{
    private readonly OrderNowDbContext _context;
    public SupplierRepository(OrderNowDbContext context) : base(context)
    {
        _context = context;
    }
}
