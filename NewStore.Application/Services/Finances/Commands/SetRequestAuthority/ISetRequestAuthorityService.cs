using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Finances;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewStore.Application.Services.Finances.Commands.SetRequestAuthority
{
    public interface ISetRequestAuthorityService
    {
        ResultDto Execute(long requestId, string authority);
    }
    public class SetRequestAuthorityService : ISetRequestAuthorityService
    {
        private readonly IDataBaseContext _context;
        public SetRequestAuthorityService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long requestId, string authority)
        {
            RequestPay requestPay = _context.RequestPays.Find(requestId);
            requestPay.Authority = authority;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
            };
        }
    }
}
