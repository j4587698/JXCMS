using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JXCMS.Core
{
    public class JXCMSMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _installPath;

        public JXCMSMiddleware(RequestDelegate next, string installPath)
        {
            _next = next;
            _installPath = installPath;
        }

        public Task Invoke(HttpContext context)
        {
            if (!File.Exists("install.lock") && !context.Request.Path.Value.Contains(_installPath))
            {
                context.Response.Redirect(
                    $"{context.Request.Scheme}://{context.Request.Host}{_installPath}");
            }
            return _next(context);
        }
    }
}