using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

            if (lstBillDetailView != null)
            {
                List<BillDetailViewModels> mainList = lstBillDetailView;
                int check = 0;
                foreach (var item in mainList)
                {
                    if (item.ID == id)
                    {
                        item.Price = price;

                        if(item.Quantity > 4 || mainList.Sum(p => p.Total) > 50 || mainList.Sum(p => p.Quantity) > 19)
                        {
                            item.Quantity = item.Quantity;
                        }
                        else
                        {
                            item.Quantity += 1;
                        }

                        item.Total = item.Price * item.Quantity;

                        check = 0;
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
                    obj.ID = id;

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

                }


                HttpContext.Session.Set("products", mainList);
            }
            else
            {
                List<BillDetailViewModels> firstList = new List<BillDetailViewModels>();
                BillDetailViewModels obj = new BillDetailViewModels();
                obj.ID = id;
                

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

            //var productCart = lstBillDetailView.FirstOrDefault(p => p.ProductID == id);

            //Product product = FacadeMaker.Instance.GetProductById(id);
            //ProductViewModels productView = new ProductViewModels(product);

            //BillDetailViewModels billDetailView = new BillDetailViewModels();


            //if (lstBillDetailView.Any(p => p.ProductID == id))
            //{
            //    productCart.Quantity += 1;
            //    productCart.Total = productCart.Quantity * productView.Price;
            //}
            //else
            //{
            //    billDetailView.ProductID = id;
            //    billDetailView.ProductName = product.Name;
            //    billDetailView.ProductImage = product.Image;
            //    billDetailView.Price = product.Price;
            //    billDetailView.Quantity = 1;
            //    billDetailView.Total = billDetailView.Price * billDetailView.Quantity;
            //    lstBillDetailView.Add(billDetailView);
            //}

            //HttpContext.Session.Set("products", lstBillDetailView);
            return Json(HttpContext.Session.Get<List<BillDetailViewModels>>("products"));
        }

        [HttpPost]
        public JsonResult RemoveFromCart(int id, decimal price)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");

            if (lstBillDetailView != null)
            {
                List<BillDetailViewModels> mainList = lstBillDetailView;
                int check = 0;
                foreach (var item in mainList)
                {
                    if (item.ID == id)
                    {
                        item.Price = price;

                        if (item.Quantity < 1)
                        {
                            if(item.Quantity == 0)
                            {
                                mainList.Remove(item);
                            }
                            else
                            {
                                item.Quantity = item.Quantity;
                            }
                        }
                        else
                        { 
                            item.Quantity -= 1;
                        }

                        item.Total = item.Price * item.Quantity;

                        check = 0;
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
                    obj.ID = id;

                    if (obj.Quantity < 1)
                    {
                        if (obj.Quantity == 0)
                        {
                            mainList.Remove(obj);
                        }
                        else
                        {
                            obj.Price = obj.Price;
                            obj.Quantity = obj.Quantity;
                            obj.Total = obj.Price * obj.Quantity;
                        }
                    }
                    else
                    {
                        obj.Price = price;
                        obj.Quantity = 1;
                        obj.Total = obj.Price * obj.Quantity;

                        mainList.Add(obj);
                    }

                }


                HttpContext.Session.Set("products", mainList);
            }
            else
            {
                List<BillDetailViewModels> firstList = new List<BillDetailViewModels>();
                BillDetailViewModels obj = new BillDetailViewModels();
                obj.ID = id;


                if (obj.Quantity < 1)
                {
                    if (obj.Quantity == 0)
                    {
                        firstList.Remove(obj);
                    }
                    else
                    {
                        obj.Price = obj.Price;
                        obj.Quantity = obj.Quantity;
                        obj.Total = obj.Price * obj.Quantity;
                    }
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
