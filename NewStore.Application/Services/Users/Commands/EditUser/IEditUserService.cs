using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Users.Commands.EditUser
{
    public interface IEditUserService
    {
        ResultDto Execute(long userId, string name, string lastname, string email);
    }

    public class EditUserService : IEditUserService
    {
        private readonly IDataBaseContext _context;
        public EditUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long userId, string name, string lastname, string email)
        {
            User user = _context.Users.Find(userId);

            if (string.IsNullOrWhiteSpace(name))
            {
               return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نام را وارد کنید"
                };
            }
            if (string.IsNullOrWhiteSpace(lastname))
            {
               return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نام خانوادگی را وارد کنید"
                };
            }
            if (string.IsNullOrWhiteSpace(email))
            {
               return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "ایمیل را وارد کنید"
                };
            }

            user.Name = name;
            user.Lastname = lastname;
            user.Email = email;
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "عملیات با موفقیت انجام شد"
            };
        }
    }
}
