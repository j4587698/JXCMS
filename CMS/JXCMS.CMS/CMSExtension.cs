using System.IO;
using JXCMS.CMS.Entity;
using JXCMS.Core.Plugin;
using JXCMS.Core.Themes;
using Microsoft.Extensions.DependencyInjection;

namespace JXCMS.CMS
{
    public static class CMSExtension
    {
        public static IServiceCollection AddCms(this IServiceCollection service)
        {
            if (File.Exists("install.lock"))
            {
                
            }
            return service;
        }
    }
}