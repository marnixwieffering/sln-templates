using AutoMapper;
using Company.Product.Data.Interfaces;
using Company.Product.Data.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Company.Product.Data
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext
			_context = new DtoModels(new DbContextOptions<DtoModels>()); // TODO: Create DbContext

		private readonly AutoMapper.Mapper _mapper = new(
			new MapperConfiguration(
				configure => { configure.AddProfile<EntityToDataTransferObjectMapperProfile>(); }
			)
		);

		public bool CanConnectToDatabase() => _context.Database.CanConnect();
	}
}
