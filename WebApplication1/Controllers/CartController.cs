using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using WebApplication1.Utilities;
using WebApplication1.ViewModels;

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
