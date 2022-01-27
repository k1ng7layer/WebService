using DataAccesLayer.Entities;
using DataAccesLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repostitories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationContext _dbContext;
        public UserRepository(ApplicationContext db)
        {
            _dbContext = db;
        }
        public async Task CreateAsync(UserDTO item)
        {
            
            //var res = await _dbContext.Roles.FirstOrDefaultAsync(x =>x.Id==2);
            //_dbContext.Entry(res).State = EntityState.Detached;
             await _dbContext.Users.AddAsync(item);
        }
            

        public async Task DeleteAsync(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
                _dbContext.Users.Remove(user);
        }
            

        public async  Task<UserDTO> FindAsync(Expression<Func<UserDTO, bool>> predicate)
        {
            
            return await _dbContext.Users.FirstOrDefaultAsync(predicate);
        }

        public async Task<UserDTO> GetAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        

        public IQueryable<UserDTO> Include(Expression<Func<UserDTO, RoleDTO>> expression)
        {
            return _dbContext.Users.Include(expression);
        }

        public async Task UpdateAsync(UserDTO item)
        {
            //var result = _dbContext.Users.FirstOrDefaultAsync(u => u.Mail == item.Mail);


            _dbContext.Users.Update(item);
        }

        public IQueryable<UserDTO> GetAll()
        {
            return _dbContext.Users;
        }

        public void DeleteAllByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task AddRangeAsync(IEnumerable<UserDTO> entities)
        {
            await _dbContext.Users.AddRangeAsync(entities);
        }
    }
}
      

       

        

       

