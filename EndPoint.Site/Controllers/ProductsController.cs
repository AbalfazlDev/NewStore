using Microsoft.AspNetCore.Mvc;
using NewStore.Application.Interfaces.FacadPatterns;
using System;

namespace EndPoint.Site.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductFacad _productFacad;
        public ProductsController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }
        public IActionResult Index(string searchKey, ushort page = 1, byte pageSize = 20, long? categoryId = null)
        {
            return View(_productFacad.GetProduct.Execute(searchKey, page, pageSize, categoryId).Data);
        }
        [HttpGet]
        public IActionResult Details(long productId)
        {
            return View(_productFacad.GetProductDetails.Execute(productId).Data);
        }


    }
}
