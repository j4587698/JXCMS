using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace JXCMS.Core.Themes
{
    public static class ThemeUtil
    {
        /// <summary>
        /// PC主题
        /// </summary>
        public const int PcTheme = 1;
        
        /// <summary>
        /// 手机主题
        /// </summary>
        public const int MobileTheme = 2;
        
        /// <summary>
        /// 自适应主题
        /// </summary>
        public const int AdaptiveTheme = 3;

        /// <summary>
        /// PC主题字段名称
        /// </summary>
        public const string PcThemeName = "PcTheme";

        /// <summary>
        /// 移动主题字段名称
        /// </summary>
        public const string MobileThemeName = "MobileTheme";
        
        public static List<ThemeConfig> ListThemes(string basePath)
        {
            var themes = Directory.GetDirectories(basePath);
            List<ThemeConfig> themeConfigs = new List<ThemeConfig>();
            foreach (var theme in themes)
            {
                var configPath = Path.Combine(theme, "theme.json");
                if (File.Exists(configPath))
                {
                    var themeConfig = JsonConvert.DeserializeObject<ThemeConfig>(File.ReadAllText(configPath));
                    themeConfig.FolderName = Path.GetFileName(theme);
                    if (themeConfig.ThemeType == MobileTheme)
                    {
                        if (TemplateViewLocationExpander.Mode == ThemeChangeMode.Domain || TemplateViewLocationExpander.Mode == ThemeChangeMode.Auto)
                        {
                            themeConfigs.Add(themeConfig);
                        }
                    }
                    else
                    {
                        themeConfigs.Add(themeConfig);
                    }
                    if (themeConfig.FolderName == TemplateViewLocationExpander.PcThemeName)
                    {
                        themeConfig.IsUsing = true;
                    }
                    else if (themeConfig.FolderName == TemplateViewLocationExpander.MobileThemeName)
                    {
                        themeConfig.IsUsing = true;
                    }
                }
            }

            return themeConfigs;
        }
    }
}