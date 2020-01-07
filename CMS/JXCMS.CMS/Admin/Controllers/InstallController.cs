using System.Collections.Generic;
using FreeSql;
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
            var settings = new List<SettingsEntity>();
            settings.Add(new SettingsEntity()
            {
                Name = "title",
                Value = "JXCMS",
                Type = "Settings"
            });
            settings.Add(new SettingsEntity()
            {
                Name = "logo",
                Value = "/images/Logo.png",
                Type = "Settings"
            });
            settings.Add(new SettingsEntity()
            {
                Name = "keyword",
                Value = "JXCMS,急速开发CMS",
                Type = "Settings"
            });
            settings.Add(new SettingsEntity()
            {
                Name = "description",
                Value = "JXCMS是一款快速开发的CMS程序",
                Type = "Settings"
            });
            settings.Add(new SettingsEntity()
            {
                Name = "copyright",
                Value = "JXCMS 2019",
                Type = "Settings"
            });
            settings.Add(new SettingsEntity()
            {
                Name = "icp",
                Value = "",
                Type = "Settings"
            });
            BaseEntity.Orm.Insert(settings).ExecuteInserted();
            System.IO.File.Create("install.lock");
            return View();
        }
    }
}