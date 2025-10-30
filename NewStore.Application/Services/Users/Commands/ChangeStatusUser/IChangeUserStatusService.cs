using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Users.Commands.ChangeStatusUser
{
    public interface IChangeUserStatusService
    {
        public ResultDto Execute(long userId);
    }
    public class ChangeUserStatusService : IChangeUserStatusService
    {
        private readonly IDataBaseContext _context;
        public ChangeUserStatusService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long userId)
        {
            User user = _context.Users.Find(userId);
            if (user == null)
            {
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "کاربر یافت نشد"
                };
            }

            user.IsActive = !user.IsActive;
            _context.SaveChanges();
            string status = (user.IsActive) ? "فعال" : "غیر فعال";
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"کاربر با موفقیت {status} شد"
            };
        }
    }
}
