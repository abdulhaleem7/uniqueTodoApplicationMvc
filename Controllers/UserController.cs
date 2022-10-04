using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var user = _userService.GetAllUser();
            return View(user);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var user = _userService.GetUser(id);
            return View(user);
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginRequestModel model)
        {
            var ro = "";
            var user = await _userService.Login(model);
            if (user.Success == true)
            {
                var claims = new List<Claim>
                {
                   new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()),
                   new Claim(ClaimTypes.Name, $"{user.Data.UserName}"),
                   new Claim(ClaimTypes.Email,$"{user.Data.Email}"),
                   //new Claim("photo", user.Data.Customer.CustomerPhoto),
                };
                foreach(var role in user.Data.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    ro = role.Name;
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                
                if(ro == "Admin")
                {
                    return RedirectToAction("IndexAdmin", "Admin");
                }
                if(ro == "Customer")
                {
                    return RedirectToAction("IndexCustomer", "Customer");
                }
            }         
       
            ViewBag.error = "Invalid username or password";
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn");
        }
    }
}