using Microsoft.AspNetCore.Mvc;
using NewStore.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.ViewComponents
{
    public class Search : ViewComponent
    {
        private readonly ICommonFacad _commonFacad;
        public Search(ICommonFacad commonFacad)
        {
            _commonFacad = commonFacad;
        }

        public IViewComponentResult Invoke()
        {
            return View(viewName: "Search", _commonFacad.GetCategories.Execute().Data);
        }
    }
}
