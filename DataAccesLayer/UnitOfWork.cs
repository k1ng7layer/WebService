 using DataAccesLayer.Entities;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Repostitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        //IUserRepository _userRepository;
        //IRepository<OrderDTO> _orderRepository;
        //IRepository<Product> _productRepository;
        //IRepository<Cart> _cartRepository;
        //IRepository<OrderDetailDTO> _orderDetailRepository;
        //IRepository<RoleDTO> _roleRepository;
        //public IUserRepository Users
        //{
        //    get
        //    {
        //        if (_userRepository == null)
        //            _userRepository = new UserRepository(DBContext);
        //        return _userRepository;
        //    }
        //}
        //public IRepository<OrderDTO> Orders
        //{
        //    get
        //    {
        //        if (_orderRepository == null)
        //            _orderRepository = new OrderRepository(DBContext);
        //        return _orderRepository;
        //    }
        //}
        //public IRepository<Product> Product
        //{
        //    get
        //    {
        //        if (_productRepository == null)
        //            _productRepository = new ProductRepository(DBContext);
        //        return _productRepository;
        //    }
        //}
        //public IRepository<Cart> Carts
        //{
        //    get
        //    {
        //        if (_cartRepository == null)
        //            _cartRepository = new CartRepository(DBContext);
        //        return _cartRepository;
        //    }
        //}
        //public IRepository<OrderDetailDTO> OrderDetails
        //{
        //    get
        //    {
        //        if (_orderDetailRepository == null)
        //            _orderDetailRepository = new OrderDetailRepository(DBContext);
        //        return _orderDetailRepository;
        //    }
        //}
        //private ApplicationContext DBContext { get; set; }
        //public IRepository<RoleDTO> Roles
        //{
        //    get
        //    {
        //        if (_roleRepository == null)
        //        {
        //            _roleRepository = new RoleRepository(DBContext);
        //        }
        //        return _roleRepository;
        //    }
        //}
        public IUserRepository Users { get; set; }
     
        public IRepository<OrderDTO> Orders { get; set; }
      
        public IRepository<Product> Product { get; set; }
    
        public ICartRepository Carts { get; set; }

        public IRepository<OrderDetailDTO> OrderDetails { get; set; }
      
        private ApplicationContext DBContext { get; set; }
        public IRepository<RoleDTO> Roles { get; set; }
       


        private bool dispoded = false;
        public UnitOfWork(ApplicationContext DBContext,IUserRepository userRepository, IRepository<OrderDTO> orderRepo, IRepository<Product> productRepo, ICartRepository cartsRepo, IRepository<OrderDetailDTO> orderDetailsRepo, IRepository<RoleDTO> rolesRepo)
        {
            
            this.DBContext = DBContext;
            Users = userRepository;
            Orders = orderRepo;
            Product = productRepo;
            Carts = cartsRepo;
            OrderDetails = orderDetailsRepo;
            Roles = rolesRepo;
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.dispoded)
            {
                if (disposing)
                {
                    DBContext.Dispose();
                }
                this.dispoded = true;
            }   
        }
                
            
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            DBContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await DBContext.SaveChangesAsync();
        }
    }
}
