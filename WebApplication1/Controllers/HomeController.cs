using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var data = _context.Categories.ToList();
            //Console.WriteLine(data);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Google
        public async Task GoogleLogin()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            }); ;
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            return Json(claims);
        }

        public async Task<IActionResult> GoogleLogout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        //////Facebook
        //public async Task FacebookLogin()
        //{
        //    await HttpContext.ChallengeAsync(FacebookDefaults.AuthenticationScheme, new AuthenticationProperties()
        //    {
        //        RedirectUri = Url.Action("FacebookResponse")
        //    }); ;
        //}

        //public async Task<IActionResult> FacebookResponse()
        //{
        //    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
        //    {
        //        claim.Issuer,
        //        claim.OriginalIssuer,
        //        claim.Type,
        //        claim.Value
        //    });

        //    return Json(claims);
        //}

        //public async Task<IActionResult> FacebookLogout()
        //{
        //    await HttpContext.SignOutAsync();
        //    return RedirectToAction("Index");
        //}

    }
}