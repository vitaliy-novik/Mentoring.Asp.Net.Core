using Microsoft.AspNetCore.Mvc;
using Northwind.Core.Entities;
using Northwind.Core.Interfaces;
using Northwind.Web.ViewModels;
using System.Collections.Generic;

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
	}
}
