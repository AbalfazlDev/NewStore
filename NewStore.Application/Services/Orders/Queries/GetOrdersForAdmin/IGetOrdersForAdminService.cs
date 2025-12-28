using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace NewStore.Application.Services.Orders.Queries.GetOrdersForAdmin
{
    public interface IGetOrdersForAdminService
    {
        public ResultDto<List<GetOrderForAdminDto>> Execute(OrderState orderState, ushort page);
    }
    public class GetOrdersForAdminService : IGetOrdersForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetOrdersForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<GetOrderForAdminDto>> Execute(OrderState orderState, ushort page)
        {
            uint ordersCount = 0;
            List<GetOrderForAdminDto> orders = _context.Orders
                .Include(p=>p.User)
                .ToPage(page, 4, out ordersCount)
                .Where(p => p.OrderState == orderState)
                .Select(p => new GetOrderForAdminDto
                {
                    CustomerEmail = p.User.Email,
                    UserId = p.UserId,
                    RequestId = p.RequestId,
                    OrderId = p.Id,
                    OrderState = p.OrderState,
                    InsertTime = p.InsertTime,
                    FinalPrice = p.FinalPrice,
                }).ToList();

            return new ResultDto<List<GetOrderForAdminDto>>
            {
                IsSuccess = true,
                Data = orders,
            };
        }
    }




    public class GetOrderForAdminDto
    {
        public string CustomerEmail { get; set; }
        public long UserId { get; set; }
        public long RequestId { get; set; }
        public long OrderId { get; set; }
        public OrderState OrderState { get; set; }
        public DateTime InsertTime { get; set; }
        public int FinalPrice { get; set; }
    }
}
