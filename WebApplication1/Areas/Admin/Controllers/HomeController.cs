using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.entities;
using WebApplication1.Utilities;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Account admin = HttpContext.Session.Get<Account>("admin");
            if(admin == null)
            {
                return RedirectToAction("Index", "Account", new { Area = "" });
            }
            return View();
        }

        [HttpPost]
        public JsonResult RemoveAdmin()
        {
            HttpContext.Session.Remove("admin");
            return Json("Success");
        }
    }
}
