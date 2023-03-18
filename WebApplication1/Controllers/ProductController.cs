using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private PVStoresContext _context = new PVStoresContext();
        public ProductController(PVStoresContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ProductViewModels> productView = FacadeMaker.Instance.GetAllProducts().Select(i => new ProductViewModels(i)).ToList();
            return View(productView);
        }
        public IActionResult ProductDetail(int id)
        {
            Product productDetail = FacadeMaker.Instance.GetProductById(id);
            ProductViewModels productView = new ProductViewModels(productDetail);
            return View(productView);
        }
    }
}
