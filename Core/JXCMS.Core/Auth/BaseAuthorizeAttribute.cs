using Microsoft.AspNetCore.Authorization;

namespace JXCMS.Core.Auth
{
    public abstract class BaseAuthorizeAttribute : AuthorizeAttribute
    {
        public static string LoginPath = "/Admin/Login";

        public static string AccessDeniedPath = "/Error/Forbidden";

        public BaseAuthorizeAttribute()
        {
            AuthenticationSchemes = GetScheme();
        }

        public string GetScheme()
        {
            return GetType().Name.Replace("AuthorizeAttribute", "");
        }
    }
}