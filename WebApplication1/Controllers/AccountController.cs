using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.entities;
using NuGet.Protocol;
using WebApplication1.Models.ModelPattern;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Utilities;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

        public AccountController(IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            _authenticationSchemeProvider = authenticationSchemeProvider;
        }

        public async Task<IActionResult> Index()
        {
            var allSchemeProvider = (await _authenticationSchemeProvider.GetAllSchemesAsync())
                .Select(n => n.DisplayName).Where(n => !String.IsNullOrEmpty(n));

            Account account = new Account();
            if (User.Identity.IsAuthenticated)
            {

                if (User.Identity.AuthenticationType.Equals("Google") || User.Identity.AuthenticationType.Equals("Facebook"))
                {
                    var id = User.Claims.Where(n => n.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).FirstOrDefault().Value;
                    string email = User.Claims.Where(n => n.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")).FirstOrDefault().Value;
                    var name = User.Claims.Where(n => n.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")).FirstOrDefault().Value;

                    Account accountExist = FacadeMaker.Instance.GetAccountByEmail(email);

                    if (accountExist == null)
                    {
                        account.Email = email;
                        account.Password = "";
                        account.Name = name;
                        account.Birth = "";
                        account.Phone = "";
                        account.Avatar = "hacker.png";
                        account.AvatarBase64 = "";
                        account.History = "";
                        account.Location = "";
                        account.Status = true;
                        account.Type = (int)EnumStatus.Customer;
                        account.DeliAddress = "";
                        account.IP = "";
                        account.FacebookID = "";
                        account.GoogleID = id;
                        account.Avatar = "hacker.png";

                        if (User.Identity.AuthenticationType.Equals("Google"))
                        {
                            account.FacebookID = "";
                            account.GoogleID = id;
                        }
                        else
                        {
                            account.GoogleID = "";
                            account.FacebookID = id;
                        }
                        FacadeMaker.Instance.CreateAccount(account);
                    }
                    else
                    {
                        account = accountExist;
                        if (User.Identity.AuthenticationType.Equals("Google"))
                        {
                            account.GoogleID = id;
                        }
                        else
                        {
                            account.FacebookID = id;
                        }
                        FacadeMaker.Instance.UpdateAccount(account.ID, account);
                    }
                }
            }

            return View(allSchemeProvider);
        }

        public IActionResult Signin(String provider)
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/Account" }, provider);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
