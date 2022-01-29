using DataAccesLayer.Entities;
using DataAccesLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DataAccesLayer.Repostitories
{
    public class RoleRepository : IRepository<RoleDTO>
    {
        ApplicationContext _dbContext;
        public RoleRepository(ApplicationContext db)
        {
            _dbContext = db;
        }

        public async Task CreateAsync(RoleDTO item)
        {
            var result = await _dbContext.Roles.Where(r => r.Name == item.Name).FirstOrDefaultAsync();
            if(result==null)
                await _dbContext.Roles.AddAsync(item);
        }
         
        public async Task DeleteAsync(int id)
        {
            var result = await _dbContext.Roles.FindAsync(id);
            if (result != null)
                _dbContext.Roles.Remove(result);
        }

        public async Task<RoleDTO> FindAsync(Expression<Func<RoleDTO, bool>> predicate)
        {
            return await _dbContext.Roles.Where(predicate).FirstOrDefaultAsync();
        }
            
        public async Task<RoleDTO> FindById(int id)
        {
            return await _dbContext.Roles.FindAsync(id);
        }

        public IQueryable<RoleDTO> GetAll()
        {
            return _dbContext.Roles;
        }

        public async Task AddRangeAsync(IEnumerable<RoleDTO> entities)
        {
            await _dbContext.Roles.AddRangeAsync(entities);
        }

        public void Update(RoleDTO item)
        {
            _dbContext.Roles.Update(item);
        }

        public void DeleteRange(Expression<Func<RoleDTO, bool>> predicate)
        {
            _dbContext.Roles.RemoveRange(_dbContext.Roles.Where(predicate));
        }
    }
}

      
