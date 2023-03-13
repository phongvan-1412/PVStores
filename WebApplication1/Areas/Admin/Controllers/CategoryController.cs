using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models.ModelPattern;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Models.entities;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {

        private readonly PVStoresContext _context;
        public CategoryController(PVStoresContext context)
        {
            _context = context;
        }

        [Route("")]
        public IActionResult Index()
        {
            ViewData["Category"] = FacadeMaker.Instance.GetAllCategories();
            return View();
        }

        [Route("Add")]
        public IActionResult Create()
        {
            return View();
        }
         
        [HttpPost]
        [Route("Add")]
        public IActionResult Create([Bind("Name, Status, SubCate")] Category category)
        {
            FacadeMaker.Instance.CreateCategory(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(int id, [Bind("ID, Name, Status, SubCate")] Category category)
        {
            FacadeMaker.Instance.UpdateCategory(id, category);
            return RedirectToAction(nameof(Index));
        }

    }
}
