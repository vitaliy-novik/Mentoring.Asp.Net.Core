using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Core.Entities;
using System.Collections.Generic;

namespace Northwind.Web.ViewModels
{
	public class CreateProductViewModel : ProductViewModel
	{
		public IEnumerable<SelectListItem> Categories { get; set; }
	}
}
