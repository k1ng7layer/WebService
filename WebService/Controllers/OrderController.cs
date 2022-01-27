using AutoMapper;
using BusinessLogicLayer;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Helpers;
using WebService.Models;

namespace WebService.Controllers
{
    //[ApiController]
    [Route("Order")]
    public class OrderController:Controller
    {
        IOrderService OrderService { get; set; }
        IProductService ProductService { get; set; }
        IAccountService AccountService { get; set; }
        IMapper Mapper { get; set; }
        public OrderController(IOrderService orderService, IProductService productService, IMapper mapper, IAccountService account)
        {
            OrderService = orderService;
            ProductService = productService;
            Mapper = mapper;
            AccountService = account;
        }
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        [Route("AddToCart")]
        public async Task<ActionResult<CartModel>> AddToCart(int id, int? quantity)
        {
            if (User.Identity.IsAuthenticated)
            {
                CartBL cart = new CartBL();
                var product = await ProductService.FindProductAsync(id);
                var requestedQuantity = quantity ?? 1;
                var user = await AccountService.FindUserAsync(u => u.Mail == User.Identity.Name);
                if (product.Count != 0)
                {
                    if (product.Count <= requestedQuantity)
                        cart.Quantity = product.Count;
                    else cart.Quantity = requestedQuantity;
                }
                cart.ProductId = product.Id;
                cart.ProductName = product.ProductName;
                
                cart.UserId = user.Id;
                //cart.ProductName = product.ProductName;
                await OrderService.AddToUserCart(cart);
                
            }
            else
            {


                CartModel cart = new CartModel();

                var product = await ProductService.FindProductAsync(id);
                var requestedQuantity = quantity ?? 1;
                if (product.Count != 0)
                {
                    if (product.Count <= requestedQuantity)
                        cart.Quantity = product.Count;
                    else cart.Quantity = requestedQuantity;
                }
                cart.ProductId = product.Id;
                cart.ProductName = product.ProductName;

                if (SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "cart") == null)
                {
                    List<CartModel> carts = new List<CartModel>();
                    carts.Add(cart);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", carts);
                }
                else
                {
                    List<CartModel> carts = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "cart");
                    carts.Add(cart);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", carts);
                }
            }
            return Json(new { Succes = true });
        }

        [NonAction]
        public async Task CartTransfer()
        {
            if(SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "cart") != null)
            {
                var carts = Mapper.Map<List<CartBL>>(SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "cart"));
                
                foreach (var item in carts)
                {
                    await OrderService.AddToUserCart(item);
                }
                
            }
        }
       

        [HttpGet]
        //[Authorize(Roles = "user")]
        [Route("ShowCart")]
        public async Task<ActionResult<List<CartModel>>> ShowCart()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await AccountService.FindUserAsync(u => u.Mail == User.Identity.Name);
                

                if (SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "cart") != null)
                {
                    var cashedCarts = Mapper.Map<List<CartBL>>(SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "cart"));
                    foreach (var item in cashedCarts)
                    {
                        item.UserId = user.Id;
                    }
                    await OrderService.AddRangeCartAsync(cashedCarts);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);
                }
                var carts = Mapper.Map<List<CartModel>>(await OrderService.FindUserCart(user.Id));
                return new JsonResult(carts);
            }
            else
            {
                if (SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "cart") != null)
                {
                    List<CartModel> carts = new List<CartModel>();
                    carts = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "cart");
                    return new ObjectResult(carts);
                }
                else
                {
                    return BadRequest();
                }
            }
        }






                    



        
        [HttpPost]
        [Route("AddOrder")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> AddOrderAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await AccountService.FindUserAsync(u => u.Mail == HttpContext.User.Identity.Name);
                Random random = new Random();
                OrderModel order = new OrderModel
                {
                    Date = DateTime.Now,
                    OrderUniqueId = $"{random.Next(0, 10000)}",
                    UserId = user.Id,
                };


                await OrderService.CreateOrderAsync(Mapper.Map<OrderBL>(order));
                var cashedCart = new List<CartModel>();
               
                if (SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "cart") != null)
                {
                    cashedCart = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "cart");
                    foreach (var item in cashedCart)
                    {
                        item.UserId = user.Id;
                    }
                }
                
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);
                await OrderService.AddRangeCartAsync(Mapper.Map<List<CartBL>>(cashedCart));
                var userCart = Mapper.Map<List<CartModel>>(await OrderService.FindUserCart(user.Id));
                if (userCart.Count != 0)
                {
                    foreach (var cart in userCart)
                    {
                        OrderDetail detail = new OrderDetail
                        {
                            ProductId = cart.ProductId,
                            Quantity = cart.Quantity,
                            Price = Mapper.Map<ProductModel>(await ProductService.FindProductAsync(cart.ProductId)).Price,
                            OrderId = order.OrderUniqueId,
                        };
                        await OrderService.CreateOrderDetailAsync(Mapper.Map<OrderDetailBL>(detail));
                        await OrderService.DeleteUserCart(user.Id);
                    }
                    return Ok($"OrderCreated for user  {User.Identity.Name}");
                }
                else
                {
                    return new OkObjectResult("Cart is empty! Cannot creaate order with empty cart...");
                }

            }
            else
            {
                return Unauthorized("Please Login to make order...");
            }
                
        }

                   

                        
                       
                    
                    
                    
                
               


        [HttpGet]
        [Route("Find")]
        public JsonResult GetOrder(int id)
        {
            var order = Mapper.Map<OrderModel>(OrderService.FindOrderByIdAsync(id));
            if (order != null)
                return Json(order);
            else return Json(new { Message = "Couldn't find order" });
        }

        
    }
}
               

