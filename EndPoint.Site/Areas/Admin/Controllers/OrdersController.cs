using Microsoft.AspNetCore.Mvc;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Orders.Queries.GetOrdersForAdmin;
using NewStore.Domain.Entities.Order;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderFacad _orderFacad;
        public OrdersController(IOrderFacad orderFacad)
        {
            _orderFacad = orderFacad;
        }
        public IActionResult Index(OrderState orderState,ushort page)
        {
            List<GetOrderForAdminDto> orders = _orderFacad.GetOrdersAdmin.Execute(orderState, page).Data;
            return View(orders);
        }
    }
}
