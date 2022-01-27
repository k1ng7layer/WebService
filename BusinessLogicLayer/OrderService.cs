using AutoMapper;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using DataAccesLayer.Entities;
using DataAccesLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class OrderService:IOrderService
    {
        IUnitOfWork _dB;
        IMapper Mapper;
        public OrderService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _dB = unitOfWork;
            Mapper = mapper;
        }

        public async Task AddToUserCart(CartBL cart)
        {
            Cart cartDTO = new Cart()
            {
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
                UserId = cart.UserId,
                ProductName = cart.ProductName,
            };
            await _dB.Carts.CreateAsync(cartDTO);
           
            await _dB.SaveAsync();
        }

        public async Task CreateOrderAsync(OrderBL order)
        {
            OrderDTO newOrder = new OrderDTO()
            {
                UserId = order.UserId,
                Date = order.Date,
                OrderUniqueId = order.OrderUniqueId,
            };
            await _dB.Orders.CreateAsync(newOrder);
            await _dB.SaveAsync();
        }

        public async Task CreateOrderDetailAsync(OrderDetailBL orderDetail)
        {
            OrderDetailDTO orderDetailDTO = new OrderDetailDTO()
            {
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity,
                Price = orderDetail.Price,
                OrderId = orderDetail.OrderId,
            };
            await _dB.OrderDetails.CreateAsync(orderDetailDTO);
            await _dB.SaveAsync();
        }

        public async Task DeleteUserCart(int userId)
        {
            _dB.Carts.DeleteAllByUserId(userId);
            await _dB.SaveAsync();
        }

        public async void DeleteOrderAsync(int orderId)
        {
            await _dB.Carts.DeleteAsync(orderId);
        }

        public async Task<OrderBL> FindOrderByIdAsync(int orderId)
        {
            
            return Mapper.Map<OrderBL>(await _dB.Orders.GetAsync(orderId));
        }

        public async Task<List<CartBL>> FindUserCart(int userId)
        {
            var cart = Mapper.Map<List<CartBL>>(await _dB.Carts.GetAll().Where(c => c.UserId == userId).ToListAsync());
            return cart;
        }

        public async Task AddRangeCartAsync(IEnumerable<CartBL> cart)
        {

            await _dB.Carts.AddRangeAsync(Mapper.Map<IEnumerable<Cart>>(cart));
            await _dB.SaveAsync();

        }

       

        public async Task RemoveCartItemsAsync(int id)
        {
            _dB.Carts.RemoveRangeByProductId(id);
            await _dB.SaveAsync();
        }
    }
}
