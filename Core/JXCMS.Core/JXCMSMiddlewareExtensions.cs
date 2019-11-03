using Microsoft.AspNetCore.Builder;

namespace JXCMS.Core
{
    public static class JXCMSMiddlewareExtensions
    {
        /// <summary>
        /// 使用JXCMS中间件的扩展方法，初始化数据库，提供安装等
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="installPath">安装路径（第一次打开时使用）</param>
        /// <returns></returns>
        public static IApplicationBuilder UseJXCMS(this IApplicationBuilder builder, string installPath)
        {
            return builder.UseMiddleware<JXCMSMiddleware>(installPath);
        }
    }
}