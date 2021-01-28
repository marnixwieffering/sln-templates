using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Company.Product.Data
{
	public class DtoModels : DbContext
	{
		public DtoModels(DbContextOptions options)
			: base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options
				.UseSqlServer(
					ConfigurationManager.AppSettings
						.Get(
							"ConnectionString"
						)!
				)
				.UseQueryTrackingBehavior(
					QueryTrackingBehavior.NoTrackingWithIdentityResolution
				);

		// TODO: Create database model
	}
}
