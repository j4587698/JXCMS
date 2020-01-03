using JXCMS.CMS.Admin.Models;
using JXCMS.CMS.Entity;
using JXCMS.Core.Db;
using JXCMS.Core.Encrypt;
using Microsoft.AspNetCore.Mvc;

namespace JXCMS.CMS.Admin.Controllers
{
    [Area("Admin")]
    public class InstallController : Controller
    {
        // GET
        public IActionResult Index()
        {
            if (System.IO.File.Exists("install.lock"))
            {
                return RedirectToAction("Index", "Home", new {area = ""});
            }
            return View();
        }

        public JsonResult CheckInstallInfo(InstallModel installModel)
        {
            var ret = DbExtension.SetDb(installModel.ToDbConfig(), true);
            return new JsonResult(new {ret = ret.isSuccess, msg = ret.msg});
        }

        public IActionResult Finish(InstallModel installModel)
        {
            DbExtension.InstallDb(installModel.ToDbConfig());
            var admin = new AdminEntity();
            admin.UserName = installModel.AdminUser;
            admin.Password = SM3.GetSM3(installModel.AdminPass);
            admin.Insert();
            System.IO.File.Create("install.lock");
            return View();
        }
    }
}