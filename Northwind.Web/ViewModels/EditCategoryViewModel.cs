using Microsoft.AspNetCore.Http;

namespace Northwind.Web.ViewModels
{
	public class EditCategoryViewModel
	{
		public int CategoryId { get; set; }

		public string CategoryName { get; set; }

		public string Description { get; set; }

		public IFormFile Picture { get; set; }
	}
}
