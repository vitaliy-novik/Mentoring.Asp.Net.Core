using Northwind.Core.Entities;
using System.Collections.Generic;

namespace Northwind.Web.ViewModels
{
	public class ProductsListViewModel
	{
		public IEnumerable<Product> Products { get; set; }
	}
}
