using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models.ModelPattern;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Models.entities;

namespace DemoASP.Controllers
{
    public class CategoryController : Controller
    {

        private readonly PVStoresContext _context;
        public CategoryController(PVStoresContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var service = new CategoryService();
            //ViewData["Category"] = _context.Categories.ToList();
            ViewData["Category"] = FacadeMaker.Instance.GetAllCategories();
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name, Status, SubCate")] Category category)
        {
            //PVStoresContext context = new();
            //_context.Categories.Add(category);
            //_context.SaveChanges();
            FacadeMaker.Instance.CreateCategory(category);
            return RedirectToAction(nameof(Index));

            //return View(category);
        }

        [HttpPost]
        public IActionResult Update(int id, [Bind("ID, Name, Status, SubCate")]  Category category)
        {
            FacadeMaker.Instance.UpdateCategory(id, category);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            ViewBag.CateUpdate = FacadeMaker.Instance.GetCategoryById(id);
            return View();
        }
    }
}
