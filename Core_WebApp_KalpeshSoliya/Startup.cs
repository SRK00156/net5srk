using Core_WebApp_KalpeshSoliya.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_DataAccess_KalpeshSoliya.Models;
using Core_DataAccess_KalpeshSoliya.Services;

namespace Core_WebApp_KalpeshSoliya
{
    public class Startup
    {
        /// <summary>
        /// IConfiguration: Used to Read the appsetting from the appsetting.json file
        /// e.g. ConnectionString (Database, Cache, etc)
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Accepts IServiceCollection, passed using the HostBuilder.
        /// IServiceCollection: Is a COntract used to Register, Load and Instantiate all application Depednencies using Default Dependency Injection Container 
        /// The IServiceCollection uses 'ServiceDescriptor' class to Discover,Load and Instatiate dependencies
        /// The ServiceDescriptor, 
        /// uses 'Singleton()' method to instantiate an object globally for the entire lifetime of the  application. 
        /// The 'Scopped()' method will be used to create a Statefull instance for a  Session, the object will be destroyed once the session is closed or terminated. 
        /// The 'Transient()' method will instantiate an object just for the current request, the object will be destroyed once the request is over.  
        /// The following objects are registered in Dependency Containers
        /// Databases Access (EF Core DbContext), Identity Objects (User,Roles, Policies,Token),
        /// Caching, Session, CORS, Authentication, Authorization, Custom Services for oure Domain workflow,  MVC Controllers and View Request Processing, API Controller Request Processing, Razor Views Request Processing, etc.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the ApplicationDbContext class in DI COntainer to CRUD 
            // for ASP.NET USers, Roles, etc.
            // Scopped Instntiation 
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // ASP.NET Core 5 , The Action Filter for rendering Database Error Page
            // if the Database connectivity is failed because oif any reason
            services.AddDatabaseDeveloperPageExceptionFilter();

            // USed to Connect to the Database that contains ASP.NET Users and Roles Informations
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Register the CUstom Objects aka Services in Dependency Injection COntainer
            // 1. Register the DbCOntext 
            services.AddDbContext<SRKCompContext>(options => {
                // Ask the Db COntext to Read COnnection String from appsettings.json
                options.UseSqlServer(Configuration.GetConnectionString("AppConnectionString"));
            });
            // 2. Register all services as Scopped
            services.AddScoped<IService<Dept, int>, DeptService>();
            services.AddScoped<IService<Emp, int>, EmpService>();
            services.AddScoped<IServiceEmpDept, EmpService>();


            // Request Processing for ASP.NET Core 5 MVC Controllers with Views and API Controllers
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
		/// The Method that Actually Manages HTTP Request Pipeline by using Middlewares
		/// Middlewares are replacement for Http Module and Http Handlers 
		/// 1. IWebHostEnvironment: COntract managed by Host Builder, used to map the Environment settings e.g. Dev. Test, Production to the CUrrent Host 
		/// 2. IApplicationBuilder: Contract, used to integarte or load middleweares in the HTTP Request Pipeline 
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Standard eror page provided by ASP.NET COre during develpment
                app.UseDeveloperExceptionPage();
                // Provide Migration Page to generate Database from CLR Classes 
                // in case of ASP.NET COre Identity
                app.UseMigrationsEndPoint();
            }
            else
            {
                // If Production or Stading, the Custom Error Page
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // The HTTP Enhanced Security used in case of SSL Certificates
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            // by defaultm, the contents of wwwroot folder will read and with its references
            // files from wwwroot folder will be added in HTTP Response
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Map the CUrrent Route Request for MVC to Home COntroller and Its Index Method
            app.UseEndpoints(endpoints =>
            {
                // used only for MVC COntrollers and View
                // controller: Ciontroller Class Name (without using Controller Word in Class)
                // action: the action name
                // id?: Optionl Paramerter
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // Map HTTP Request for Razor Views (new MVC Views)
                // e.g. All Identioty Views like Register, Login , etc.
                endpoints.MapRazorPages();
            });
        }
    }
}
