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
            if(!_dbContext.Products.Contains(item))
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
            


        public async Task<Product> GetAsync(int id)
        {
            var product = await _dbContext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            return product;
        }

       

       

        public async Task UpdateAsync(Product item)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == item.Id);
            Product newProduct = new Product()
            {
                ProductName = item.ProductName,
                Category = item.Category,
                Price = item.Price,
                OnSale = item.OnSale,
                Count = item.Count,
            };
            await DeleteAsync(product.Id);
            await CreateAsync(newProduct);
        }

        public IQueryable<Product> GetAll()
        {
            return _dbContext.Products;
        }

        public void DeleteAllByUserId(int userId)
        {
            
        }

        public async Task AddRangeAsync(IEnumerable<Product> entities)
        {
            await _dbContext.Products.AddRangeAsync(entities);
        }
    }
}
