using NewStore.Application.Services.Orders.Commands.AddNewOrder;
using NewStore.Application.Services.Orders.Queries.GerOrders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewStore.Application.Interfaces.FacadPatterns
{
    public interface IOrderFacad
    {
        public IAddNewOrderService AddNewOrder { get; }
        public IGetOrdersService GetOrders {  get; }
    }
}
