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
    public class OrderRepository : IRepository<OrderDTO>
    {
        private ApplicationContext _dbContext;
        public OrderRepository(ApplicationContext db)
        {
            _dbContext = db;
        }
        public async Task CreateAsync(OrderDTO item)
        {
            var result = await _dbContext.Orders.Where(o => o.OrderUniqueId == item.OrderUniqueId).FirstOrDefaultAsync();
            //var result = _dbContext.Orders.FindAsync(item.);
            if (result==null)
                await _dbContext.Orders.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _dbContext.Orders.Where(u=>u.Id==id).FirstOrDefaultAsync();
            _dbContext.Orders.Remove(result);
        }

        public async Task<OrderDTO> FindAsync(Expression<Func<OrderDTO, bool>> predicate)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(predicate);
            
            return order;
        }

        public async Task<OrderDTO> GetAsync(int id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(p => p.Id == id);
            return order;
        }

       
        public async Task UpdateAsync(OrderDTO item)
        {
            var result = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderUniqueId == item.OrderUniqueId);
            if (result != null)
                result.Date = item.Date;
        }

        public IQueryable<OrderDTO> GetAll()
        {
            return _dbContext.Orders;
        }

        public void DeleteAllByUserId(int userId)
        {
            
        }

        public async Task AddRangeAsync(IEnumerable<OrderDTO> entities)
        {
           await _dbContext.Orders.AddRangeAsync(entities);
        }
    }
       

}
