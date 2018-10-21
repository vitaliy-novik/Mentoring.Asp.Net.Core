using Microsoft.AspNetCore.Mvc;
using Northwind.Core.Entities;
using Northwind.Core.Interfaces;
using Northwind.Web.ViewModels;
using System.Collections.Generic;
using System.IO;

namespace Northwind.Web.Controllers
{
	public class CategoryController : Controller
	{
		private ICategoriesRepository categoriesRepository;

		public CategoryController(ICategoriesRepository categoriesRepository)
		{
			this.categoriesRepository = categoriesRepository;
		}

		public IActionResult Index()
		{
			IEnumerable<Category> categories = this.categoriesRepository.List();
			var viewModel = new CategoriesListViewModel
			{
				Categories = categories
			};

			return View(viewModel);
		}

		public IActionResult Details(int id)
		{
			Category category = this.categoriesRepository.Get(id);

			return View(category);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			Category category = this.categoriesRepository.Get(id);

			return View(category);
		}

		[HttpPost]
		public IActionResult Edit(CategoryViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			RedirectToAction(nameof(Index));
		}

		[Route("images/{id}")]
		public IActionResult Image(int id)
		{
			MemoryStream image = this.categoriesRepository.GetImage(id);

			return new FileStreamResult(image, "image/jpeg");
		}
	}
}
