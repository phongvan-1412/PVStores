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
        public IActionResult Index()
        {
            List<ProductViewModels> productView = FacadeMaker.Instance.GetAllProducts().Select(i => new ProductViewModels(i)).ToList();
            return View(productView);
        }

        //[HttpPost]
        //public IActionResult AddToCart(int id)
        //{
        //    List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
        //    if (lstBillDetailView == null)
        //    {
        //        lstBillDetailView = new List<BillDetailViewModels>();
        //    }
        //    var productCart = lstBillDetailView.FirstOrDefault(p => p.ProductID == id);

        //    Product product = FacadeMaker.Instance.GetProductById(id);
        //    ProductViewModels productView = new ProductViewModels(product);

        //    BillDetailViewModels billDetailView = new BillDetailViewModels();


        //    if (lstBillDetailView.Any(p => p.ProductID == id))
        //    {
        //        if (lstBillDetailView.Count() > 19 || lstBillDetailView.Sum(b => b.Total) > 50)
        //        {
        //            TempData["cartFlag"] = "Cart reached limitation";
        //            RedirectToAction("Index", "Category");
        //        }
        //        else
        //        {
        //            if (productCart.Quantity > 4)
        //            {
        //                productCart.Quantity = productCart.Quantity;
        //                HttpContext.Session.Set("products", lstBillDetailView);
        //            }
        //            productCart.Quantity += 1;
        //            productCart.Total = productCart.Quantity * productView.Price;
        //            HttpContext.Session.Set("products", lstBillDetailView);
        //        }
        //    }
        //    else
        //    {
        //        billDetailView.ProductID = id;
        //        billDetailView.ProductName = product.Name;
        //        billDetailView.ProductImage = product.Image;
        //        billDetailView.Price = product.Price;
        //        billDetailView.Quantity = 1;
        //        billDetailView.Total = billDetailView.Price * billDetailView.Quantity;
        //        lstBillDetailView.Add(billDetailView);
        //        HttpContext.Session.Set("products", lstBillDetailView);

        //    }

        //    return RedirectToAction(nameof(Index));
        //}
        [HttpPost]
        public JsonResult AddToCart(int id, decimal price)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            Product product = FacadeMaker.Instance.GetProductById(id);

            if (lstBillDetailView != null)
            {
                List<BillDetailViewModels> mainList = lstBillDetailView;
                int check = 0;
                foreach (var item in mainList)
                {
                    if (item.ProductID == id)
                    {
                        item.Price = price;
                        item.ProductImage = product.Image;
                        item.ProductName = product.Name;

                        if (item.Quantity > 4 || mainList.Sum(p => p.Total) > 50 || mainList.Sum(p => p.Quantity) > 19)
                        {
                            item.Quantity = item.Quantity;
                        }
                        else
                        {
                            item.Quantity += 1;
                        }

                        item.Total = item.Price * item.Quantity;

                        check = 0;
                        HttpContext.Session.Set("products", mainList);

                        break;
                    }
                    else
                    {
                        check = 1;
                    }
                }

                if (check == 1)
                {
                    BillDetailViewModels obj = new BillDetailViewModels();
                    obj.ProductID = id;
                    obj.ProductImage = product.Image;
                    obj.ProductName = product.Name;
                    obj.Price = price;

                    if (mainList.Sum(p => p.Total) > 50 || mainList.Sum(p => p.Quantity) > 19)
                    {
                        obj.Price = obj.Price;
                        obj.Quantity = obj.Quantity;
                        obj.Total = obj.Price * obj.Quantity;
                    }
                    else
                    {
                        obj.Price = price;
                        obj.Quantity = 1;
                        obj.Total = obj.Price * obj.Quantity;

                        mainList.Add(obj);
                    }

                    HttpContext.Session.Set("products", mainList);
                }

                if (lstBillDetailView.Count == 0)
                {
                    List<BillDetailViewModels> newList = new List<BillDetailViewModels>();
                    BillDetailViewModels newObj = new BillDetailViewModels();
                    newObj.ProductImage = product.Image;
                    newObj.ProductName = product.Name;
                    newObj.ProductID = id;
                    newObj.Quantity = 1;
                    newObj.Price = price;

                    newList.Add(newObj);
                    HttpContext.Session.Set("products", newList);
                }


            }
            else
            {
                List<BillDetailViewModels> firstList = new List<BillDetailViewModels>();
                BillDetailViewModels obj = new BillDetailViewModels();
                obj.ProductImage = product.Image;
                obj.ProductName = product.Name;
                obj.ProductID = id;
                obj.Price = price;

                if (firstList.Sum(p => p.Total) > 50 || firstList.Sum(p => p.Quantity) > 19)
                {
                    obj.Price = obj.Price;
                    obj.Quantity = obj.Quantity;
                    obj.Total = obj.Price * obj.Quantity;
                }
                else
                {
                    obj.Price = price;
                    obj.Quantity = 1;
                    obj.Total = obj.Price * obj.Quantity;

                    firstList.Add(obj);
                }

                HttpContext.Session.Set("products", firstList);

            }

            //List<BillDetail> a = _context.BillDetails.Where(b => b.ProductID == id).ToList();
            //List<BillDetailViewModels> finalLst = new List<BillDetailViewModels>();

            //foreach (var item in a)
            //{
            //    BillDetailViewModels billDetailView = new BillDetailViewModels(item);

            //    finalLst.Add(billDetailView);
            //}
            //HttpContext.Session.Set("products", finalLst);

            return Json(HttpContext.Session.Get<List<BillDetailViewModels>>("products"));
        }

        [HttpPost]
        public JsonResult RemoveFromCart(int id, decimal price)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            Product product = FacadeMaker.Instance.GetProductById(id);

            if (lstBillDetailView != null)
            {
                List<BillDetailViewModels> mainList = lstBillDetailView;


                foreach (var item in mainList)
                {
                    if (item.ProductID == id)
                    {
                        item.Price = price;
                        item.ProductImage = product.Image;
                        item.ProductName = product.Name;

                        if (item.Quantity < 1)
                        {
                            item.Quantity = item.Quantity;
                        }
                        else
                        {
                            item.Quantity -= 1;
                        }

                        item.Total = item.Price * item.Quantity;
                        break;
                    }

                }
                HttpContext.Session.Set("products", mainList);

            }

            return Json(HttpContext.Session.Get<List<BillDetailViewModels>>("products"));
        }

        //[HttpPost]
        //public IActionResult RemoveFromCart(int id)
        //{
        //    List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
        //    if (lstBillDetailView == null)
        //    {
        //        lstBillDetailView = new List<BillDetailViewModels>();
        //    }

        //    var productCart = lstBillDetailView.FirstOrDefault(p => p.ProductID == id);

        //    if (lstBillDetailView.Any(p => p.ProductID == id))
        //    {
        //        if (lstBillDetailView.Count() == 0 || productCart.Quantity == 0)
        //        {
        //            productCart.Quantity = 0;
        //            HttpContext.Session.Set("products", lstBillDetailView);
        //        }
        //        else
        //        {
        //            if (productCart.Quantity == 0)
        //            {
        //                lstBillDetailView.Remove(productCart);
        //                HttpContext.Session.Set("products", lstBillDetailView);
        //            }
        //            productCart.Quantity -= 1;
        //            HttpContext.Session.Set("products", lstBillDetailView);
        //        }

        //    }

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
