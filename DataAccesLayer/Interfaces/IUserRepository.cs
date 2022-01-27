using DataAccesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Interfaces
{
    public interface IUserRepository:IRepository<UserDTO>
    {
        IQueryable<UserDTO>Include(Expression<Func<UserDTO, RoleDTO>> expression);
    }
}
