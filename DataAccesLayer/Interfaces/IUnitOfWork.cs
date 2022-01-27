using DataAccesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository Users { get;}
        IRepository<OrderDTO> Orders { get; }
        IRepository<Product> Product { get;}
        ICartRepository Carts { get;}
        IRepository<OrderDetailDTO> OrderDetails { get; }
        IRepository<RoleDTO> Roles { get; }
        void Save();
        Task SaveAsync();
    }
}
