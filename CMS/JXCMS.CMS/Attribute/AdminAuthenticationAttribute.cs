using JXCMS.Core.Auth;

namespace JXCMS.CMS.Attribute
{
    public class AdminAuthenticationAttribute : BaseAuthorizeAttribute
    {
        public new static string LoginPath = "/Admin/Home/Login";

        public new static string AccessDeniedPath = "/Denied";
    }
}