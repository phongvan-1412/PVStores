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
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            if (lstBillDetailView == null)
            {
                lstBillDetailView = new List<BillDetailViewModels>();
            }

            Product product = FacadeMaker.Instance.GetProductById(id);
            ProductViewModels productView = new ProductViewModels(product);

            BillDetailViewModels billDetailView = new BillDetailViewModels();

            if (lstBillDetailView.Any(p => p.ProductID == id))
            {
                int updateQuantity = lstBillDetailView.Where(p => p.ProductID == id).FirstOrDefault().Quantity += 1;
                lstBillDetailView.Where(p => p.ProductID == id).FirstOrDefault().Total = updateQuantity * productView.Price;
            }
            else
            {
                billDetailView.ProductID = id;
                billDetailView.ProductName = product.Name;
                billDetailView.ProductImage = product.Image;
                decimal price = billDetailView.Price = product.Price;
                int quantity = billDetailView.Quantity = 1;
                billDetailView.Total = price * quantity;
                lstBillDetailView.Add(billDetailView);
            }
            HttpContext.Session.Set("products", lstBillDetailView);

            return RedirectToAction("ProductDetail", "Product", productView);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            if (lstBillDetailView.Any(p => p.ProductID == id))
            {
                int updateQuantity = lstBillDetailView.Where(p => p.ProductID == id).FirstOrDefault().Quantity -= 1;
                HttpContext.Session.Set("products", lstBillDetailView);
            }

            Product productDetail = FacadeMaker.Instance.GetProductById(id);
            ProductViewModels productView = new ProductViewModels(productDetail);

            return RedirectToAction("ProductDetail", "Product", productView);
        }
    }
}
