using Microsoft.AspNetCore.Http;

namespace CoronaApi.Core
{
    public static class StaticUrl
    {
        public static string Get(IHttpContextAccessor httpContextAccessor, string folder, string fileName)
        {
            return
                $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}{Statics.FileBasePath}/{folder}/{fileName}";
        }
    }
}