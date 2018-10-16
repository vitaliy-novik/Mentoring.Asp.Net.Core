using System.ComponentModel.DataAnnotations;

namespace Northwind.Web.ViewModels
{
	public class ProductViewModel
	{
		public int ProductId { get; set; }

		[Display(Name = "Name")]
		[Required, MaxLength(30)]
		public string ProductName { get; set; }

		[Display(Name = "Category")]
		[Required]
		public int CategoryId { get; set; }

		[Display(Name = "Price")]
		[Required, Range(typeof(decimal), "0", "1000")]
		public decimal UnitPrice { get; set; }

		[Display(Name = "Count")]
		[Required, Range(0, 1000)]
		public short UnitsInStock { get; set; }
	}
}
