using BusinessLogicLayer.Entities;
using DataAccesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Interfaces 
{
    public interface IAccountService
    {
        Task RegisterUserAsync(UserBL user);
        List<UserBL> Include(Expression<Func<UserDTO, RoleDTO>> expression);
        Task<UserBL> Include(Expression<Func<UserDTO, RoleDTO>> expression,Expression<Func<UserDTO,bool>> comparer);
        Task<UserBL> FindUserAsync(Expression<Func<UserDTO, bool>> predicate);
        Task UpdateUser(UserBL user);
        Task<RoleBL> GetRoleAsync(Expression<Func<RoleDTO, bool>> predicate);
        Task DeleteUserAsync(int userId);
        IEnumerable<UserBL> GetAll();
    }
}
       
       
        
