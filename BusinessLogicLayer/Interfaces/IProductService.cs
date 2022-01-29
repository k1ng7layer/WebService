using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProductService
    {
        Task<ProductBL> FindProductAsync(int id);
        Task AddProductAsync(ProductBL product);
        Task DeleteProductAsync(int id);
        IEnumerable<ProductBL> GetAll();
        Task UpdateProductAsync(ProductBL product);
    }
}
