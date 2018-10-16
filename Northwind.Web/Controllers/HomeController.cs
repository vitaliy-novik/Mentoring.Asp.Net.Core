using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Northwind.Web.Controllers
{
	public class HomeController : Controller
	{
		private ILogger logger;

		public HomeController(ILogger logger)
		{
			this.logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Error()
		{
			var error = this.HttpContext.Features.Get<IExceptionHandlerFeature>().Error;

			return View();
		}
	}
}
