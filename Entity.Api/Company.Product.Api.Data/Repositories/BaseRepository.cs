using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.QueryableExtensions;
using Company.Product.Core.Interfaces;
using Company.Product.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Company.Product.Data.Repositories
{
	public class BaseRepository<T, TDto> : IRepository<T, TDto>
		where T : IEntity
		where TDto : class
	{
		private readonly DbContext _context;
		private readonly AutoMapper.Mapper _mapper;

		protected BaseRepository(DbContext dbContext, AutoMapper.Mapper mapper)
		{
			_context = dbContext;
			_mapper = mapper;
		}

		public T GetById(Guid id) => _mapper.Map<T>(_context.Set<TDto>().Find(id));

		public IEnumerable<T> GetAll()
		{
			return _mapper.Map<IEnumerable<T>>(_context.Set<TDto>());
		}

		public int Count() => _context.Set<TDto>().Count();

		public IEnumerable<T> GetBatch<TResult>(
			int skip,
			int take,
			SortOrder sortOrder,
			Expression<Func<T, TResult>> orderBy
		)
		{
			var batch = _context.Set<TDto>()
				.Skip(skip)
				.Take(take);

			batch = sortOrder == SortOrder.Ascending
				? batch.OrderBy(
					_mapper.MapExpression<Expression<Func<TDto, bool>>>(orderBy)
				)
				: batch.OrderByDescending(
					_mapper.MapExpression<Expression<Func<TDto, bool>>>(orderBy)
				);

			return batch.ProjectTo<T>(_mapper.ConfigurationProvider).ToList();
		}

		public void Insert(T entity)
		{
			_context.Set<TDto>().Add(_mapper.Map<TDto>(entity));
			_context.SaveChanges();
		}

		public void InsertRange(IEnumerable<T> entities)
		{
			_context.AddRange(_mapper.Map<IEnumerable<TDto>>(entities));
			_context.SaveChanges();
		}

		public void Update(T entity)
		{
			_context.Set<TDto>().Update(_mapper.Map<TDto>(entity));
			_context.SaveChanges();
		}

		public void Upsert(T entity)
		{
			if (GetById(entity.Id) == null) Insert(entity);
			else Update(entity);
		}

		public IEnumerable<T> Query(Expression<Func<T, bool>> filter) =>
			_context.Set<TDto>()
				.Where(
					_mapper.MapExpression<Expression<Func<TDto, bool>>>(filter)
				)
				.ProjectTo<T>(
					_mapper.ConfigurationProvider
				)
				.ToList();

		public void Delete(T entity)
		{
			_context.Set<TDto>().Remove(_mapper.Map<TDto>(entity));
			_context.SaveChanges();
		}

		public void DeleteAll()
		{
			_context.RemoveRange(_mapper.Map<IEnumerable<TDto>>(GetAll()));
			_context.SaveChanges();
		}
	}
}
