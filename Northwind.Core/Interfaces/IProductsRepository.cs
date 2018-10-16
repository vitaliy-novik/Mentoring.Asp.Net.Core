using Northwind.Core.Entities;
using System.Collections.Generic;

namespace Northwind.Core.Interfaces
{
	public interface IProductsRepository
	{
		Product Get(int id);

		IEnumerable<Product> List(int count = 0);

		void Add(Product product);

		void Update(Product product);
	}
}
