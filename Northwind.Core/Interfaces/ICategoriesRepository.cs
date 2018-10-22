using Northwind.Core.Entities;
using System.Collections.Generic;
using System.IO;

namespace Northwind.Core.Interfaces
{
	public interface ICategoriesRepository
	{
		Category Get(int id);

		IEnumerable<Category> List();

		MemoryStream GetImage(int id);
		void Update(Category category);
	}
}
