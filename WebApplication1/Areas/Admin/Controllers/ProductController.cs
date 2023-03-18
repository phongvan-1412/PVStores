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
        private IWebHostEnvironment _webHostEnvironment;
        public ProductController(PVStoresContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
            productView.Image = UploadImage(productView);
            Product product = new Product(productView);
            FacadeMaker.Instance.CreateProduct(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(int id, ProductViewModels productView)
        {
            Product newProduct = _context.Products.Where(p => p.ID == productView.ID).FirstOrDefault();
            try
            {
                Product delProduct = new Product(productView);
                delProduct = newProduct;
                string uniqueFileName = string.Empty;
                if (productView.ImageFile != null)
                {
                    if (delProduct.Image != null)
                    {
                        string folder = "img/products/";
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folder, delProduct.Image);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        uniqueFileName = UploadImage(productView);
                    }
                }

                if(productView.Image != null)
                {
                    delProduct.Image = uniqueFileName;
                }

                if(productView.Description != null)
                {
                    delProduct.Description = productView.Description;
                }
                FacadeMaker.Instance.UpdateProduct(id, newProduct);

            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        private string UploadImage(ProductViewModels productView)
        {
            string uniqueFileName = string.Empty;
            if (productView.ImageFile != null)
            {
                string folder = "img/products/";
                uniqueFileName = Guid.NewGuid().ToString() + "-" + productView.ImageFile.FileName;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder, uniqueFileName);

                productView.ImageFile.CopyTo(new FileStream(serverFolder, FileMode.Create));
                productView.Image = productView.ImageFile.FileName;
            }
            return uniqueFileName;
        }
    }
}
