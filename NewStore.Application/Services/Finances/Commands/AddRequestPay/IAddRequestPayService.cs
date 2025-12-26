using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Finances;
using NewStore.Domain.Entities.Users;
using System;

namespace NewStore.Application.Services.Finances.Commands.AddRequest
{
    public interface IAddRequestPayService
    {
        public ResultDto<ResultRequestPay> Execute(int amount, long userID);
    }
    public class AddRequestPayService : IAddRequestPayService
    {
        private readonly IDataBaseContext _context;
        public AddRequestPayService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultRequestPay> Execute(int amount, long userID)
        {
            User user = _context.Users.Find(userID);
            RequestPay requestPay = new RequestPay()
            {
                Guid = Guid.NewGuid(),
                User = user,
                Amount = amount,
                IsPay = false,

            };
            _context.RequestPays.Add(requestPay);
            _context.SaveChanges();
            return new ResultDto<ResultRequestPay>
            {
                IsSuccess = true,
                Data = new ResultRequestPay
                {
                    Guid = requestPay.Guid,
                    Amount = requestPay.Amount,
                    Email = user.Email,
                    RequestPayId = requestPay.Id,
                }
            };
        }
    }
    public class ResultRequestPay
    {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public int Amount { get; set; }
        public long RequestPayId { get; set; }
    }
}
