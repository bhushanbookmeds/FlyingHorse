using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class ContextHelper
    {
        private static IHttpContextAccessor HttpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        private static Uri GetAbsoluteUri()
        {
            var request = HttpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Path = request.Path.ToString(),
                Query = request.QueryString.ToString()
            };

            return uriBuilder.Uri;
        }

        private static Uri GetUri()
        {
            var request = HttpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder(request.Scheme, request.Host.Host);
            return uriBuilder.Uri;
        }

        // Similar methods for Url/AbsolutePath which internally call GetAbsoluteUri
        public static string GetAbsoluteUrl()
        {
            var url = GetAbsoluteUri();
            return url.AbsoluteUri;
        }
        public static string GetAbsolutePath()
        {
            var url = GetAbsoluteUri();
            return url.AbsolutePath;
        }

        public static string GetHostingUrl()
        {
            var url = GetUri();
            var request = HttpContextAccessor.HttpContext.Request;
            var path = request.Scheme + "://" + request.Host;
            // return url.AbsoluteUri;

            return path;
        }
    }
}