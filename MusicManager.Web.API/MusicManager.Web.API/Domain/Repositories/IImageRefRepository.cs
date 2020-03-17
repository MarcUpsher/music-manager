using MusicManager.Web.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Domain.Repositories
{
	public interface IImageRefRepository
	{
		Task<ImageRef> FindByIdAsync(int id);
		Task AddAsync(ImageRef imageRef);
		void Update(ImageRef imageRef);		
	}
}
