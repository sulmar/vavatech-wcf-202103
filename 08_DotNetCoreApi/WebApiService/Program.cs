using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Running Our Web App As A Service
        // https://dotnetcoretutorials.com/2019/12/21/hosting-an-asp-net-core-web-app-as-a-windows-service-in-net-core-3/
        // Install-Package Microsoft.Extensions.Hosting.WindowsServices
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("https://*:5010");   // https://docs.microsoft.com/pl-pl/aspnet/core/fundamentals/host/web-host?view=aspnetcore-5.0#server-urls
                })
                .UseWindowsService()   // <- Windows Service
            ;


        // Publikacja aplikacji
        // dotnet publish -r win-x64 -c Release

        // Utworzenie us³ugi
        // sc create TestService BinPath="C:\...\win-x64\publish\WebApiService.exe"
        // sc start TestService
        // sc stop TestService
        // sc delete TestService


    }
}
