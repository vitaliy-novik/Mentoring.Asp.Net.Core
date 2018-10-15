using Microsoft.Extensions.FileProviders;
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
	}
}
