using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Users.Commands.LoginUser;
using NewStore.Application.Services.Users.Commands.RegisterUser;
using NewStore.Common.Dto;
using System.Collections.Generic;
using System.Security.Claims;

namespace EndPoint.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserFacad _userFacad;
        public AuthenticationController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login(string url = "/")
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password, string url = "/")
        {
            ResultLoginUserDto loginResult = _userFacad.Login.Execute(email, password);
            if (loginResult.IsSuccess)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,loginResult.UserId.ToString()),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, loginResult.Name),
                    new Claim(ClaimTypes.Role, "Customer"),
                };


                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true
                };
                HttpContext.SignInAsync(principal, properties);
            }
            return Json(loginResult);
        }

        [HttpGet]
        public IActionResult Register(string email = "")
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public IActionResult Register(string name, string lastname, string email, string password, string confirmPassword)
        {
            RequestRegisterUserDto request = new RequestRegisterUserDto()
            {
                Name = name,
                Lastname = lastname,
                Email = email,
                Password = password,
                RePassword = confirmPassword,
                Roles = new List<RoleInRegisterUserDto>()
                {
                    new RoleInRegisterUserDto()
                    {
                        RoleId = 3,
                    }
                }

            };
            ResultDto<ResultRegisterUserDto> registerResult = _userFacad.Register.Execute(request);
            if (registerResult.IsSuccess == true)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,registerResult.Data.UserId.ToString()),
                    new Claim(ClaimTypes.Email, request.Email),
                    new Claim(ClaimTypes.Name, request.Name),
                    new Claim(ClaimTypes.Role, "Customer"),
                };


                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true
                };
                HttpContext.SignInAsync(principal, properties);

            }
            return Json(registerResult);
        }

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
