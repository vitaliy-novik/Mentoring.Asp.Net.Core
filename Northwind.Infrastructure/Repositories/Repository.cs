using Microsoft.EntityFrameworkCore;
using Northwind.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Northwind.Infrastructure.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly DbContext _dbContext;

		public Repository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public virtual T Get(int id)
		{
			return _dbContext.Set<T>().Find(id);
		}

		public virtual IEnumerable<T> List()
		{
			return _dbContext.Set<T>();
		}

		public virtual IEnumerable<T> List(Expression<Func<T, bool>> predicate)
		{
			return _dbContext.Set<T>().Where(predicate);
		}

		public void Add(T entity)
		{
			_dbContext.Set<T>().Add(entity);
			_dbContext.SaveChanges();
		}

		public void Update(T entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			_dbContext.SaveChanges();
		}

		public void Delete(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			_dbContext.SaveChanges();
		}
	}
}
