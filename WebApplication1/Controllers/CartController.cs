using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Utilities;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            List<ProductViewModels> lstProductView = HttpContext.Session.Get<List<ProductViewModels>>("products");
            if (lstProductView == null)
            {
                lstProductView = new List<ProductViewModels>();
            }
            return View(lstProductView);
        }
    }
}
