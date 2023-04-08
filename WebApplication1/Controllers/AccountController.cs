using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;
using WebApplication1.Utilities;
using System.Text;

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

            HttpContext.Session.Set("account", account);

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
                        account.Name = name;
                        account.Status = true;
                        account.GoogleID = id;

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
                        HttpContext.Session.Set("account", account);
                        TempData["LoginSuccess"] = "Login Successfully";
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
                        HttpContext.Session.Set("account", account);
                        TempData["LoginSuccess"] = "Login Successfully";
                    }
                }
            }

            return View(allSchemeProvider);
        }

        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }
        }

        public static string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword = Convert.FromBase64String(password);
                string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                return decryptedPassword;
            }
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Account account)
        {
            var data = new Account
            {
                Name = account.Name,
                Email = account.Email,
                Password = EncryptPassword(account.Password),
                Status = true
            };

            FacadeMaker.Instance.CreateAccount(data);
            TempData["registerFlag"] = "Login succeeded!";

            return RedirectToAction("Index", "Account");
        }

        public IActionResult Signin(String provider)
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/Account" }, provider);
        }

        [HttpPost]
        public IActionResult Signin(Account account)
        {

            Account accountExist = FacadeMaker.Instance.GetAccountByEmail(account.Email);
            if (!accountExist.Email.Equals(account.Email) || !DecryptPassword(accountExist.Password).Equals(account.Password) || accountExist.Status == false)
            {
                TempData["LoginFlag"] = "Invalid Email or Password";
                return RedirectToAction("Index");
            }

            HttpContext.Session.Set("acc", accountExist);
            TempData["LoginSuccess"] = "Login Successfully";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Signout()
        {
            HttpContext.Session.Remove("acc");
            return RedirectToAction("Index", "Home");
        }

    }
}
