using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataAccesLayer;
using DataAccesLayer.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using DataAccesLayer.Entities;
using DataAccesLayer.Repostitories;
using Newtonsoft.Json;

namespace WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        IConfiguration Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddSession();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IRepository<OrderDetailDTO>, OrderDetailRepository>();
            services.AddScoped<IRepository<OrderDTO>, OrderRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<RoleDTO>, RoleRepository>();
            services.AddScoped<IRepository<UserDTO>, UserRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddMvc().AddNewtonsoftJson();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(opt=>opt.UseSqlServer(connection),ServiceLifetime.Scoped);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie((options) => //CookieAuthenticationOptions
                {
                  options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                  options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            
            app.UseSession();
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", pattern: "{controller}/{action}/{id?}");
            });
        }
    }
}
