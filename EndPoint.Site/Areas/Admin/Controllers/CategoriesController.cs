using Microsoft.AspNetCore.Mvc;
using NewStore.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IProductFacad _productFacad;
        public CategoriesController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }
        [HttpGet]
        public IActionResult AddNewCategory(long? parentId)
        {
            ViewBag.ParentId = parentId;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCategory(long? parentId, string name)
        {
            var result = _productFacad.AddCategory.Execute(parentId, name);
            return Json(result);
        }
    }
}
