using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Three.Services;

namespace Three
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            //var three = _configuration["Three:BoldDepartmentEmployeeCountThreshold"];直接取appsettings.json里面的值，
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc(); MVC的所有功能，很全面，引入也很多
            services.AddControllersWithViews();//MVC的主要功能，web网站适用
            //services.AddControllers();//仅使用api 注册
            //services.AddRazorPages();//使用RazorPage模式
            //services.AddSingleton<IClock, ChinaClock>();

            //注入服务,整个应用程序生命周期以内只创建一个实例 
            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();

            //注入appsettings.json配置中的Three下的节点
            services.Configure<ThreeOptions>(_configuration.GetSection("Three"));

        }
        //public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    //开发环境进入
        //}
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //env.IsProduction();
            //env.IsStaging();
            //env.IsEnvironment("OK");
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
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});

                //endpoints.MapControllers();//attr方法
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Department}/{action=Index}/{id?}");
            });
        }
    }
}
