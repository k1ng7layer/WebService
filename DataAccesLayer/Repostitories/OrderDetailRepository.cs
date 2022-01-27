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
            //var result = await _dbContext.OrderDetails.FirstOrDefaultAsync(o=>o.)
            //if (!_dbContext.OrderDetails.Contains(item))
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
            var orderDetail = await _dbContext.OrderDetails.FirstOrDefaultAsync(predicate);
            return orderDetail;
        }

        public async Task<OrderDetailDTO> GetAsync(int id)
        {
            var orderDetail = await _dbContext.OrderDetails.FindAsync(id);
            return orderDetail;
        }

        //public async Task<IEnumerable<OrderDetailDTO>> GetAll()
        //{
        //    var q = from item in _dbContext.OrderDetails
        //            select item;
        //    return await q.ToListAsync();
        //}
        public IQueryable<OrderDetailDTO> GetAll()
        {
            return _dbContext.OrderDetails;
        }



        public async Task UpdateAsync(OrderDetailDTO item)
        {
            var result = await _dbContext.OrderDetails.FirstOrDefaultAsync(o => o.OrderId == item.OrderId);
            


        }

        public void DeleteAllByUserId(int orderId)
        {
            //_dbContext.OrderDetails.RemoveRange(_dbContext.OrderDetails.Where(o => o.OrderId == orderId));
        }

        public async Task AddRangeAsync(IEnumerable<OrderDetailDTO> entities)
        {
            await _dbContext.OrderDetails.AddRangeAsync(entities);
        }
    }
}
