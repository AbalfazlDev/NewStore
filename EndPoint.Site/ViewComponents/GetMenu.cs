using Microsoft.AspNetCore.Mvc;
using NewStore.Application.Interfaces.FacadPatterns;
using System.Xml.Schema;

namespace EndPoint.Site.ViewComponents
{
    public class GetMenu : ViewComponent
    {
        private readonly ICommonFacad _commonFacad;
        public GetMenu(ICommonFacad commonFacad)
        {
            _commonFacad = commonFacad;
        }
        public IViewComponentResult Invoke()
        {
            var menuItems = _commonFacad.GetMenuItems.Execute().Data;
            return View(viewName: "GetMenu", menuItems);
        }
    }
}
