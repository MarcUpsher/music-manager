using MusicManager.Web.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MusicManager.Web.API.Database.Repositories
{
	public class ImageRefRepository : BaseRepository, IImageRefRepository
	{
		public ImageRefRepository(MusicManagerContext context) : base(context)
		{

		}

		public async Task<ImageRef> FindByIdAsync(int id)
		{
			return await _context.ImageRefs.FindAsync(id);
		}

		public async Task AddAsync(ImageRef imageRef)
		{
			await _context.ImageRefs.AddAsync(imageRef);
		}

		public void Update(ImageRef imageRef)
		{
			_context.ImageRefs.Update(imageRef);
		}
	}
}
