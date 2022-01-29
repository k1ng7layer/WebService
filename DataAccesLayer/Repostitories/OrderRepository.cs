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
            var order = await _dbContext.Orders.Where(predicate).FirstOrDefaultAsync();
            return order;
        }

        public async Task<OrderDTO> FindById(int id)
        {
            var order = await _dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            return order;
        }

        public IQueryable<OrderDTO> GetAll()
        {
            return _dbContext.Orders;
        }

        public async Task AddRangeAsync(IEnumerable<OrderDTO> entities)
        {
           await _dbContext.Orders.AddRangeAsync(entities);
        }

        public void Update(OrderDTO item)
        {
            _dbContext.Orders.Update(item);
        }

        public void DeleteRange(Expression<Func<OrderDTO, bool>> predicate)
        {
            _dbContext.Orders.RemoveRange(_dbContext.Orders.Where(predicate));
        }
    }
}
            
       
       

       

        

