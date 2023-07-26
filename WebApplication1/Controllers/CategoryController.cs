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
            if(cateID == null)
            {
                ViewData["lstAllProducts"] = FacadeMaker.Instance.GetAllProducts().Select(i => new ProductViewModels(i)).ToList();
            }
            ViewData["lstAllProducts"] = FacadeMaker.Instance.GetProductByCateId(cateID).Select(i => new ProductViewModels(i)).ToList();
            List<CategoryViewModels> categoryView = FacadeMaker.Instance.GetAllCategories().Select(i => new CategoryViewModels(i)).ToList();
            return View(categoryView);
        }
        [HttpPost]
        public JsonResult FilterProduct(int cateID)
        {
            return Json(FacadeMaker.Instance.GetProductByCateId(cateID).Select(i => new ProductViewModels(i)).ToList());
        }
    }
}
