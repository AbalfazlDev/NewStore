using EndPoint.Site.Utilities;
using EndPoint.Site.ViewComponents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Users.Queris.GetCommonUesrDetails;
using static EndPoint.Site.Models.Enums;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class AccountCenterController : Controller
    {
        private readonly IUserFacad _userFacad;
        public AccountCenterController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }
        public IActionResult Index()
        {
            long? userId = ClaimUtilities.GetUserId(User);
            CommonUserDetails commonUserDetails = _userFacad.GetCommonUserDetails.Execute(userId.Value).Data;
            return View(commonUserDetails);
        }

        public IActionResult LoadPanel(AccountCenterPanel panel)
        {
            return ViewComponent("AccountCenterVC", new { panel });
        }
    }
}
