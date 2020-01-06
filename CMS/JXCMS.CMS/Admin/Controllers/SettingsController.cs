using JXCMS.CMS.Attribute;
using JXCMS.CMS.Entity;
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
            ViewBag.title = "系统设置";
            ViewBag.settings = SettingsEntity.Where(x => x.Type == "Settings").ToList();
            return View();
        }
    }
}