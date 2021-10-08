using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.Models;
using Core.DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Core_API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddDbContext<CompanyContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("AppConnectionString"));
			});

			services.AddScoped<IService<Department,int>, DepartmentService>();
			services.AddScoped<IService<Employee,int>,EmployeeService>();

			// Add the CORS service
			services.AddCors(options=> {
				options.AddPolicy("corspolicy", policy=> {
					policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
				});
			});


			// the method that will be used for the API Request Processing
			// define a schema changes for Property NAmes of the CLR Object
			services.AddControllers().AddJsonOptions(options=>{
				// supress the default JSON camelCase serialization for
				// // CLR Object properties
				options.JsonSerializerOptions.PropertyNamingPolicy = null;
			});



			// Swagger ENdPoints for Publishing Metadata

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core_API", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				// Swagger Middleware
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core_API v1"));
			}

			app.UseHttpsRedirection();

			// apply the CORS Policy in the HTTP Pipeline using Middleware
			app.UseCors("corspolicy");

			// Configure the Static File Middleware
			app.UseStaticFiles();
			// Configure the Static File Path to the Http Request Pipeline so that
			// it can be used for File Read/Write Operations
			app.UseStaticFiles(new StaticFileOptions() { 
			
				// set the file Provider Physical Path
			  FileProvider = new PhysicalFileProvider(
				    // Map the Current Hosting Environment to Access the
					// Storage Directory
				    Path.Combine(Directory.GetCurrentDirectory(), @"Storage")),
				// Configure the Path for the Directory in Http Request Pipeline
				// All file contained in Http Message will be put in this folder 
			        RequestPath = new Microsoft.AspNetCore.Http.PathString("/Storage")
			});


			app.UseRouting();

			app.UseAuthorization();
			// Map the request to API Request Processing
			app.UseEndpoints(endpoints =>
			{
				// Evaluate the Rpute Expression and map the Request to the corresponding
				// API Controller
				endpoints.MapControllers();
			});
		}
	}
}
