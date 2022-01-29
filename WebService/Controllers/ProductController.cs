using AutoMapper;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("Products")]
    public class ProductController:Controller
    {
        IProductService ProductService { get; set; }
        IOrderService OrderService;
        IMapper Mapper { get; set; }
        public ProductController(IProductService productService,IMapper mapper,IOrderService orderService)
        {
            ProductService = productService;
            Mapper = mapper;
            OrderService = orderService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Remove")]
        public async Task<ActionResult> RemoveProduct(int id)
        {
            var product = await ProductService.FindProductAsync(id);
            if (product != null)
            {
                await OrderService.RemoveCartItemsAsync(product.Id);
                await ProductService.DeleteProductAsync(id);
                
            }
            return Ok("product removed");
        }

     
        [HttpGet]
        [Route("Find")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var prod = Mapper.Map<ProductModel>(await ProductService.FindProductAsync(id));
            return new ObjectResult(prod);
        }

        [Route("Add")]
        public async Task<ActionResult> AddNewProduct(ProductModel model)
        {
            ProductBL product = new ProductBL()
            {
                Price = model.Price,
                ProductName = model.ProductName,
                Category = model.Category,
                Count = model.Count,
            };
                

            await ProductService.AddProductAsync(product);
            return new ObjectResult($"product added {product}");
        }

        [HttpGet]
        [Route("All")]
        public  ActionResult ShowAllProducts()
        {
            var products = ProductService.GetAll();
            return new ObjectResult(products);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult>UpdateProduct(ProductModel product)
        {
            if (string.IsNullOrEmpty(product.ProductName))
            {
                ModelState.AddModelError("product name", "invalid prod");
            }

            if (ModelState.IsValid)
            {
                ProductBL productToUpdate = new ProductBL()
                {
                    Id = product.Id,
                    Price = product.Price,
                    ProductName = product.ProductName,
                    Category = product.Category,
                    Count = product.Count,
                    OnSale = product.OnSale,
                };
                await ProductService.UpdateProductAsync(productToUpdate);
                return Ok("product updated");
            }

            return new ObjectResult(product);
            
     
        }
    }
        




}
