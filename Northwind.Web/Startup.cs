using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Northwind.Core.Interfaces;
using Northwind.Infrastructure;
using Northwind.Infrastructure.Repositories;
using Northwind.Web.Configuration;
using Northwind.Web.Logging;
using System;
using System.IO;

namespace Northwind.Web
{
	public class Startup
	{
		private const string NorthwindConnectionStringName = "Northwind";
		private IConfiguration configuration;

		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<IApplicationConfiguration, ApplicationConfiguration>();
			services.AddDbContext<NorthwindContext>(options => 
				options.UseSqlServer(this.configuration.GetConnectionString(NorthwindConnectionStringName)));
			services.AddScoped<IProductsRepository, ProductsRepository>();
			services.AddScoped<ICategoriesRepository, CategoriesRepository>();

			MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new AutoMapperProfile());
				mc.AddProfile(new WebMapperProfile());
			});

			IMapper mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);

			services.AddMvc();
		}

		public void Configure(
			IApplicationBuilder app, 
			IHostingEnvironment env, 
			ILoggerFactory loggerFactory,
			IConfiguration configuration)
		{
			loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"));
			var logger = loggerFactory.CreateLogger("FileLogger");
			logger.LogInformation("Application STARTED at {0}", DateTime.Now);
			logger.LogInformation("Maximum products per page: {0} \n ConnectionString: {1}",
				configuration["MaxProductsOnPage"],
				configuration.GetConnectionString(NorthwindConnectionStringName));

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/home/error");
			}

			app.UseStaticFiles();

			app.UseNodeModules(env.ContentRootPath);

			app.UseMvc(this.ConfigureRoutes);
		}

		private void ConfigureRoutes(IRouteBuilder routeBuilder)
		{
			routeBuilder.MapRoute("Images", "images/{id}", defaults: new { controller = "Category", action = "Image" });
			routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
		}
	}
}
