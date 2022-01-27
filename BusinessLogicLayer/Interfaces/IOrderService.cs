using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderBL order);
        public Task CreateOrderDetailAsync(OrderDetailBL orderDetail);
        void DeleteOrderAsync(int orderId);
        Task<OrderBL> FindOrderByIdAsync(int orderId);
        Task AddToUserCart(CartBL cart);
        Task<List<CartBL>> FindUserCart(int userId);
        Task DeleteUserCart(int userId);
        Task AddRangeCartAsync(IEnumerable<CartBL> cart);
        Task RemoveCartItemsAsync(int id);
    }
}
