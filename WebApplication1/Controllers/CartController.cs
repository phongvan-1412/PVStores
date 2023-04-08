using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Stripe;
using System.Text.Json;
using WebApplication1.Utilities;
using WebApplication1.ViewModels;
using WebApplication1.Models.entities;
using System.Collections.Generic;
using WebApplication1.Models.ModelPattern;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private readonly PVStoresContext _context;
        public static List<BillDetailViewModels> billDetail;

        public CartController(PVStoresContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            if (lstBillDetailView == null)
            {
                lstBillDetailView = new List<BillDetailViewModels>();
            }

            return View(lstBillDetailView);
        }

        public IActionResult Increase(int id)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            if (lstBillDetailView == null)
            {
                lstBillDetailView = new List<BillDetailViewModels>();
            }

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

                HttpContext.Session.Set("products", lstBillDetailView);
            }

            return RedirectToAction("Index", "Cart");

        }

        public IActionResult Decrease(int id)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            if (lstBillDetailView == null)
            {
                lstBillDetailView = new List<BillDetailViewModels>();
            }

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

                HttpContext.Session.Set("products", lstBillDetailView);
            }

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Remove(int id)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            if (lstBillDetailView == null)
            {
                lstBillDetailView = new List<BillDetailViewModels>();
            }

            var product = lstBillDetailView.Find(p => p.ProductID == id);

            if (lstBillDetailView.Any(p => p.ProductID == id))
            {
                lstBillDetailView.Remove(product);
                HttpContext.Session.Set("products", lstBillDetailView);
            }

            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public IActionResult CheckOut(string stripeEmail, string stripeToken, decimal total)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            if (lstBillDetailView == null)
            {
                lstBillDetailView = new List<BillDetailViewModels>();
            }

            string email = "";
            int id = 0;
            Models.entities.Account accBill = new Models.entities.Account();

            WebApplication1.Models.entities.Account acc = HttpContext.Session.Get<WebApplication1.Models.entities.Account>("acc");
            if (acc == null)
            {
                acc = new Models.entities.Account();
                HttpContext.Session.Set("acc", acc);
            }
            Models.entities.Account account = HttpContext.Session.Get<WebApplication1.Models.entities.Account>("account");
            if (account == null)
            {
                account = new Models.entities.Account();
                HttpContext.Session.Set("account", account);
            }

            if (acc.Email.Equals("") && account.Email.Equals(""))
            {
                TempData["checkOutFlag"] = "Please log in before payment";
                return RedirectToAction("Index", "Account");
            }
            else
            {
                if (account.Email.Equals(""))
                {
                    email = acc.Email;
                    accBill = acc;
                    id = acc.ID;
                }
                else
                {
                    email = account.Email;
                    accBill = account;
                    id = account.ID;
                }
            }
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = email,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = long.Parse(lstBillDetailView.Sum(p => p.Total).ToString()) * 100,
                Description = "Test Payment",
                Currency = "usd",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail,
                Metadata = new Dictionary<string, string>
            {
                {"OrderID", "1"},
                {"ReceiptNo", "1"}
            }
            });

            if (charge.Status.Equals("succeeded"))
            {
                string BalanceTransactionID = charge.BalanceTransactionId;
                billDetail = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
                Bill bill = new Bill
                {
                    CreatedTime = DateTime.Now.ToString(),
                    Total = total,
                    Status = (int)EnumStatus.Paid,
                    PaymentId = (int)EnumStatus.Stripe,
                    PaymentCode = BalanceTransactionID,
                    AccId = id
                };

                FacadeMaker.Instance.CreateBill(bill);


                List<Bill> billView = _context.Bills.Where(b => b.AccId == accBill.ID).ToList();
                List<BillViewModels> lstBillVM = new List<BillViewModels>();
                foreach (var item in billView)
                {
                    BillViewModels billVM = new BillViewModels(item);
                    lstBillVM.Add(billVM);
                }
                HttpContext.Session.Set("bill", lstBillVM);

                return RedirectToAction("Success", "Home");
            }
            else
            {
                return RedirectToAction("Fail", "Home");
            }

        }

        //public IActionResult CheckOut()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[Route("/Cart/Increase")]
        //public JsonResult Increase(string productId)
        //{
        //    List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
        //    if (lstBillDetailView == null)
        //    {
        //        lstBillDetailView = new List<BillDetailViewModels>();
        //    }

        //    int.TryParse(productId, out int id);
        //    var product = lstBillDetailView.FirstOrDefault(p => p.ProductID == id);

        //    if (lstBillDetailView.Any(p => p.ProductID == id))
        //    {
        //        decimal productPrice = product.Price;
        //        int updateQuantity = product.Quantity;

        //        if (updateQuantity >= 20)
        //        {
        //            updateQuantity = product.Quantity;
        //        }
        //        else
        //        {
        //            updateQuantity = product.Quantity += 1;
        //        }

        //        decimal totalPrice = product.Total = updateQuantity * productPrice;

        //        product.Quantity = updateQuantity;
        //        product.Total = totalPrice;

        //        HttpContext.Session.Set("products", lstBillDetailView);
        //    }

        //    return Json(lstBillDetailView);
        //}

        //[HttpPost]
        //[Route("/Cart/Decrease")]

        //public IActionResult Decrease(string productId)
        //{
        //    List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
        //    if (lstBillDetailView == null)
        //    {
        //        lstBillDetailView = new List<BillDetailViewModels>();
        //    }

        //    int.TryParse(productId, out int id);
        //    var product = lstBillDetailView.FirstOrDefault(p => p.ProductID == id);

        //    if (lstBillDetailView.Any(p => p.ProductID == id))
        //    {
        //        decimal productPrice = product.Price;
        //        int updateQuantity = product.Quantity;

        //        if (updateQuantity <= 0)
        //        {
        //            updateQuantity = product.Quantity;
        //        }
        //        else
        //        {
        //            updateQuantity = product.Quantity -= 1;
        //        }

        //        decimal totalPrice = product.Total = updateQuantity * productPrice;

        //        product.Quantity = updateQuantity;
        //        product.Total = totalPrice;

        //        HttpContext.Session.Set("products", lstBillDetailView);
        //    }

        //    return Json(lstBillDetailView);
        //}
    }
}
