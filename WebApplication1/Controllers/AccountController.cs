using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;
using WebApplication1.Utilities;
using System.Text;
using WebApplication1.ViewModels;
using System.Net.NetworkInformation;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;
        private readonly PVStoresContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public AccountController(IAuthenticationSchemeProvider authenticationSchemeProvider, PVStoresContext context, IWebHostEnvironment webHostEnvironment)
        {
            _authenticationSchemeProvider = authenticationSchemeProvider;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
                        account.Name = name;
                        account.Status = true;

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

                        account.Password = EncryptPassword(account.Password);   
                        FacadeMaker.Instance.CreateAccount(account);
                        HttpContext.Session.Set("account", account);

                        var siteCookies = HttpContext.Request.Cookies.Where(n => n.Key.Equals("Cookies"));
                        foreach (var cookie in siteCookies)
                        {
                            Response.Cookies.Delete(cookie.Key);
                        }

                        List<Bill> bill = _context.Bills.Where(b => b.AccId == account.ID).ToList();
                        List<BillViewModels> lstBillVM = new List<BillViewModels>();
                        foreach (var item in bill)
                        {
                            BillViewModels billVM = new BillViewModels(item);
                            lstBillVM.Add(billVM);
                        }
                        HttpContext.Session.Set("bill", lstBillVM);

                        TempData["LoginSuccess"] = "Login Successfully";
                    }
                    else
                    {
                        account = accountExist;

                        if (accountExist.Status)
                        {
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

                            var siteCookies = HttpContext.Request.Cookies.Where(n => n.Key.Equals("Cookies"));
                            foreach (var cookie in siteCookies)
                            {
                                Response.Cookies.Delete(cookie.Key);
                            }

                            List<Bill> bill = _context.Bills.Where(b => b.AccId == account.ID).ToList();
                            List<BillViewModels> lstBillVM = new List<BillViewModels>();
                            foreach (var item in bill)
                            {
                                BillViewModels billVM = new BillViewModels(item);
                                lstBillVM.Add(billVM);
                            }
                            HttpContext.Session.Set("bill", lstBillVM);

                            TempData["LoginSuccess"] = "Login Successfully";
                        }
                        else
                        {
                            TempData["socialFlag"] = "Your email has been blocked. Please try another email!";
                        }
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

            if (data.Email.Equals(_context.Accounts.FirstOrDefault(a => a.Email.Equals(account.Email) && a.Type == (int)EnumStatus.Admin).Email) || data.Email.Equals(_context.Accounts.FirstOrDefault(a => a.Email.Equals(account.Email))))
            {
                TempData["regiterFail"] = "Your email is valid, please try another email";
            }
            else
            {
                FacadeMaker.Instance.CreateAccount(data);
                TempData["registerSuccess"] = "Register succeeded! Please Log in to your account";
            }

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

            if (accountExist == null)
            {
                accountExist = new Account();
            }

            if (!accountExist.Email.Equals(account.Email) || !DecryptPassword(accountExist.Password).Equals(account.Password) || accountExist.Status == false || DecryptPassword(accountExist.Password).Equals(""))
            {
                TempData["LoginFlag"] = "Invalid Email or Password";
                return RedirectToAction("Index");
            }
            else if (accountExist.Email.Equals(account.Email) && DecryptPassword(accountExist.Password).Equals(account.Password) && accountExist.Type == (int)EnumStatus.Admin)
            {
                HttpContext.Session.Set("admin", accountExist);
                return RedirectToAction("Index", "Home", new { Area = "Admin" });
            }

            HttpContext.Session.Set("account", accountExist);


            List<Bill> bill = _context.Bills.Where(b => b.AccId == accountExist.ID).ToList();
            List<BillViewModels> lstBillVM = new List<BillViewModels>();
            foreach (var item in bill)
            {
                BillViewModels billVM = new BillViewModels(item);
                lstBillVM.Add(billVM);
            }
            HttpContext.Session.Set("bill", lstBillVM);

            TempData["LoginSuccess"] = "Login Successfully";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("account");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Signout()
        {
            HttpContext.Session.Remove("account");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdatePassword(Account account)
        {
            Account acc = FacadeMaker.Instance.GetAccountById(account.ID);
            acc.Password = EncryptPassword(account.Password);

            FacadeMaker.Instance.UpdateAccount(account.ID, acc);
            TempData["updatePwdFlag"] = "Your password has been changed";

            return RedirectToAction("Profile", "Account");
        }

        [HttpPost]
        public IActionResult UpdateAccount(Account data)
        {
            Account newAcc = FacadeMaker.Instance.GetAccountById(data.ID);
            newAcc.Email = _context.Accounts.FirstOrDefault(p => p.ID == data.ID).Email;
            newAcc.Password = _context.Accounts.FirstOrDefault(p => p.ID == data.ID).Password;
            if (data.Name == null)
            {
                newAcc.Name = "";
            }
            else
            {
                newAcc.Name = data.Name;
            }
            if (data.Birth == null)
            {
                newAcc.Birth = "";
            }
            else
            {
                newAcc.Birth = data.Birth;

            }
            if (data.Phone == null)
            {
                newAcc.Phone = "";
            }
            else
            {
                newAcc.Phone = data.Phone;
            }
            newAcc.Avatar = _context.Accounts.FirstOrDefault(p => p.ID == data.ID).Avatar;
            newAcc.AvatarBase64 = "";
            newAcc.History = "";
            newAcc.Location = "";
            newAcc.Status = true;
            newAcc.Type = (int)EnumStatus.Customer;
            if (data.DeliAddress == null)
            {
                newAcc.DeliAddress = "";
            }
            else
            {
                newAcc.DeliAddress = data.DeliAddress;
            }
            newAcc.IP = "";
            newAcc.GoogleID = "";
            newAcc.FacebookID = "";

            FacadeMaker.Instance.UpdateAccount(data.ID, newAcc);
            TempData["updateAccFlag"] = "Your information has been saved";

            Account account = HttpContext.Session.Get<Account>("account");

            if (!account.Email.Equals(""))
            {
                account = newAcc;
                HttpContext.Session.Set("account", account);
            }

            return RedirectToAction("Profile", "Account");
        }

        [HttpPost]
        public IActionResult UpdateAvatar(Account account, IFormFile avatar)
        {
            Account newAcc = _context.Accounts.FirstOrDefault(p => p.ID == account.ID);

            try
            {
                Account accDel = new Account();
                accDel = newAcc;
                string uniqueFileName = string.Empty;
                if (avatar != null)
                {
                    if (accDel.Avatar != null)
                    {
                        string folder = "img/profile/";
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folder, accDel.Avatar);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        uniqueFileName = UploadImage(account, avatar);
                    }
                }

                if (account.Avatar != null)
                {
                    accDel.Avatar = uniqueFileName;
                }

                FacadeMaker.Instance.UpdateAccount(account.ID, newAcc);

                Account accAvatar = HttpContext.Session.Get<Account>("account");
                Account acc = HttpContext.Session.Get<Account>("acc");
                if (accAvatar == null)
                {
                    accAvatar = new Account();
                    HttpContext.Session.Set("acc", newAcc);
                }
                else
                {
                    acc = new Account();
                    HttpContext.Session.Set("account", newAcc);
                }
            }
            catch (Exception)
            {
                throw;
            }

            TempData["updateAvatar"] = "Update avatar successfully!";
            return RedirectToAction("Profile", "Account");
        }
        private string UploadImage(Account account, IFormFile avatar)
        {
            string uniqueFileName = string.Empty;
            if (avatar != null)
            {
                string folder = "img/profile/";
                uniqueFileName = Guid.NewGuid().ToString() + "-" + avatar.FileName;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder, uniqueFileName);

                avatar.CopyTo(new FileStream(serverFolder, FileMode.Create));
                account.Avatar = avatar.FileName;
            }
            return uniqueFileName;
        }
    }
}
