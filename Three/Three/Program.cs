using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Three
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //.ConfigureAppConfiguration((context,configBuilder)=> {
            //    configBuilder.Sources.Clear();//清理所有其他源配置json
            //    configBuilder.AddJsonFile("myconfig.json");//注入自定义 myconfig.json
            //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.ConfigureAppConfiguration();
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseStartup(typeof(Program));//引用程序集
                    //webBuilder.UseKestrel();
                });
    }
}
