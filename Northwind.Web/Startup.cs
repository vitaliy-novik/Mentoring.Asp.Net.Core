using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Core.Interfaces;
using Northwind.Infrastructure;
using Northwind.Infrastructure.Repositories;
using Northwind.Web.Configuration;

namespace Northwind.Web
{
	public class Startup
	{
		private IConfiguration configuration;

		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IApplicationConfiguration, ApplicationConfiguration>();
			services.AddDbContext<NorthwindContext>(options => 
				options.UseSqlServer(this.configuration.GetConnectionString("Northwind")));
			services.AddScoped<IProductsRepository, ProductsRepository>();
			services.AddScoped<ICategoriesRepository, CategoriesRepository>();

			MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new AutoMapperProfile());
			});

			IMapper mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);

			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			app.UseNodeModules(env.ContentRootPath);

			app.UseMvc(this.ConfigureRoutes);
		}

		private void ConfigureRoutes(IRouteBuilder routeBuilder)
		{
			routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
		}
	}
}
