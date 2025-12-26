using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewStore.Application.Services.Finances.Queries.GetRequestPay
{
    public interface IGetRequestPayService
    {
        ResultDto<ResultGetRequestPay> Execute(Guid guid);
    }
    public class GetRequestPayService : IGetRequestPayService
    {
        private readonly IDataBaseContext _context;
        public GetRequestPayService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetRequestPay> Execute(Guid guid)
        {
            var result = _context.RequestPays.Where(p => p.Guid== guid).FirstOrDefault();
            if (result == null)
                return new ResultDto<ResultGetRequestPay>
                {
                    IsSuccess = false,
                    Message = "پرداختی یافت نشد"
                };
            return new ResultDto<ResultGetRequestPay>
            {
                IsSuccess = true,
                Data = new ResultGetRequestPay
                {
                    Amount = result.Amount,
                }
            };
        }
    }
    public class ResultGetRequestPay
    {
        public int Amount { get; set; }
    }
}
