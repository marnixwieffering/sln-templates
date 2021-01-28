using System;
using System.Linq.Expressions;
using Company.Product.Core.Interfaces;
using Company.Product.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Company.Product.Controllers
{
	public class EntityController<T, TDto> : BaseController
		where T : IEntity
		where TDto : class
	{
		public EntityController(
			IUnitOfWork unitOfWork,
			IRepository<T, TDto> repository,
			ILogger logger
		)
			: base(unitOfWork, logger)
		{
			Repository = repository;
		}

		private IRepository<T, TDto> Repository { get; }

		[HttpGet("{id}")]
		public IActionResult Get(Guid id)
		{
			try
			{
				var entity = Repository.GetById(id);
				if (entity == null) return NotFound();
				return Ok(entity);
			}
			catch (Exception exception)
			{
				Logger.LogCritical(exception.Message);
				return StatusCode(500, exception.Message);
			}
		}

		[HttpGet("all")]
		public IActionResult GetAll()
		{
			try
			{
				var entities = Repository.GetAll();
				return Ok(entities);
			}
			catch (Exception exception)
			{
				Logger.LogCritical(exception.Message);
				return StatusCode(500, exception.Message);
			}
		}

		[HttpGet("batch")]
		public IActionResult GetBatch<TResult>(
			int skip,
			int take,
			SortOrder sortOrder,
			Expression<Func<T, TResult>> orderBy
		)
		{
			try
			{
				var entities = Repository.GetBatch(
					skip,
					take,
					sortOrder,
					orderBy
				);
				return Ok(entities);
			}
			catch (Exception exception)
			{
				Logger.LogCritical(exception.Message);
				return StatusCode(500, exception.Message);
			}
		}

		[HttpPost]
		public IActionResult Post(T entity)
		{
			try
			{
				Repository.Upsert(entity);
				return StatusCode(204);
			}
			catch (Exception exception)
			{
				Logger.LogCritical(exception.Message);
				return StatusCode(500, exception.Message);
			}
		}

		[HttpDelete]
		public IActionResult Delete(T entity)
		{
			try
			{
				if (Repository.GetById(entity.Id) == null) return NotFound();
				Repository.Delete(entity);
				return StatusCode(204);
			}
			catch (Exception exception)
			{
				Logger.LogCritical(exception.Message);
				return StatusCode(500, exception.Message);
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id)
		{
			try
			{
				var entity = Repository.GetById(id);
				if (entity == null) return NotFound();
				Repository.Delete(entity);
				return StatusCode(204);
			}
			catch (Exception exception)
			{
				Logger.LogCritical(exception.Message);
				return StatusCode(500, exception.Message);
			}
		}

		[HttpDelete("all")]
		public IActionResult Delete()
		{
			try
			{
				Repository.DeleteAll();
				return StatusCode(204);
			}
			catch (Exception exception)
			{
				Logger.LogCritical(exception.Message);
				return StatusCode(500, exception.Message);
			}
		}
	}
}
