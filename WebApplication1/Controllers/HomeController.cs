using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;
using WebApplication1.Utilities;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<CategoryViewModels> categoryView = FacadeMaker.Instance.GetAllCategories().Select(i => new CategoryViewModels(i)).ToList();
            HttpContext.Session.Set("lstCate", categoryView);
            return View();
        }

        public IActionResult Success()
        {
            HttpContext.Session.Remove("products");
            return View();
        }

        public IActionResult Fail()
        {
            return View();
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