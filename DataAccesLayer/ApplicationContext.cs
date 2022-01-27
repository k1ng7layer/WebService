using DataAccesLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class ApplicationContext:DbContext
    {
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetailDTO> OrderDetails { get; set; }
        public DbSet<RoleDTO> Roles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            //Init();
            //SaveChanges();
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        //public ApplicationContext()
        //{
        //    //Database.EnsureDeleted();
        //    //Database.EnsureCreated();
        //}

        void Init()
        {
            var vs = Users.Include<UserDTO, string>(x => x.Mail);
            Product pr1 = new Product
            {
                Id = 1,
                ProductName = "IPhone",
                Category = "Phone",
                Price = 10000,
                OnSale = false,
                Count = 100,
            };
            Product pr2 = new Product
            {
                Id = 2,
                ProductName = "Nintendo",
                Category = "Game Device",
                Price = 1000,
                OnSale = true,
                Count = 300,
            };
            Product pr3 = new Product
            {
                Id = 3,
                ProductName = "XBox",
                Category = "Game Device",
                Price = 2000,
                OnSale = false,
                Count = 500,
            };
            Products.Add(pr1);
            Products.Add(pr2);
            Products.Add(pr3);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=TutorialDB;Trusted_Connection=True;");
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product[]
                {
                    new Product
                    {
                        Id = 1,
                        ProductName = "IPhone23",
                        Category = "Phone",
                        Price = 10000,
                        OnSale = false,
                        Count = 100,
                    },
                    new Product
                    {
                        Id = 2,
                        ProductName = "Nintendo",
                        Category = "Game Device",
                        Price = 1000,
                        OnSale = true,
                        Count = 300,
                    },
                    new Product
                    {
                        Id = 3,
                        ProductName = "XBox",
                        Category = "Game Device",
                        Price = 2000,
                        OnSale = false,
                        Count = 500,
                    },

                });
            //var users = Users.Include(x => x.Role);
            string adminRoleName = "admin";
            string userRoleName = "user";
            string adminEmail = "admin@mail.ru";
            string adminPass = "admin";
            RoleDTO adminRole = new RoleDTO { Id = 1,Name = adminRoleName };
            RoleDTO userRole = new RoleDTO { Id = 2,Name = userRoleName };
            UserDTO adminUser = new UserDTO { Id = 1, Mail = adminEmail, Password = adminPass, RoleId = adminRole.Id };
            modelBuilder.Entity<RoleDTO>().HasData(new RoleDTO[] { adminRole, userRole });
            modelBuilder.Entity<UserDTO>().HasData(new UserDTO[] { adminUser });
            
            base.OnModelCreating(modelBuilder);
        } 
    }
}
