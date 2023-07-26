using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.Collections.Generic;
using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;
using WebApplication1.Utilities;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private PVStoresContext _context = new PVStoresContext();
        public CategoryController(PVStoresContext context)
        {
            _context = context;
        }
        public IActionResult Index(int cateID)
        {
            var products = FacadeMaker.Instance.GetProductByCateId(cateID).Select(i => new ProductViewModels(i)).ToList();

            if (cateID == 0)
            {
                ViewData["cateName"] = "All";
                ViewData["lstAllProducts"] = FacadeMaker.Instance.GetAllProducts().Select(i => new ProductViewModels(i)).ToList();
            }
            else
            {
                ViewData["cateName"] = FacadeMaker.Instance.GetCategoryById(cateID).Name;
                if (products.Count() == 0)
                {
                    TempData["filterProductFlag"] = "No product found";
                }
                else
                {
                    ViewData["lstAllProducts"] = products;
                }
            }

            List<CategoryViewModels> categoryView = FacadeMaker.Instance.GetAllCategories().Select(i => new CategoryViewModels(i)).ToList();

            return View(categoryView);
        }
        [HttpPost]
        public JsonResult FilterProduct(int cateID)
        {
            var products = FacadeMaker.Instance.GetProductByCateId(cateID).Select(i => new ProductViewModels(i)).ToList();
            if(products.Count() == 0)
            {
                var cateName = FacadeMaker.Instance.GetCategoryById(cateID).Name;
                return Json(cateName);
            }
            if(cateID == 0)
            {
                products = FacadeMaker.Instance.GetAllProducts().Select(i => new ProductViewModels(i)).ToList();
                return Json(products);
            }
            return Json(products);
        }
    }
}
