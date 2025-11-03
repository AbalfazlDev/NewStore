using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Users.Commands.LoginUser
{
    public interface ILoginUserService
    {
        ResultLoginUserDto Execute(string email, string password);
    }
    public class LoginUserService : ILoginUserService
    {
        IDataBaseContext _context { get; set; }
        private PasswordHasher _passwordHasher;

        public LoginUserService(IDataBaseContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher();
        }

        public ResultLoginUserDto Execute(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ResultLoginUserDto()
                {
                    IsSuccess = false,
                    Message = "ایمیل را وارد کنید"
                };

            }
            if (string.IsNullOrWhiteSpace(password))
            {
                return new ResultLoginUserDto()
                {
                    IsSuccess = false,
                    Message = "رمز خود را وارد کنید"
                };
            }

            var user = _context.Users.
                Include(p => p.UserInRoles)
                .ThenInclude(p => p.Role)
                .Where(p => p.Email.Equals(email) && p.IsActive == true)
                .FirstOrDefault();

            if(user == null)
            {
                return new ResultLoginUserDto()
                {
                    
                    IsSuccess = false,
                    Message = "کاربری با این ایمیل ثبت نام نکرده است :("
                };
            }

            bool resultVerifyPassword = _passwordHasher.VerifyPassword(user.Password, password);
            if(!resultVerifyPassword)
            {
                return new ResultLoginUserDto()
                {
                    IsSuccess = false,
                    Message = "رمز اشتباه است",
                };
            }

            return new ResultLoginUserDto()
            {
                IsSuccess = true,
                Message = "ورود به سایت با موفقیت انجام شد",
                UserId = user.Id,
                Name = user.Name,
                Roles = 3
            };
        }
    }
}
