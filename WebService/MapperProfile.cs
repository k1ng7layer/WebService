using AutoMapper;
using BusinessLogicLayer.Entities;
using DataAccesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;

namespace WebService
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductModel, ProductBL>();
            CreateMap<ProductBL, ProductModel>();
            CreateMap<Product, ProductBL>();
            CreateMap<OrderModel, OrderBL>();
            CreateMap<UserModel, UserBL>();
            CreateMap<UserBL, UserModel>();
            CreateMap<RoleBL, RoleDTO>();
            CreateMap<RoleDTO, RoleBL>();
            CreateMap<RoleDTO, RoleDTO>();
            CreateMap<RoleModel, RoleBL>();
            CreateMap<RoleBL, RoleModel>();
            CreateMap<UserDTO, UserBL>();
            CreateMap<Cart, CartBL>();
            CreateMap<CartBL, Cart>();
            CreateMap<CartBL, CartModel>();
            CreateMap<CartModel, CartBL>();
            CreateMap<ProductBL, Product>();

            CreateMap<IQueryable<UserDTO>, IQueryable<UserBL>>();
            
            CreateMap<UserBL, UserDTO>();
            CreateMap<OrderBL, OrderModel>();
            CreateMap<OrderDTO, OrderBL>();
            CreateMap<OrderDetailBL, OrderDetail>();
            CreateMap<OrderDetail, OrderDetailBL>(); 
            
        }
    }
}

