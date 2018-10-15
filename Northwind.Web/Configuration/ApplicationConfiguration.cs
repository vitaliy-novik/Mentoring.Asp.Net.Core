using Microsoft.Extensions.Configuration;

namespace Northwind.Web.Configuration
{
	public class ApplicationConfiguration : IApplicationConfiguration
	{
		private IConfiguration configuration;

		public ApplicationConfiguration(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public int MaxProductsOnPage  => this.configuration.GetValue<int>("MaxProductsOnPage");
	}
}
