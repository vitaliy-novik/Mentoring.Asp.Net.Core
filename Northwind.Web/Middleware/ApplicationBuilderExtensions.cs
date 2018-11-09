using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Northwind.Web.Middleware;
using System.IO;

namespace Microsoft.AspNetCore.Builder
{
	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder UseNodeModules(
			this IApplicationBuilder appBuilder,
			string root)
		{
			var path = Path.Combine(root, "node_modules");
			var fileProvider = new PhysicalFileProvider(path);
			var options = new StaticFileOptions();
			options.RequestPath = "/node_modules";
			options.FileProvider = fileProvider;

			appBuilder.UseStaticFiles(options);

			return appBuilder;
		}


        public static IApplicationBuilder UseImageCaching(
            this IApplicationBuilder appBuilder,
            string cacheDirectory,
            int maxImages,
            int maxTimeInSeconds)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), cacheDirectory);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

			appBuilder.UseWhen(
				context => context.Request.Path.StartsWithSegments(new PathString("/images")), 
				app => app.UseMiddleware<ImageCachingMiddleware>(path, maxImages));

            return appBuilder;
        }
    }
}
