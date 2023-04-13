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
            var productCart = lstBillDetailView.FirstOrDefault(p => p.ProductID == id);

            Product product = FacadeMaker.Instance.GetProductById(id);
            ProductViewModels productView = new ProductViewModels(product);

            BillDetailViewModels billDetailView = new BillDetailViewModels();


            if (lstBillDetailView.Any(p => p.ProductID == id))
            {
                if (lstBillDetailView.Count() > 19 || lstBillDetailView.Sum(b => b.Total) > 50)
                {
                    TempData["cartFlag"] = "Cart reached limitation";
                    RedirectToAction("Index", "Category");
                }
                else
                {
                    if (productCart.Quantity > 4)
                    {
                        productCart.Quantity = productCart.Quantity;
                        HttpContext.Session.Set("products", lstBillDetailView);
                    }
                    productCart.Quantity += 1;
                    productCart.Total = productCart.Quantity * productView.Price;
                    HttpContext.Session.Set("products", lstBillDetailView);
                }
            }
            else
            {
                billDetailView.ProductID = id;
                billDetailView.ProductName = product.Name;
                billDetailView.ProductImage = product.Image;
                billDetailView.Price = product.Price;
                billDetailView.Quantity = 1;
                billDetailView.Total = billDetailView.Price * billDetailView.Quantity;
                lstBillDetailView.Add(billDetailView);
                HttpContext.Session.Set("products", lstBillDetailView);

            }

            return RedirectToAction("ProductDetail", "Product", productView);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            if (lstBillDetailView == null)
            {
                lstBillDetailView = new List<BillDetailViewModels>();
            }

            var productCart = lstBillDetailView.FirstOrDefault(p => p.ProductID == id);

            if (lstBillDetailView.Any(p => p.ProductID == id))
            {
                if (lstBillDetailView.Count() == 0 || productCart.Quantity == 0)
                {
                    productCart.Quantity = 0;
                    HttpContext.Session.Set("products", lstBillDetailView);
                }
                else
                {
                    if (productCart.Quantity == 0)
                    {
                        lstBillDetailView.Remove(productCart);
                        HttpContext.Session.Set("products", lstBillDetailView);
                    }
                    productCart.Quantity -= 1;
                    HttpContext.Session.Set("products", lstBillDetailView);
                }

            }

            Product productDetail = FacadeMaker.Instance.GetProductById(id);
            ProductViewModels productView = new ProductViewModels(productDetail);

            return RedirectToAction("ProductDetail", "Product", productView);
        }
    }
}
