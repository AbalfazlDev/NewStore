using Microsoft.AspNetCore.Mvc;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Products.Queris.GetCategories;
using System.Collections.Generic;
using System.Net.Sockets;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Route("Admin/[controller]/[action]")]
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IProductFacad _productFacad;
        public CategoriesController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }
        
        public IActionResult Index(long? parentId)
        { 
            List<ResultGetCategories> result= _productFacad.GetCategories.Execute(parentId).Data;
            return View(result);
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
