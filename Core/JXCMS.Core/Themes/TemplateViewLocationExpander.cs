using System.Collections.Generic;
using System.Linq;
using DeviceDetectorNET.Parser.Device;
using JXCMS.Core.Extensions;
using Microsoft.AspNetCore.Mvc.Razor;

namespace JXCMS.Core.Themes
{
    public class TemplateViewLocationExpander : IViewLocationExpander
    {
        /// <summary>
        /// PC主题，不切换主题与自适应主题同样使用此主题
        /// </summary>
        public static string PcThemeName = "Default";

        /// <summary>
        /// 手机版主题
        /// </summary>
        public static string MobileThemeName = "Mobile";

        /// <summary>
        /// 主题切换方式，默认为不切换
        /// </summary>
        public static ThemeChangeMode Mode { get; set; } = ThemeChangeMode.None;

        /// <summary>
        /// 手机版域名
        /// </summary>
        public static string MobileDomain;
        
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (!context.AreaName.IsNullOrEmpty())
            {
                return viewLocations;
            }
            var themeName = context.Values["template"] ?? PcThemeName;
            string[] locations = { "/Views/" + themeName + "/{1}/{0}.cshtml", "/Views/" + themeName + "/{0}.cshtml", "/Views/" + themeName + "/Shared/{0}.cshtml", "/Views/Shared/{0}.cshtml" };
            return locations.Union(viewLocations.Where(x => !x.StartsWith("/Views/")));
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            switch (Mode)
            {
                case ThemeChangeMode.None:
                case ThemeChangeMode.Adaptive:
                    context.Values["template"] = PcThemeName;
                    break;
                case ThemeChangeMode.Auto:
                    MobileParser mobileParser = new MobileParser();
                    mobileParser.SetUserAgent(context.ActionContext.HttpContext.Request.Headers["User-Agent"]);
                    var result = mobileParser.Parse();
                    if ( result.Success)
                    {
                        context.Values["template"] = MobileThemeName;
                    }
                    else
                    {
                        context.Values["template"] = PcThemeName;
                    }
                    break;
                case ThemeChangeMode.Domain:
                    if (context.ActionContext.HttpContext.Request.Host.Host == MobileDomain)
                    {
                        context.Values["template"] = MobileThemeName;
                    }
                    else
                    {
                        context.Values["template"] = PcThemeName;
                    }
                    break;
            }
            
        }
    }
}