using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Core.Entities;
using Northwind.Core.Interfaces;
using Northwind.Web.Configuration;
using Northwind.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Web.Controllers
{
	public class ProductsController : Controller
	{
		private IProductsRepository productsRepository;
		private ICategoriesRepository categoriesRepository;
		private IApplicationConfiguration appConfiguration;

		public ProductsController(
			IProductsRepository productsRepository,
			ICategoriesRepository categoriesRepository,
			IApplicationConfiguration applicationConfiguration)
		{
			this.productsRepository = productsRepository;
			this.categoriesRepository = categoriesRepository;
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

		[HttpGet]
		public IActionResult Create()
		{
			IEnumerable<Category> categories = this.categoriesRepository.List();
			var viewModel = new CreateProductViewModel
			{
				Categories = categories.Select(c => new SelectListItem
				{
					Value = c.CategoryId.ToString(),
					Text = c.CategoryName
				})
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ProductViewModel product)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var newProduct = new Product
			{
				ProductName = product.ProductName,
				UnitPrice = product.UnitPrice,
				UnitsInStock = product.UnitsInStock,
				CategoryId = product.CategoryId
			};

			this.productsRepository.Add(newProduct);

			return RedirectToAction(nameof(Index));
		}
	}
}
