using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Account")]
    public class AccountController : Controller
    {
        private PVStoresContext _context = new PVStoresContext();
        public AccountController(PVStoresContext context)
        {
            _context = context;
        }

        [Route("")]
        public IActionResult Index()
        {
            List<Account> accounts = FacadeMaker.Instance.GetAllAccounts();
            return View(accounts);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(int id, Account account)
        {
            Account acc = _context.Accounts.FirstOrDefault(a => a.ID == id);
            if (account.Status == true)
            {
                acc.Status = false;
                FacadeMaker.Instance.UpdateAccount(id, acc);

            }
            else
            {
                acc.Status = true;
                FacadeMaker.Instance.UpdateAccount(id, acc);

            }
            return RedirectToAction("Index", "Account");
        }
    }
}
