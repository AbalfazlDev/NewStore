using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewStore.Application.Services.Users.Queris.GetCommonUesrDetails
{
    public interface IGetCommonUserDetailsService
    {
        ResultDto<CommonUserDetails> Execute(long userId);
    }
    public class GetCommonUserDetailsService : IGetCommonUserDetailsService
    {
        private readonly IDataBaseContext _context;
        public GetCommonUserDetailsService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<CommonUserDetails> Execute(long userId)
        {
            User user = _context.Users.Find(userId);
            return new ResultDto<CommonUserDetails>
            {
                IsSuccess = true,
                Data = new CommonUserDetails
                {
                    UserId = user.Id,
                    UserName = user.Name,
                    Email = user.Email,
                }
            };
        }
    }
    public class CommonUserDetails
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
