using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Orders.Queries.GerOrders;
using NewStore.Common.Dto;
using static EndPoint.Site.Models.Enums;

namespace EndPoint.Site.ViewComponents
{
    public class AccountCenterVC : ViewComponent
    {
        private readonly IOrderFacad _orderFacad;
        public AccountCenterVC(IOrderFacad orderFacad)
        {
            _orderFacad = orderFacad;
        }
        public IViewComponentResult Invoke(AccountCenterPanel panel, ushort page=1)
        {
            switch (panel)
            {
                case AccountCenterPanel.Orders:
                    ResultDto<ResGetOrdersDto> resOrders = _orderFacad.GetOrders.Execute(page);
                    if (resOrders.IsSuccess) return View("Orders", resOrders.Data);
                    break;
              

                   
            }
            // page 404 not complate
            return View("404");
        }
    }
}
