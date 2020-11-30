using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MyMiddleware
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var query = context.Request.Query["culture"];
            if (!string.IsNullOrEmpty(query))
            {
                var info = new CultureInfo(query);
                CultureInfo.CurrentCulture = info;
                CultureInfo.CurrentUICulture = info;
            }
            
            await (_next(context));
        }
    }

    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
        {
            //在管道中添加一个中间件
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}
