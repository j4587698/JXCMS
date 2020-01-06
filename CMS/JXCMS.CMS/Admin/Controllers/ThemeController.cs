using JXCMS.CMS.Attribute;
using JXCMS.CMS.Entity;
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

        public IActionResult ChangeTheme(string themeName, int themeType)
        {
            if (themeType == ThemeUtil.MobileTheme)
            {
                TemplateViewLocationExpander.MobileThemeName = themeName;
                var settingsEntity = SettingsEntity.Where(x => x.Name == ThemeUtil.MobileThemeName).First() ??
                                     new SettingsEntity {Name = ThemeUtil.MobileThemeName, Type = "Theme"};
                settingsEntity.Value = themeName;
                settingsEntity.Save();
            }
            else
            {
                TemplateViewLocationExpander.PcThemeName = themeName;
                var settingsEntity = SettingsEntity.Where(x => x.Name == ThemeUtil.PcThemeName).First() ??
                                     new SettingsEntity {Name = ThemeUtil.PcThemeName, Type = "Theme"};
                settingsEntity.Value = themeName;
                settingsEntity.Save();
            }
            return new JsonResult(new {code = 200, msg = "success"});
        }
    }
}