using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MusicManager.Web.API.Controllers
{
	[Route("api")]
	[ApiController]
	public class BaseController : ControllerBase
	{		
		public BaseController(IWebHostEnvironment env)
		{			
		}

		[HttpGet]
		public IActionResult Index()
		{
			return File("index.html", "text/html");
		}
	}
}
