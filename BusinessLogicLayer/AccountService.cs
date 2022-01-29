using AutoMapper;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using DataAccesLayer.Entities;
using DataAccesLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer
{
    public class AccountService : IAccountService
    {
        IUnitOfWork DB { get; set; }
        IMapper Mapper { get; set; }
        public AccountService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            DB = unitOfWork;
            Mapper = mapper;
        }
        public async Task DeleteUserAsync(int userId)
        {
            await Task.Run(()=>DB.Users.DeleteAsync(userId));
        }

        public async Task<UserBL> FindUserAsync(Expression<Func<UserDTO, bool>> predicate)
        {
            //return Mapper.Map<UserBL>(await DB.Users.FindAsync(predicate));
            var user = await DB.Users.Include(u=>u.Role).FirstOrDefaultAsync(predicate);
            return Mapper.Map<UserBL>(user);
        }
       

        public IEnumerable<UserBL> GetAll()
        {
            return Mapper.Map<IEnumerable<UserBL>>(DB.Users.GetAll());
        }

        public async Task RegisterUserAsync(UserBL user)
        {
            UserDTO userDTO = new UserDTO()
            {
                Mail = user.Mail,
                Password = user.Password,
                RoleId = user.RoleId,
            };
            await DB.Users.CreateAsync(userDTO);
            await DB.SaveAsync();
        }
        public List<UserBL> Include(Expression<Func<UserDTO, RoleDTO>> expression)
        {
            var users = DB.Users.Include(expression);
            return Mapper.Map<List<UserBL>>(users);
        }

        public async Task<UserBL> Include(Expression<Func<UserDTO, RoleDTO>> expression, Expression<Func<UserDTO, bool>> comparer)
        {
            var user = await DB.Users.Include(expression).FirstOrDefaultAsync(comparer);
            return Mapper.Map<UserBL>(user);
        }

        public async Task<RoleBL> GetRoleAsync(Expression<Func<RoleDTO, bool>> predicate)
        {
            var asasas = Include(x => x.Role);
            return Mapper.Map<RoleBL>(await DB.Roles.FindAsync(predicate));
        }

        public async Task UpdateUser(UserBL user)
        {
            var res = await DB.Users.FindAsync(x=>x.Mail==user.Mail);
            if (res != null)
            {
                res.Name = user.Name;
                res.Password = user.Password;
                res.RoleId = user.RoleId;
            }
            DB.Users.Update(res);
            await DB.SaveAsync();
        }

      
    }
            
            
             
            
      
                
}
