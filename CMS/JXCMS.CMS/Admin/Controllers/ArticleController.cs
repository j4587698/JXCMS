using JXCMS.CMS.Attribute;
using Microsoft.AspNetCore.Mvc;

namespace JXCMS.CMS.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthentication]
    public class ArticleController : Controller
    {
        // GET
        public IActionResult Index()
        {
            ViewBag.title = "所有文章";
            
            return View();
        }
    }
}