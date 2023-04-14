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
            List<BillDetailViewModels> finalLst = new List<BillDetailViewModels>();
            if (lstBillDetailView == null)
            {
                lstBillDetailView = new List<BillDetailViewModels>();
            }

            foreach (var item in lstBillDetailView)
            {
                if(item.Quantity != 0)
                {
                    finalLst.Add(item);
                }
            }

            return View(finalLst);
        }

        //public IActionResult Increase(int id)
        //{
        //    List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
        //    if (lstBillDetailView == null)
        //    {
        //        lstBillDetailView = new List<BillDetailViewModels>();
        //    }

        //    var product = lstBillDetailView.FirstOrDefault(p => p.ProductID == id);

        //    if (lstBillDetailView.Any(p => p.ProductID == id))
        //    {

        //        if (lstBillDetailView.Count() > 19 || lstBillDetailView.Sum(b => b.Total) > 50)
        //        {
        //            TempData["cartFlag"] = "Cart reached limitation";
        //            RedirectToAction("Index", "Category");
        //        }
        //        else
        //        {
        //            if (product.Quantity > 4)
        //            {
        //                product.Quantity = product.Quantity;
        //                HttpContext.Session.Set("products", lstBillDetailView);
        //            }
        //            else
        //            {
        //                product.Quantity += 1;
        //                product.Total = product.Quantity * product.Price;

        //            }
        //        }
        //        product.Total = product.Quantity * product.Price;

        //        HttpContext.Session.Set("products", lstBillDetailView);
        //    }

        //    return RedirectToAction("Index", "Cart");

        //}

        //public IActionResult Decrease(int id)
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

        //    return RedirectToAction("Index", "Cart");
        //}

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

            if (lstBillDetailView.Sum(p => p.Quantity) == 0)
            {
                TempData["cartEmpty"] = "Cart is empty. Please choose at least one product";
                lstBillDetailView = new List<BillDetailViewModels>();

                HttpContext.Session.Set("products", lstBillDetailView);

                return RedirectToAction("Index", "Category");
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
                TempData["checkOutFlag"] = "Card is valid but please log in before payment";
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

        [HttpPost]
        public JsonResult Increase(int id, decimal price)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            Models.entities.Product product = FacadeMaker.Instance.GetProductById(id);

            int qty = 0;
            for (int i = 0; i < lstBillDetailView.Count; i++)
            {
                if (lstBillDetailView[i].ProductID == id)
                {
                    lstBillDetailView[i].ProductImage = product.Image;
                    lstBillDetailView[i].ProductName = product.Name;

                    if (lstBillDetailView[i].Quantity > 4 || lstBillDetailView.Sum(p => p.Total) > 50 || lstBillDetailView.Sum(p => p.Quantity) > 19)
                    {
                        lstBillDetailView[i].Quantity = lstBillDetailView[i].Quantity;
                    }
                    else
                    {
                        lstBillDetailView[i].Quantity += 1;
                    }

                    qty = lstBillDetailView[i].Quantity;
                    lstBillDetailView[i].Total = qty * price;
                    break;
                }
            }
            HttpContext.Session.Set("products", lstBillDetailView);

            return Json(lstBillDetailView);
        }

        [HttpPost]
        public IActionResult Decrease(int id, decimal price)
        {
            List<BillDetailViewModels> lstBillDetailView = HttpContext.Session.Get<List<BillDetailViewModels>>("products");
            List<BillDetailViewModels> finalLst = new List<BillDetailViewModels>();
            Models.entities.Product product = FacadeMaker.Instance.GetProductById(id);

            int qty = 0;
            for (int i = 0; i < lstBillDetailView.Count; i++)
            {
                if (lstBillDetailView[i].ProductID == id)
                {
                    lstBillDetailView[i].ProductImage = product.Image;
                    lstBillDetailView[i].ProductName = product.Name;

                    if (lstBillDetailView[i].Quantity > 1)
                    {
                        lstBillDetailView[i].Quantity -= 1;
                        qty = lstBillDetailView[i].Quantity;

                        lstBillDetailView[i].Total = qty * price;
                        break;
                    }
                    else
                    {
                        qty = 1;
                        lstBillDetailView[i].Total = qty * price;

                        break;
                    }

                }

            }
            HttpContext.Session.Set("products", lstBillDetailView);

            foreach (var item in lstBillDetailView)
            {
                if(item.Quantity != 0)
                {
                    finalLst.Add(item);
                }
            }

            HttpContext.Session.Set("products", finalLst);

            return Json(HttpContext.Session.Get<List<BillDetailViewModels>>("products"));
        }
    }
}
