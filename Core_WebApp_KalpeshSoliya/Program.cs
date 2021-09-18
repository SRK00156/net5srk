using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp_KalpeshSoliya
{
    public class Program
    {
        /// <summary>
		/// EWNtry Point to the Application
		/// </summary>
		/// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
		/// IHostBuilder: A COntract between the ASP.NET COre and the Currunt Hosting Process
		/// 1. Hosting Process: IIS, Docker, Apache, Nginx, Default is Kestral
		/// 2. Uses the 'Host' object to Manage the Lifecycle(?) of the ASP.NET Core App
		/// a. Manages===> All Service Objects (aka Dependencies), Middleware, Execution
		/// b. The Host uses the 'Startup' class for complete Application Management
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // Receive request from the Current Host
            Host.CreateDefaultBuilder(args)
                // Configure the application for the Execution
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Manage the Applicatrion using Startup class
                    // UserStartup() will invoke the Constructor of the 'Startup' class
                    // and inject the IConfiguration Interface
                    webBuilder.UseStartup<Startup>();
                });
    }
}
