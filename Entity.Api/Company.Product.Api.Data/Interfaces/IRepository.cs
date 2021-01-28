using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Company.Product.Core.Interfaces;
using Microsoft.Data.SqlClient;

namespace Company.Product.Data.Interfaces
{
	public interface IRepository<T, TDto>
		where T : IEntity
		where TDto : class
	{
		T GetById(Guid id);
		IEnumerable<T> GetAll();

		IEnumerable<T> GetBatch<TResult>(
			int skip,
			int take,
			SortOrder sortOrder,
			Expression<Func<T, TResult>> orderBy
		);

		void Insert(T entity);
		void InsertRange(IEnumerable<T> entities);
		void Update(T entity);
		void Upsert(T entity);
		IEnumerable<T> Query(Expression<Func<T, bool>> filter);
		void Delete(T entity);
		void DeleteAll();
	}
}
