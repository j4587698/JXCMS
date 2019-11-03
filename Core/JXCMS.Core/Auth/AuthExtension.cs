using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace JXCMS.Core.Auth
{
    public static class AuthExtension
    {
        public static IServiceCollection AddJXAuth(this IServiceCollection services)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x =>
                x.GetTypes().Where(y => y.BaseType == typeof(BaseAuthorizeAttribute)));
            var builder = services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
            foreach (var type in types)
            {
                builder.AddCookie(type.Name.Replace("AuthorizeAttribute", ""), o =>
                    {
                        o.LoginPath = type.GetField("LoginPath", BindingFlags.Static|BindingFlags.Public).GetValue(type) as string;
                        o.AccessDeniedPath =
                            type.GetField("AccessDeniedPath", BindingFlags.Static | BindingFlags.Public).GetValue(type) as string;
                    });
            }
            
            return services;
        }

        public static async Task LoginAsync(BaseAuthorizeAttribute authorize, HttpContext httpContext, ClaimsPrincipal claims)
        {
            await httpContext.SignInAsync(authorize.GetScheme(), claims);
        }
    }
}