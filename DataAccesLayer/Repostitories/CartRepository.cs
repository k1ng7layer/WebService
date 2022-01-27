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
    public class CartRepository : ICartRepository
    {
        private ApplicationContext _dbContext;
        public CartRepository(ApplicationContext db)
        {
            _dbContext = db;
        }

        public async Task AddRangeAsync(IEnumerable<Cart> entities)
        {
            await _dbContext.AddRangeAsync(entities);
        }

        public async Task CreateAsync(Cart item)
        {
            //var res = await _dbContext.Carts.FindAsync(item);
            //if (res == null)
                await _dbContext.AddAsync(item);
        }

        public void DeleteAllByUserId(int userId)
        {
            _dbContext.Carts.RemoveRange(_dbContext.Carts.Where(c => c.UserId == userId));
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _dbContext.Carts.FindAsync(id);
            if (res != null)
                _dbContext.Remove(id);
        }

        public async Task<Cart> FindAsync(Expression<Func<Cart, bool>> predicate)
        {
            var res = await _dbContext.Carts.FirstOrDefaultAsync(predicate);
            return res;
        }

        public IQueryable<Cart> GetAll()
        {
            return _dbContext.Carts;
        }

        public async Task<Cart> GetAsync(int id)
        {
            return await _dbContext.Carts.FindAsync(id);
        }

        public void RemoveRangeByProductId(int id)
        {
            _dbContext.Carts.RemoveRange(_dbContext.Carts.Where(c => c.ProductId == id));
        }

        public Task UpdateAsync(Cart item)
        {
            throw new NotImplementedException();
        }
    }
}
