using Northwind.Core.Entities;
using System.Collections.Generic;

namespace Northwind.Web.ViewModels
{
	public class CategoriesListViewModel
	{
		public IEnumerable<Category> Categories { get; set; }
	}
}
