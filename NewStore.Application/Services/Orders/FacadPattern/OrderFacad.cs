using NewStore.Application.Interfaces.Contexts;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Orders.Commands.AddNewOrder;
using NewStore.Application.Services.Orders.Queries.GerOrders;
using NewStore.Application.Services.Orders.Queries.GetOrdersForAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NewStore.Application.Services.Orders.FacadPattern
{
    public class OrderFacad : IOrderFacad
    {
        private readonly IDataBaseContext _context;
        public OrderFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IAddNewOrderService _addNewOrder;
        public IAddNewOrderService AddNewOrder
        {
            get { return _addNewOrder = _addNewOrder ?? new AddNewOrderService(_context); }
        }

        private IGetOrdersService _getOrders;
        public IGetOrdersService GetOrders
        {
            get
            {
                return _getOrders = _getOrders ?? new GetOrdersService(_context);
            }
        }

        private IGetOrdersForAdminService _getOrdersAdmin;
        public IGetOrdersForAdminService GetOrdersAdmin
        {
            get
            {
                return _getOrdersAdmin= _getOrdersAdmin ?? new GetOrdersForAdminService(_context);
            }
        }


    }
}
