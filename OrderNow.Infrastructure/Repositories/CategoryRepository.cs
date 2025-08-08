using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Domain.Entities;
using OrderNow.Domain.Repositories;

namespace OrderNow.Infrastructure.Repositories;
public class CategoryRepository:Repository<Category>,ICategoryRepository
{
    private readonly OrderNowDbContext _dbContext;
    public CategoryRepository(OrderNowDbContext context):base(context)
    {
       _dbContext = context;     
    }
}
