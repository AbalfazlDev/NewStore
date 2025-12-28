using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Carts;
using NewStore.Domain.Entities.Finances;
using NewStore.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NewStore.Application.Services.Orders.Commands.AddNewOrder
{
    public interface IAddNewOrderService
    {
        public ResultDto Execute( RequestAddNewOrder  request);
    }
    public class RequestAddNewOrder
    {
        public long RequestPayId { get; set; }
        public long CartId { get; set; }
        public long RefId { get; set; }
    }
    public class AddNewOrderService : IAddNewOrderService
    {
        private readonly IDataBaseContext _context;
        public AddNewOrderService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestAddNewOrder request)
        {
            RequestPay requestPay = _context.RequestPays.Include(p => p.User).Where(p => p.Id == request.RequestPayId).FirstOrDefault();
            if (requestPay == null)
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "فاکتور پرداخت یافت نشد"
                };
            requestPay.RefId = request.RefId;
            requestPay.IsPay = true;
            requestPay.PayDate = DateTime.Now;
            _context.SaveChanges();

            Cart cart = _context.Carts.Include(p=>p.CartItems).ThenInclude(p=>p.Proudct).Where(p => p.Id == request.CartId && !p.IsFinished).FirstOrDefault();
            if (cart == null)
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "سبدخرید یافت نشد"
                };

            Order order = new Order
            {
                User = requestPay.User,
                RequestPay = requestPay,
                OrderState = OrderState.Processing,
                Address = "",
            };

            Collection<OrderDetails> lstOrderDetails = new Collection<OrderDetails>();
            foreach (var item in cart.CartItems)
            {
                OrderDetails orderDetails = new OrderDetails()
                {
                    Order = order,
                    Product = item.Proudct,
                    Count = item.Count,
                    Price = item.Proudct.Price,
                };
                lstOrderDetails.Add(orderDetails);
            }
            order.FinalPrice = lstOrderDetails.Sum(p => p.Price);

            cart.IsFinished = true;

            _context.OrderDetails.AddRange(lstOrderDetails);
            _context.Orders.Add(order);
            _context.SaveChanges();

            return new ResultDto { IsSuccess = true };
        }
    }
}
