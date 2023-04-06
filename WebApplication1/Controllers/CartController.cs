using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Stripe;
using System.Text.Json;
using WebApplication1.Utilities;
using WebApplication1.ViewModels;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            if (lstBillDetailView == null)
            {
                lstBillDetailView = new List<BillDetailViewModels>();
            }

            return View(lstBillDetailView);
        }

        //public IActionResult Increase(int id)
        //{
        //    List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
        //    if (lstBillDetailView == null)
        //    {
        //        lstBillDetailView = new List<BillDetailViewModels>();
        //    }

        //    if (lstBillDetailView.Any(p => p.ProductID == id))
        //    {
        //        decimal productPrice = lstBillDetailView.Where(p => p.ProductID == id).FirstOrDefault().Price;
        //        int updateQuantity = lstBillDetailView.Where(p => p.ProductID == id).FirstOrDefault().Quantity;

        //        if (updateQuantity > 19) 
        //        {
        //            updateQuantity = lstBillDetailView.Where(p => p.ProductID == id).FirstOrDefault().Quantity;
        //        }
        //        else
        //        {
        //            updateQuantity = lstBillDetailView.Where(p => p.ProductID == id).FirstOrDefault().Quantity += 1;
        //        }

        //        decimal totalPrice = lstBillDetailView.Where(p => p.ProductID == id).FirstOrDefault().Total = updateQuantity * productPrice;


        //        HttpContext.Session.Set("products", lstBillDetailView);
        //    }

        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [Route("/Cart/Increase")]
        public JsonResult Increase(string productId)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            if (lstBillDetailView == null)
            {
                lstBillDetailView = new List<BillDetailViewModels>();
            } 

            int.TryParse(productId, out int id);
            var product = lstBillDetailView.FirstOrDefault(p => p.ProductID == id);

            if (lstBillDetailView.Any(p => p.ProductID == id))
            {
                decimal productPrice = product.Price;
                int updateQuantity = product.Quantity;

                if (updateQuantity >= 20)
                {
                    updateQuantity = product.Quantity;
                }
                else
                {
                    updateQuantity = product.Quantity += 1;
                }

                decimal totalPrice = product.Total = updateQuantity * productPrice;

                product.Quantity = updateQuantity;
                product.Total = totalPrice;

                HttpContext.Session.Set("products", lstBillDetailView);
            }

            return Json(lstBillDetailView);
        }

        [HttpPost]
        [Route("/Cart/Decrease")]

        public IActionResult Decrease(string productId)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            if (lstBillDetailView == null)
            {
                lstBillDetailView = new List<BillDetailViewModels>();
            } 

            int.TryParse(productId, out int id);
            var product = lstBillDetailView.FirstOrDefault(p => p.ProductID == id);

            if (lstBillDetailView.Any(p => p.ProductID == id))
            {
                decimal productPrice = product.Price;
                int updateQuantity = product.Quantity;

                if (updateQuantity <= 0)
                {
                    updateQuantity = product.Quantity;
                }
                else
                {
                    updateQuantity = product.Quantity -= 1;
                }

                decimal totalPrice = product.Total = updateQuantity * productPrice;

                product.Quantity = updateQuantity;
                product.Total = totalPrice;

                HttpContext.Session.Set("products", lstBillDetailView);
            }

            return Json(lstBillDetailView);
        }

        [HttpPost]
        public IActionResult CheckOut(string stripeEmail, string stripeToken)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            if (lstBillDetailView == null)
            {
                lstBillDetailView = new List<BillDetailViewModels>();
            }

            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = long.Parse(lstBillDetailView.Sum(p => p.Total).ToString()) * 100,
                Description = "Test Payment",
                Currency = "usd",
                Customer = customer.Id,
                //ReceiptEmail = stripeEmail
                //Metadata = new Dictionary<string, string>
                //{
                //    {"OrderID", "1"},
                //    {"ReceiptNo", "1"}
                //}
            });

            if (charge.Status.Equals("Succeeded"))
            {
                string BalanceTransactionID = charge.BalanceTransactionId;
                return View();
            }
            else
            {

            }

            return View();
        }
    }
}
