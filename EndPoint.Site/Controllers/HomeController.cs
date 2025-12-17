using EndPoint.Site.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewStore.Application.Interfaces.FacadPatterns;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class HomeController : Controller
    {
        IHomePageFacad _homePageFacad;
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IHomePageFacad homePageFacad)
        {
            _logger = logger;
            _homePageFacad = homePageFacad;
        }

        public IActionResult Index()
        {          
            HomePageVM homePageVM = new HomePageVM();
            homePageVM.PageImages = _homePageFacad.GetImages.Execute().Data;
            return View(homePageVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
