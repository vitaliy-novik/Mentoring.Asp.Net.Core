using Microsoft.AspNetCore.Mvc;
using Northwind.Core.Entities;
using Northwind.Core.Interfaces;
using Northwind.Web.Configuration;
using Northwind.Web.ViewModels;
using System.Collections.Generic;

namespace Northwind.Web.Controllers
{
	public class ProductsController : Controller
	{
		private IProductsRepository productsRepository;
		private IApplicationConfiguration appConfiguration;

		public ProductsController(
			IProductsRepository productsRepository,
			IApplicationConfiguration applicationConfiguration)
		{
			this.productsRepository = productsRepository;
			this.appConfiguration = applicationConfiguration;
		}

		public IActionResult Index()
		{
			IEnumerable<Product> products =
				this.productsRepository.List(this.appConfiguration.MaxProductsOnPage);

			ProductsListViewModel viewModel = new ProductsListViewModel
			{
				Products = products
			};

			return View("Index", viewModel);
		}
	}
}
