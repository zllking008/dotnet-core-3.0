using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Three.Services;

namespace Three
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc(); MVC的所有功能，很全面，引入也很多
            services.AddControllersWithViews();//MVC的主要功能，web网站适用
            //services.AddControllers();//仅使用api 注册
            //services.AddRazorPages();//使用RazorPage模式
            services.AddSingleton<IClock, ChinaClock>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                //开发环境进入此判断
                app.UseDeveloperExceptionPage();
            }
            //静态文件中间件
            app.UseStaticFiles();

            //强制跳转HTTPS的中间件
            app.UseHttpsRedirection();

            //身份认证 必须放在UseEndpoints之前
            app.UseAuthentication();
            //路由中间件
            app.UseRouting();

            //端点中间件
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                //endpoints.MapControllers();//attr方法
                //endpoints.MapControllerRoute(
                //    "default",
                //    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
