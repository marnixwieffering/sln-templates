using Company.Product.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Company.Product.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public abstract class BaseController : Controller
	{
		protected BaseController(
			IUnitOfWork unitOfWork, ILogger logger)
		{
			UnitOfWork = unitOfWork;
			Logger = logger;
		}

		protected readonly IUnitOfWork UnitOfWork;
		protected ILogger Logger { get; }
		
	}
}
