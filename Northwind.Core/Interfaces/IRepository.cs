using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Northwind.Core.Interfaces
{
	public interface IRepository<T>
	{
		T Get(int id);

		IEnumerable<T> List();

		IEnumerable<T> List(Expression<Func<T, bool>> predicate);

		void Add(T entity);

		void Delete(T entity);

		void Update(T entity);
	}
}
