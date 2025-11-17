using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Products.Commands.AddNewProduct;
using System;
using System.Collections.Generic;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[Action]")]
    public class ProductsController : Controller
    {
        private readonly IProductFacadForAdmin _productFacadAdmin;
        public ProductsController(IProductFacadForAdmin productFacad)
        {
            _productFacadAdmin = productFacad;
        }
        public IActionResult Index(UInt16 page =1,byte pageSize = 20)
        {
            return View(_productFacadAdmin.GetProductForAdmin.Execute(page,pageSize).Data);
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            ViewBag.Categories = new SelectList(_productFacadAdmin.GetAllCategories.Execute().Data, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(RequestAddProductDto request,List<AddProduct_Feature> features)
        {
            List<IFormFile> images = new List<IFormFile>();
            int imagesNumber = Request.Form.Files.Count;
            for (int i = 0;i< imagesNumber;i++)
            {
                var image = Request.Form.Files[i];
                images.Add(image);
            }
            request.Images = images;
            request.Features = features;
            var result = _productFacadAdmin.AddNewProduct.Execute(request);
            return View();
        }

        [HttpGet]
        public IActionResult ProductDetails(long productId)
        {
            var result = _productFacadAdmin.GetProductDetailsForAdmin.Execute(productId);
            return View(result.Data);
        }
    }
}
