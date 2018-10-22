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
		public IActionResult Edit(EditCategoryViewModel editModel)
		{
			if (ModelState.IsValid)
			{
				Category category = new Category
				{
					CategoryId = editModel.CategoryId,
					CategoryName = editModel.CategoryName,
					Description = editModel.Description
				};

				using (var memoryStream = new MemoryStream())
				{
					editModel.Picture.CopyTo(memoryStream);
					category.Picture = memoryStream.ToArray();
				}

				this.categoriesRepository.Update(category);

				return RedirectToAction(nameof(Index));
			}

			return View();
		}

		//[Route("images/{id}")]
		public IActionResult Image(int id)
		{
			MemoryStream image = this.categoriesRepository.GetImage(id);

			return new FileStreamResult(image, "image/jpeg");
		}
	}
}
