using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;
using WebApplication1.Utilities;
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

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            List<ProductViewModels> lstProductView = HttpContext.Session.Get<List<ProductViewModels>>("products");
            if (lstProductView == null)
            {
                lstProductView = new List<ProductViewModels>();
            }

            Product product = FacadeMaker.Instance.GetProductById(id);
            ProductViewModels productView = new ProductViewModels(product);

            lstProductView.Add(productView);
            HttpContext.Session.Set("products", lstProductView);

            return RedirectToAction("ProductDetail", "Product", lstProductView.First(p => p.ID == id));
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            List<ProductViewModels> lstProductView = HttpContext.Session.Get<List<ProductViewModels>>("products");
            if (lstProductView.Any(p => p.ID == id))
            {
                lstProductView.Remove(lstProductView.First(p => p.ID == id));
                HttpContext.Session.Set("products", lstProductView);
            }

            Product productDetail = FacadeMaker.Instance.GetProductById(id);
            ProductViewModels productView = new ProductViewModels(productDetail);

            return RedirectToAction("ProductDetail", "Product", productView);
        }
    }
}
