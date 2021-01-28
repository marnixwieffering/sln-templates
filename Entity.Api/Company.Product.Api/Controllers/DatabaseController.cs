using Company.Product.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Company.Product.Controllers
{
	public class DatabaseController : BaseController
	{
		public DatabaseController(
			IUnitOfWork unitOfWork,
			ILogger<DatabaseController> logger)
			: base(unitOfWork, logger)
		{
		}

		[HttpGet("ping")]
		public bool Ping() => UnitOfWork.CanConnectToDatabase();
	}
}
