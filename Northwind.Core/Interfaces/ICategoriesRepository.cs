using Northwind.Core.Entities;
using System.Collections.Generic;

namespace Northwind.Core.Interfaces
{
	public interface ICategoriesRepository
	{
		Category Get(int id);

		IEnumerable<Category> List();
	}
}
