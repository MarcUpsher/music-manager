using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicManager.Web.API.Database.Contexts;
using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Domain.Models.DTO;

namespace MusicManager.Web.API.Controllers
{
	[Route("api/albums")]
	[ApiController]
	public class AlbumController : ControllerBase
	{
	}
}
