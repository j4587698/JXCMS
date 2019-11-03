namespace Microsoft.AspNetCore.Mvc
{
    public static class UrlExtensions
    {
        public static string ContentAdmin(this IUrlHelper url, string path)
        {
            return url.Content("~/Admin/Content/" + path);
        }
    }
}