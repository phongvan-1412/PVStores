using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
            var data = _context.Category.ToList();
            return View(data);
        }
    }
}
