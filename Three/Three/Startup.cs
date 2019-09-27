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
            //services.AddMvc(); MVC�����й��ܣ���ȫ�棬����Ҳ�ܶ�
            services.AddControllersWithViews();//MVC����Ҫ���ܣ�web��վ����
            //services.AddControllers();//��ʹ��api ע��
            //services.AddRazorPages();//ʹ��RazorPageģʽ
            services.AddSingleton<IClock, ChinaClock>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                //��������������ж�
                app.UseDeveloperExceptionPage();
            }
            //��̬�ļ��м��
            app.UseStaticFiles();

            //ǿ����תHTTPS���м��
            app.UseHttpsRedirection();

            //�����֤ �������UseEndpoints֮ǰ
            app.UseAuthentication();
            //·���м��
            app.UseRouting();

            //�˵��м��
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                //endpoints.MapControllers();//attr����
                //endpoints.MapControllerRoute(
                //    "default",
                //    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
