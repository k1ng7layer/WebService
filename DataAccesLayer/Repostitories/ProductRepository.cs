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
    public class ProductRepository : IRepository<Product>
    {
        private ApplicationContext _dbContext;
        public ProductRepository(ApplicationContext db)
        {
            _dbContext = db;
        }
        public async Task CreateAsync(Product item)
        {
            var result = await _dbContext.Products.Where(p => p.ProductName == item.ProductName).FirstOrDefaultAsync();
            if(result==null)
                await _dbContext.Products.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _dbContext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (product!=null)
                _dbContext.Products.Remove(product);
        }

        public async Task<Product> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            var product = await _dbContext.Products.Where(predicate).FirstOrDefaultAsync();
            return product;
        }

        public async Task<Product> FindById(int id)
        {
            var product = await _dbContext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            return product;
        }

        public IQueryable<Product> GetAll()
        {
            return _dbContext.Products;
        }
        public async Task AddRangeAsync(IEnumerable<Product> entities)
        {
            await _dbContext.Products.AddRangeAsync(entities);
        }

        public void Update(Product item)
        {
            _dbContext.Products.Update(item);
        }

        public void DeleteRange(Expression<Func<Product, bool>> predicate)
        {
            _dbContext.Products.RemoveRange(_dbContext.Products.Where(predicate));
        }
    }
            
}


       

       

  


      

