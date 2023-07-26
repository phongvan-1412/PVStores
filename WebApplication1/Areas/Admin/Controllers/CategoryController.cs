using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.ModelPattern;
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
            Category category = new Category();
            ViewData["Categories"] = FacadeMaker.Instance.GetAllCategoriesAdmin();
            ViewData["Cate"] = category;
            return View();
        }

        [HttpPost]
        [Route("Create")]
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
