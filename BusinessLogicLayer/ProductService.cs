using AutoMapper;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using DataAccesLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccesLayer.Entities;
using System.Web.Mvc;

namespace BusinessLogicLayer
{
    public class ProductService : IProductService
    {
        IUnitOfWork Db { get; set; }
        IMapper Mapper { get; set; }
        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            Db = unitOfWork;
            Mapper = mapper;
        }

        public async Task AddProductAsync(ProductBL product)
        {
            if (product != null)
            {
                var productDTO = Mapper.Map<Product>(product);
                await Db.Product.CreateAsync(productDTO);
                await Db.SaveAsync();
            }
        }
            
        public async Task DeleteProductAsync(int id)
        {
            await Db.Product.DeleteAsync(id);
            await Db.SaveAsync();
        }

        public async Task<ProductBL> FindProductAsync(int id)
        {
            var product = Mapper.Map<ProductBL>(await Db.Product.FindById(id));
            return product;
        }

        public IEnumerable<ProductBL> GetAll()
        {
            return Mapper.Map <IEnumerable<ProductBL>> ( Db.Product.GetAll());
        }

        public async Task UpdateProductAsync(ProductBL product)
        {
           
            var productDto = await Db.Product.FindById(product.Id);
            if (productDto != null)
            {
                productDto.OnSale = product.OnSale;
                productDto.Category = product.Category;
                productDto.Price = product.Price;
                productDto.ProductName = product.ProductName;
                productDto.Count = product.Count;
                Db.Product.Update(productDto);
                await Db.SaveAsync();
            }
        }



    }
}

        

       
