using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace JXCMS.Core.Themes
{
    public class ThemeUtil
    {
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
                    themeConfigs.Add(themeConfig);
                }
            }

            return themeConfigs;
        }
    }
}