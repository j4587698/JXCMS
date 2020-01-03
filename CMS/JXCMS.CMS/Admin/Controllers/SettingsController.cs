using JXCMS.CMS.Attribute;
using Microsoft.AspNetCore.Mvc;

namespace JXCMS.CMS.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthentication]
    public class SettingsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}