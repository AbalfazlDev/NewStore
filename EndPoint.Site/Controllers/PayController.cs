using Dto.Payment;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Carts;
using NewStore.Application.Services.Finances.Commands.AddRequest;
using NewStore.Application.Services.Orders.Commands.AddNewOrder;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.Carts;
using Newtonsoft.Json;
using System;
using System.Text;
using ZarinPal.Class;

namespace EndPoint.Site.Controllers
{
    [Authorize("Customer")]
    public class PayController : Controller
    {

        private const string MerchantId = "1a2b3c4d-1234-5678-90ab-abcdef123456";

        private readonly IFinancesFacad _financesFacad;
        private readonly ICartService _cartService;
        private readonly CookiesManager _cookiesManager;
        private readonly IOrderFacad _orderFacad;
        public PayController(IFinancesFacad financesFacad, ICartService cartService, IOrderFacad orderFacad)
        {
            _financesFacad = financesFacad;
            _cartService = cartService;
            _cookiesManager = new CookiesManager();
            _orderFacad = orderFacad;
        }

        public async Task<IActionResult> Index()
        {
            long? userId = ClaimUtilities.GetUserId(User);
            ResultDto<GetCartDto> resCart = _cartService.GetCart(_cookiesManager.GetBrowserId(HttpContext), userId);
            if (!resCart.IsSuccess | !resCart.Data.CartItems.Any())
                return Json("سبدخرید خالی است");

            ResultDto<ResultRequestPay> resRequestPay = _financesFacad.AddRequestPay.Execute(resCart.Data.TotalPrice, userId.Value, resCart.Data.CartId);
            if (resCart.Data.TotalPrice > 0)
            {
                var client = new HttpClient();

                var requestData = new
                {
                    merchant_id = MerchantId,
                    amount = resRequestPay.Data.Amount,
                    callback_url = "https://localhost:44349/pay/verify",
                    description = "تست پرداخت",
                    metadata = new
                    {
                        order_id = resRequestPay.Data.RequestPayId.ToString(),
                        email = resRequestPay.Data.Email,
                        mobile = "09120000000"
                    }
                };

                var json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(
                    "https://sandbox.zarinpal.com/pg/v4/payment/request.json",
                    content
                );

                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseBody);

                // IMPORTANT: authority is generated HERE
                if (result.data.code == 100)
                {
                    string authority = result.data.authority;
                    _financesFacad.SetRequestAuthority.Execute(resRequestPay.Data.RequestPayId, authority);
                    // STEP 2: Redirect user to payment page
                    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + authority);
                }

                return Content("Request Error: " + result.errors.message);
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Verify(string Authority, string Status)
        {
            var requestPay = _financesFacad.GetRequestPay.Execute(Authority);
            if (requestPay.IsSuccess == false)
                return Content("درخواستی یافت نشد");
            // Authority comes from ZarinPal (NOT manual input)
            if (Status != "OK")
                return Content("عملیات کنسل شد");

            var client = new HttpClient();

            var verifyData = new
            {
                merchant_id = MerchantId,
                amount = requestPay.Data.Amount,
                authority = Authority
            };

            var json = JsonConvert.SerializeObject(verifyData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(
                "https://sandbox.zarinpal.com/pg/v4/payment/verify.json",
                content
            );

            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseBody);
            long receiveRequestId = (long)result.data.order_id;
            if (result.data.code == 100)
            {
                if (requestPay.Data.RequestId == receiveRequestId)
                {
                    RequestAddNewOrder requestAddNewOrder = new RequestAddNewOrder()
                    {
                        RefId = result.data.ref_id,
                        CartId = requestPay.Data.CartId.Value,
                        RequestPayId = requestPay.Data.RequestId
                    };
                    _orderFacad.AddNewOrder.Execute(requestAddNewOrder);
                    return RedirectToAction("index", "AccountCenter");
                }

                //return Content("عملیات با موفقیت انجام شد" + result.data.ref_id);
            }

            return Content("عملیات شکست‌ خورد: " + result.data.code);
        }
    }
}
