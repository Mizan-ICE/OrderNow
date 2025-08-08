using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderNow.Domain;

namespace OrderNow.Infrastructure;
public class UnitOfWork:IUnitOfWork
{
    private readonly DbContext _dbContext;
    public UnitOfWork(DbContext dbContext)
    {
       _dbContext = dbContext; 
    }

    public void Save()
    {
       _dbContext.SaveChanges();
    }

    public async Task SaveAsync()
    {
       await _dbContext.SaveChangesAsync();
    }
}
