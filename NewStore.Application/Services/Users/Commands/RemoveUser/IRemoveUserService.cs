using Microsoft.EntityFrameworkCore.Storage;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Application.Services.Users.Commands.RemoveUser
{
    public interface IRemoveUserService
    {
        ResultDto Execute(long userId);
    }
    public class RemoveUserService : IRemoveUserService
    {
        private readonly IDataBaseContext _context;
        public RemoveUserService(IDataBaseContext context)
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
                    IsSuccess = false,
                    Message = "کاربر وجود ندارد"
                };
            }

            user.IsRemoved = true;
            user.RemoveTime = DateTime.Now;
            _context.SaveChangesAsync();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "کاربرد با موفقیت حذف شد"
            }; 
        }
    }

}
