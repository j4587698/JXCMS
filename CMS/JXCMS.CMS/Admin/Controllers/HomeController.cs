using System.Security.Claims;
using System.Threading.Tasks;
using JXCMS.CMS.Attribute;
using JXCMS.CMS.Entity;
using JXCMS.CMS.Utils;
using JXCMS.Core.Auth;
using JXCMS.Core.Encrypt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JXCMS.CMS.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthentication]
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password, string returnUrl = null)
        {
            password = SM3.GetSM3(password);
            var admin = AdminEntity.Where(x => x.UserName == username && x.Password == password).First();
            if (admin == null)
            {
                ViewBag.errormsg = "用户名或密码错误";
                return View();
            }
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Sid, admin.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, admin.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, Constants.AdminRoleName));
            await AuthExtension.LoginAsync(new AdminAuthenticationAttribute(), HttpContext,
                new ClaimsPrincipal(identity));
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}