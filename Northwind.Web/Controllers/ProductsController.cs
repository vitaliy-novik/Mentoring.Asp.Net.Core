using AutoMapper;
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
		private IMapper mapper;

		public ProductsController(
			IProductsRepository productsRepository,
			ICategoriesRepository categoriesRepository,
			IApplicationConfiguration applicationConfiguration,
			IMapper mapper)
		{
			this.productsRepository = productsRepository;
			this.categoriesRepository = categoriesRepository;
			this.appConfiguration = applicationConfiguration;
			this.mapper = mapper;
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

			ViewBag.CategoriesSelector = this.CreateCategoriesSelector();

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ProductViewModel product)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			Product newProduct = this.mapper.Map<Product>(product);

			this.productsRepository.Add(newProduct);

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			Product product = this.productsRepository.Get(id);
			ProductViewModel viewModel = this.mapper.Map<ProductViewModel>(product);
			ViewBag.CategoriesSelector = this.CreateCategoriesSelector();

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ProductViewModel product)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			Product newProduct = this.mapper.Map<Product>(product);

			this.productsRepository.Update(newProduct);

			return RedirectToAction(nameof(Index));
		}

		private IEnumerable<SelectListItem> CreateCategoriesSelector()
		{
			IEnumerable<Category> categories = this.categoriesRepository.List();
			return categories.Select(c => new SelectListItem
			{
				Value = c.CategoryId.ToString(),
				Text = c.CategoryName
			});
		}
	}
}
