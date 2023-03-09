using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication1.ModelPattern.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.ModelPattern;

namespace DemoASP.Controllers
{
    public class CategoryController : Controller
    {

        public CategoryController()
        {
        }
        private readonly PVStoresContext _context;
        public CategoryController(PVStoresContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var service = new CategoryService();
            ViewData["Category"] = _context.Category.ToList();
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("cate_name, cate_status, sub_cate")] Category category)
        {
            //if (ModelState.IsValid)
            //{

            //}
            _context.Category.Add(category);
            return RedirectToAction(nameof(Index));

            //return View(category);
        }
    }
}
