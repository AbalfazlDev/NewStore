using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Products.Commands.AddNewProduct;
using NewStore.Application.Services.Products.Queris.GetAllCategoris;
using NewStore.Application.Tools;
using NewStore.Common.Dto;
using NewStore.Domain.Entities.HomePage;
using NewStore.Domain.Entities.Product;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageController : Controller
    {
        IHomePageFacad _homePageFacad;
        IProductFacadForAdmin _productFacadForAdmin;
        Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public HomePageController(IHomePageFacad homePageFacad, IProductFacadForAdmin productFacadForAdmin, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _homePageFacad = homePageFacad;
            _productFacadForAdmin = productFacadForAdmin;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetPageImages()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddPageImage()
        {
            List<ResultGetAllCategories> categories = _productFacadForAdmin.GetAllCategories.Execute().Data;
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }


        [HttpPost]
        public IActionResult AddPageImage(string name, long categoryId,string filePath, PositionInPage position)
        {
            ResultDto result = _homePageFacad.AddImage.Execute(name, filePath, categoryId, position);
            return Redirect("/");

        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile files)
        {

            UploadDto result = CommonFunctions.UploadFile(_environment, files, "HomePage");
            return Json(new
            {
                success = result.Status,
                filePath = result.FileNameAddress
            });
        }


    }
}
