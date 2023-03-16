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
            List<ProductViewModels> productView = FacadeMaker.Instance.GetAllProducts().Select(i => new ProductViewModels(i)).ToList();
            ProductViewModels singleProductView = new ProductViewModels();
            ViewBag.Category = _context.Categories.Where(c => c.Status == true).ToList();
            ViewBag.Product = singleProductView;
            return View(productView);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(ProductViewModels productView)
        {
            Product product = new Product(productView);
            FacadeMaker.Instance.CreateProduct(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(int id, ProductViewModels productView)
        {
            Product product = new Product(productView);
            FacadeMaker.Instance.UpdateProduct(id, product);
            return RedirectToAction(nameof(Index));
        }
    }
}
