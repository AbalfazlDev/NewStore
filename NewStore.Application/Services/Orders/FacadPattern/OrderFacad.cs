using NewStore.Application.Interfaces.Contexts;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Orders.Commands.AddNewOrder;
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
    }
}
