using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;
using WebApplication1.ViewModels;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly PVStoresContext _context;
        public ProductController(PVStoresContext context)
        {
            _context = context;
        }

        [Route("")]
        public IActionResult Index()
        {
            ProductIndexViewModels productView = new ProductIndexViewModels();
            productView.Categories = _context.Categories.ToList();
            productView.Products = FacadeMaker.Instance.GetAllProducts();
            //ViewData["Product"] = FacadeMaker.Instance.GetAllProducts();
            //ViewData["CateID"] = _context.Categories.ToList();
            return View(productView);
        }

        [Route("Add")]
        public IActionResult Create()
        {
            ViewData["CateID"] = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Create([Bind("Name, Description, Price, Status, Image")] Product product)
        {
            FacadeMaker.Instance.CreateProduct(product);
            return RedirectToAction(nameof(Index));
        }

        [Route("Update")]
        public IActionResult Update(int id)
        {
            ViewBag.ProductUpdate = FacadeMaker.Instance.GetProductById(id);
            return View();
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(int id, [Bind("ID, Name, Description, Price, Status, Image")] Product product)
        {
            FacadeMaker.Instance.UpdateProduct(id, product);
            return RedirectToAction(nameof(Index));
        }
    }
}
