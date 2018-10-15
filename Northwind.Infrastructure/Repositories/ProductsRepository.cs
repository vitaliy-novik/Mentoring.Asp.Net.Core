using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Core.Entities;
using Northwind.Core.Interfaces;
using Northwind.Infrastructure.Models;

namespace Northwind.Infrastructure.Repositories
{
	public class ProductsRepository : IProductsRepository
	{
		private DbContext dbContext;
		private IMapper mapper;

		public ProductsRepository(
			NorthwindContext dbContext,
			IMapper mapper)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
		}

		public Product Add(Product product)
		{
			this.dbContext.Set<Products>().Add(this.mapper.Map<Products>(product));
			this.dbContext.SaveChanges();

			return product;
		}

		public Product Get(int id)
		{
			Products product = this.dbContext.Set<Products>().Find(id);

			return this.mapper.Map<Product>(product);
		}

		public IEnumerable<Product> List(int count = 0)
		{
			IEnumerable<Products> products;
			if (count <= 0)
			{
				products = this.dbContext.Set<Products>().Include(p => p.Category).AsEnumerable();
			}
			else
			{
				products = this.dbContext.Set<Products>().Take(count).Include(p => p.Category).AsEnumerable();
			}

			return products.Select(p => this.mapper.Map<Product>(p));
		}
	}
}
