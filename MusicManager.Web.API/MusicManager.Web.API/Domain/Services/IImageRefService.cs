using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Domain.Services
{
	public interface IImageRefService
	{
		Task<ImageRefResponse> SaveAsync(ImageRef imageRef);
	}
}
