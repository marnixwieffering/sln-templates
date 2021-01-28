using Company.Product.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Company.Product.Controllers
{
	public class ConnectionController : BaseController
	{
		public ConnectionController(
			IUnitOfWork unitOfWork,
			ILogger<ConnectionController> logger)
			: base(unitOfWork, logger)
		{
		}

		[HttpGet("ping")]
		public static string Ping() => "pong";
	}
}
