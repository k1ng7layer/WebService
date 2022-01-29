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
            var user = _dbContext.Users.Where(u => u.Mail == item.Mail).FirstOrDefaultAsync();
            if(user==null)       
                await _dbContext.Users.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
                _dbContext.Users.Remove(user);
        }

        public async  Task<UserDTO> FindAsync(Expression<Func<UserDTO, bool>> predicate)
        {
            return await _dbContext.Users.Where(predicate).FirstOrDefaultAsync();
        }
            
        public async Task<UserDTO> FindById(int id)
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

        public IQueryable<UserDTO> GetAll()
        {
            return _dbContext.Users;
        }

        public async Task AddRangeAsync(IEnumerable<UserDTO> entities)
        {
            await _dbContext.Users.AddRangeAsync(entities);
        }

        public void Update(UserDTO item)
        {
            _dbContext.Users.Update(item);
        }

        public void DeleteRange(Expression<Func<UserDTO, bool>> predicate)
        {
            _dbContext.Users.RemoveRange(_dbContext.Users.Where(predicate));
        }

    }
            
}


            


        


      
      

       

        

       

