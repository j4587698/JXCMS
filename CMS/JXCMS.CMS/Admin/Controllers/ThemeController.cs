using JXCMS.CMS.Attribute;
using JXCMS.Core.Themes;
using Microsoft.AspNetCore.Mvc;

namespace JXCMS.CMS.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthentication]
    public class ThemeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            ViewBag.themes = ThemeUtil.ListThemes("./Views");
            return View();
        }
    }
}