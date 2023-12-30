using System.Net;
using System.Security.Claims;
using ibrozk.Data.Abstract;
using ibrozk.Data.Concrete.EfCore;
using ibrozk.Entity;
using ibrozk.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ibrozk.Controllers
{
    public class UsersController : Controller
    {

        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize]
        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isUser = _userRepository.Users.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

                if (isUser != null)
                {
                    var userClaims = new List<Claim>();

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));

                    if (isUser.UserName == "ibrozk")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                        );
                    return RedirectToAction("Edit", "Users");
                }
                else
                {
                    ModelState.AddModelError("", "Kontrol Ediniz!");
                }
            }
            return View(model);
        }




    }
}