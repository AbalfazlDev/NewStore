using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NewStore.Application.Services.Users.Commands.ChangeStatusUser;
using NewStore.Application.Services.Users.Commands.EditUser;
using NewStore.Application.Services.Users.Commands.RegisterUser;
using NewStore.Application.Services.Users.Commands.RemoveUser;
using NewStore.Application.Services.Users.Queris.GetRole;
using NewStore.Application.Services.Users.Queris.GetUser;
using NewStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Route("Admin/[controller]/[action]")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IGetUserServise _userServise;
        private readonly IGetRolesService _getRolesService;
        private readonly IRegisterUserService _registerUserService;
        private readonly IRemoveUserService _removeUserService;
        private readonly IChangeUserStatusService _changeUserStatusService;
        private readonly IEditUserService _editUserService;
        public UsersController(IGetUserServise getUserServise, IGetRolesService getRolesService, IRegisterUserService registerUserService, IRemoveUserService removeUserService, IChangeUserStatusService changeUserStatusService, IEditUserService editUserService)
        {
            _userServise = getUserServise;
            _getRolesService = getRolesService;
            _registerUserService = registerUserService;
            _removeUserService = removeUserService;
            _changeUserStatusService = changeUserStatusService;
            _editUserService = editUserService;
        }
        [HttpGet]
        [HttpPost]
        public IActionResult Index(string searchKey, int page = 1)
        {
            var result = _userServise.Execute(new RequestGetUserDto { SearchKey = searchKey, Page = page }).Users;
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var tt = _getRolesService.Execute().Data.ToList();

            ViewBag.tst = "hello";
            ViewBag.Roles = new SelectList(_getRolesService.Execute().Data, "Id", "Name", null);
            return View();

        }

        [HttpPost]
        public IActionResult Create(string name, string lastname, string email, int age, string password, string rePassword, int roleId)
        {
            RequestRegisteUserDto request = new RequestRegisteUserDto();
            request.Name = name;
            request.Lastname = lastname;
            request.Email = email;
            request.Age = (Int16)age;
            request.Password = password;
            request.RePassword = rePassword;
            request.Roles = new List<RoleInRegisterUserDto>
            {
                new RoleInRegisterUserDto()
                {
                    RoleId = roleId
                }
            };

            var result = _registerUserService.Execute(request);
            return Json(result);
        }

        [HttpPost]
        public IActionResult Remove(long userId)
        {
            var result = _removeUserService.Execute(userId);
            return Json(result);
        }


        [HttpPost]
        public IActionResult ChangeUserStatus(long userId)
        {
            var result = _changeUserStatusService.Execute(userId);
            return Json(result);
        }

        [HttpPost]
        public IActionResult Edit(long userId, string name, string lastname, string email)
        {
           ResultDto result= _editUserService.Execute(userId, name, lastname, email);
            return Json(result);
        }

    }
}
