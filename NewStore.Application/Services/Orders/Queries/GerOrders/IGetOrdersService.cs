using Microsoft.EntityFrameworkCore;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Common;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewStore.Application.Services.Orders.Queries.GerOrders
{
    public interface IGetOrdersService
    {
        public ResultDto<ResGetOrdersDto> Execute(ushort page);
    }
    public class GetOrdersService : IGetOrdersService
    {
        private readonly IDataBaseContext _context;
        public GetOrdersService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResGetOrdersDto> Execute(ushort page)
        {
            uint ordersCount;
            List<OrderDto> orders = _context.Orders
                .Include(p=>p.OrdersDetails)
                .ThenInclude(p=>p.Product)
                .ThenInclude(p=>p.ProductImages)
                .ToPage(page,5,out ordersCount)
                .Select(p => new OrderDto
                {
                    OrderId = p.Id,
                    Date = p.InsertTime.ToShortDateString(),
                    FinalPrice = p.FinalPrice,
                    OrderState = p.OrderState,
                    Items = p.OrdersDetails.Select(c=>new ItemDto
                    {
                        ItemId=c.Product.Id,
                        Title = c.Product.Name,
                        ItemSrc = c.Product.ProductImages.FirstOrDefault().Src,
                        Price = c.Price,
                        Count = c.Count,
                    }).ToList()
                }).ToList();
            return new ResultDto<ResGetOrdersDto>
            {
                IsSuccess = true,
                Data = new ResGetOrdersDto
                {
                    OrderCount = ordersCount,
                    Orders = orders
                }
            };
        }
    }

    public class ResGetOrdersDto
    {
        public uint OrderCount { get; set; }
        public List<OrderDto> Orders { get; set; }
    }

    public class OrderDto
    {
        public long OrderId { get; set; }
        public string Date { get; set; }
        public int FinalPrice { get; set; }
        public OrderState OrderState { get; set; }
        public List<ItemDto> Items { get; set; }
    }

    public class ItemDto
    {
        public long ItemId { get; set; }
        public string Title { get; set; }
        public string ItemSrc { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }
}
