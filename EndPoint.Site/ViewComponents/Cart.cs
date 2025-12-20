using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using NewStore.Application.Services.Carts;
using NewStore.Common.Dto;

namespace EndPoint.Site.ViewComponents
{
    public class Cart : ViewComponent
    {
        private readonly ICartService _cartService;
        private readonly CookiesManager _cookiesManager;
        public Cart(ICartService cartService)
        {
            _cartService = cartService;
            _cookiesManager = new CookiesManager();
        }
        [HttpGet]
        public IViewComponentResult Invoke()
        {
            long? userId = ClaimUtilities.GetUserId(HttpContext.User);
            ResultDto<GetCartDto> result = _cartService.GetCart(_cookiesManager.GetBrowserId(HttpContext), userId);
            return View(viewName: "Cart", result.Data);
        }
    }
}
