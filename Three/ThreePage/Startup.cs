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
using Three;
using Three.Services;

namespace ThreePage
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorPagesOptions(o =>
            {
                o.Conventions.ConfigureFilter(new Microsoft.AspNetCore.Mvc.IgnoreAntiforgeryTokenAttribute());
                
            });
            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();//json�ַ�����Сдԭ�����
            });
            //ע�����,����Ӧ�ó���������������ֻ����һ��ʵ�� 
            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            //services.AddSingleton<EmployeeService>();
            //ע��appsettings.json�����е�Three�µĽڵ�
            services.Configure<ThreeOptions>(_configuration.GetSection("Three"));

            //������ݷ��ʿ�������
            services.AddCors(c=>c.AddPolicy("any",p=>p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            
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
            //app.UseHttpsRedirection();

            //�����֤ �������UseEndpoints֮ǰ
            app.UseAuthentication();
            //·���м��
            app.UseRouting();
            //������ݷ��ʿ�������,����UseRouting���� , �ڿ������ϻ���razorPage ��д�����ԣ�EnableCors("any")
            app.UseCors();

            //�˵��м��
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

        }
    }
}
