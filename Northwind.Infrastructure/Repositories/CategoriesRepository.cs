using Microsoft.EntityFrameworkCore;
using Northwind.Core.Interfaces;
using Northwind.Infrastructure.Models;

namespace Northwind.Infrastructure.Repositories
{
	public class CategoriesRepository : Repository<Categories>, ICategoriesRepository
	{
		public CategoriesRepository(DbContext dbContext) : base(dbContext) { }
	}
}
