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
            if (productView.ImageFile != null)
            {
                string folder = "img/products/";
                folder += productView.ImageFile.FileName;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                productView.ImageFile.CopyTo(new FileStream(serverFolder, FileMode.Create));
                productView.Image = productView.ImageFile.FileName;
            }
            Product product = new Product(productView);
            FacadeMaker.Instance.CreateProduct(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(int id, ProductViewModels productView)
        {
            ProductViewModels newProduct = new ProductViewModels
            {
                ID = productView.ID,
                Name = productView.Name,
                Description = productView.Description,
                Price = productView.Price,
                Status = productView.Status,
                Image = productView.Image,
                ImageFile = productView.ImageFile,
                CategoryId = productView.CategoryId,
                CateName = productView.CateName
            };

            if (productView.ImageFile != null)
            {
                string folder = "img/products/";
                folder += productView.ImageFile.FileName;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                FileInfo fi = new FileInfo(serverFolder);
                if (fi != null)
                {
                    System.IO.File.Delete(serverFolder);
                    fi.Delete();
                }

                newProduct.ImageFile.CopyTo(new FileStream(serverFolder, FileMode.Create));
                newProduct.Image = newProduct.ImageFile.FileName;
            }
            
            Product product = new Product(newProduct);
            FacadeMaker.Instance.UpdateProduct(id, product);

            return RedirectToAction(nameof(Index));
        }
    }
}
