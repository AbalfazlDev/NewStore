using NewStore.Application.Services.Orders.Commands.AddNewOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewStore.Application.Interfaces.FacadPatterns
{
    public interface IOrderFacad
    {
        public IAddNewOrderService AddNewOrder { get; }
    }
}
