using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNow.Domain;
public interface IUnitOfWork
{
    void Save();
    Task SaveAsync();
}
