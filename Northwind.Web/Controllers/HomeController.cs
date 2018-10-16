using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Web.ViewModels;

namespace Northwind.Web.Controllers
{
	public class HomeController : Controller
	{
		private ILogger logger;

		public HomeController(ILogger<HomeController> logger)
		{
			this.logger = logger;
		}

		public IActionResult Index()
		{
			throw new System.Exception();

			return View();
		}

		public IActionResult Error()
		{
			var error = this.HttpContext.Features.Get<IExceptionHandlerFeature>().Error;

			this.logger.LogError(error.ToString());

			ErrorViewModel viewModel = new ErrorViewModel
			{
				RequestId = this.HttpContext.TraceIdentifier
			};

			return View(viewModel);
		}
	}
}
