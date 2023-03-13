﻿using Microsoft.AspNetCore.Mvc;
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
            ViewBag.Category = _context.Categories.Where(c => c.Status == true).ToList();
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
