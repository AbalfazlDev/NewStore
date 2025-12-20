using EndPoint.Site.Models;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NewStore.Application.Services.Carts;
using NewStore.Common.Dto;

namespace EndPoint.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly CookiesManager _cookiesManager;
        private readonly Guid _browserId;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
            _cookiesManager = new CookiesManager();

        }
        public IActionResult Index()
        {
            ResultDto<GetCartDto> result = _cartService.GetCart(_cookiesManager.GetBrowserId(HttpContext));
            return View(result.Data);
        }

        public IActionResult AddToCart(long productId)
        {
            _cartService.AddToCart(productId, _cookiesManager.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }

        public IActionResult IncrementItem(long cartItemId)
        {
            _cartService.IncrementItemFromCart(cartItemId, _cookiesManager.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }

        public IActionResult DecrementItem(long cartItemId)
        {
            _cartService.DecrementItemFromCart(cartItemId, _cookiesManager.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }

        public IActionResult remove(long cartItemId)
        {
            _cartService.RemoveFromCart(cartItemId, _cookiesManager.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }

        // this action called in view componet cart
        [HttpPost]
        public IActionResult DirectToCart([FromBody] List<CountCartItemDto> cartItems)
        {
            Guid browserId = _cookiesManager.GetBrowserId(HttpContext);
            foreach (CountCartItemDto cartItem in cartItems)
                _cartService.UpdateCountCartItem(cartItem.CartItemId, browserId, cartItem.Count);
            return Json(new { redirectUrl = Url.Action("Index", "Cart") });
        }
    }
}
