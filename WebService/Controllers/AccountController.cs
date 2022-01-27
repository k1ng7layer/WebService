using AutoMapper;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using WebService.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebService.Controllers
{
    //[ApiController]
    [Route("Account")]
    public class AccountController:Controller
    {
        IAccountService AccountService { get; set; }
        IProductService ProductService { get; set; }
        IMapper Mapper { get; set; }
        public AccountController(IAccountService accountService,IMapper mapper,IProductService productService)
        {
            AccountService = accountService;
            Mapper = mapper;
            ProductService = productService;
        }

        [Route("Registration")]
        [HttpPost]
       
        public async Task<ActionResult> Register([FromBody] RegistrationModel model)
        {
            if (ModelState.IsValid)
            {

                

                
                var user = await AccountService.FindUserAsync(u => u.Mail == model.Email);

                //var user = users.FirstOrDefault(us => us.Mail == model.Email);
                if (user == null)
                {
                    RoleBL role = await AccountService .GetRoleAsync(r => r.Name == "user");




                    UserBL newUser = new UserBL()
                    {
                        Mail = model.Email,
                        Password = model.Password,

                    };
                    if (role != null)
                        newUser.RoleId = role.Id;

                    await  AccountService.RegisterUserAsync(Mapper.Map<UserBL>(newUser));
                    UserModel uModel = Mapper.Map<UserModel>(newUser);
                    await Authenticate(uModel);
                    return Json(new { Message = "User registered" });
                }
                else
                {

                    return Json(new { Message = "User alredayExists" });
                }
            }
            else
            {
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                return Json(new { Message = "Invalid user data" });
            }

        }
       

        [NonAction]
        public async Task Authenticate(UserModel user)
        {
            //RoleModel role = Mapper.Map<RoleModel>(await AccountService.GetRoleAsync(r => r.Name == "user"));
            var role = user.Role;
            //user.Role = role;
            var claims = new List<Claim>();
            Claim cl1 = new Claim(ClaimsIdentity.DefaultNameClaimType, user.Mail);
            Claim cl2 = new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name);
            claims.Add(cl1);
            claims.Add(cl2);



            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            
        }

        [Route("user")]
        [HttpGet]
        public async Task<ActionResult> ShowCurrentUser()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await AccountService.FindUserAsync(u => u.Mail == User.Identity.Name);
                return new ObjectResult($"You are {User.Identity.Name} and you have {user.Role.Name} rights");
            }
            else
                return new ObjectResult("You are not Authenticated");
        }
        
        [Route("Login")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAsync([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var us = AccountService.Include(x => x.Role)
                   .FirstOrDefault(x => x.Mail == model.Email && x.Password == model.Password);
                var uModel = Mapper.Map<UserModel>(us);




                UserModel user = Mapper.Map<UserModel>(await AccountService.FindUserAsync(us => us.Mail == model.Email && us.Password == model.Password));
                if (us != null)
                {
                    RoleBL role = await AccountService.GetRoleAsync(r => r.Name == "user");
                    await Authenticate(uModel);
                    return Json(new { Message = "Succes" });
                }
                else
                {
                    return Json(new { Message = "User Not Found" });
                }
            }
            else
            {
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                return Json(new { Message = "Invalid user Data" });
            }
        }
   
        [HttpGet]
        [Route("logOut")]
        public async void LogOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Redirect("Home/Index");
        }
        [HttpPost]
        [Route("Test")]
        public JsonResult TestAction([FromBody]TestModel model)
        {
            return Json(model);
        }
    }

}
                    





