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
    public class OrderDetailRepository : IRepository<OrderDetailDTO>
    {
        private ApplicationContext _dbContext;
        public OrderDetailRepository(ApplicationContext db)
        {
            _dbContext = db;
        }
        public async Task CreateAsync(OrderDetailDTO item)
        {
            await _dbContext.OrderDetails.AddAsync(item);
        }
        public async Task DeleteAsync(int id)
        {
            var orderDetail = await _dbContext.OrderDetails.FirstOrDefaultAsync(p => p.Id == id);
            if (orderDetail != null)
                 _dbContext.OrderDetails.Remove(orderDetail);
        }

        public async Task<OrderDetailDTO> FindAsync(Expression<Func<OrderDetailDTO, bool>> predicate)
        {
            return await _dbContext.OrderDetails.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<OrderDetailDTO> FindById(int id)
        {
            var orderDetail = await _dbContext.OrderDetails.FindAsync(id);
            return orderDetail;
        }

        public IQueryable<OrderDetailDTO> GetAll()
        {
            return _dbContext.OrderDetails;
        }
        public void Update(OrderDetailDTO item)
        {
            _dbContext.OrderDetails.Update(item);
        }
      
        public async Task AddRangeAsync(IEnumerable<OrderDetailDTO> entities)
        {
            await _dbContext.OrderDetails.AddRangeAsync(entities);
        }

        public void DeleteRange(Expression<Func<OrderDetailDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }
            

    }

}
       





            



