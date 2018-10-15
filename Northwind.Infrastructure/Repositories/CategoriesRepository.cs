using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Core.Entities;
using Northwind.Core.Interfaces;
using Northwind.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Infrastructure.Repositories
{
	public class CategoriesRepository : ICategoriesRepository
	{
		private DbContext dbContext;
		private IMapper mapper;

		public CategoriesRepository(
			NorthwindContext dbContext,
			IMapper mapper)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
		}

		public Category Get(int id)
		{
			Categories category = this.dbContext.Set<Categories>().Find(id);

			return this.mapper.Map<Category>(category);
		}

		public IEnumerable<Category> List()
		{
			var categories = this.dbContext.Set<Categories>().AsEnumerable();

			return categories.Select(c => this.mapper.Map<Category>(c));
		}
	}
}
