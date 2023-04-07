using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.ModelPattern;
using WebApplication1.Models.entities;
using WebApplication1.ViewModels;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Bill")]
    public class BillController : Controller
    {
        private readonly PVStoresContext _context;
        public BillController(PVStoresContext context)
        {
            _context = context;
        }

        [Route("")]
        public IActionResult Index()
        {

            List<Bill> bill = FacadeMaker.Instance.GetAllBills();
            List<BillViewModels> lstBillVM = new List<BillViewModels>();
            List<BillDetailViewModels> lstBillDetailVM = new List<BillDetailViewModels>();

            foreach (var item in bill)
            {
                BillViewModels billVM = new BillViewModels(item);
                billVM.AccName = FacadeMaker.Instance.GetAccountById(item.AccId).Name;
                lstBillVM.Add(billVM);
            };

            return View(lstBillVM);
        }
    }
}
