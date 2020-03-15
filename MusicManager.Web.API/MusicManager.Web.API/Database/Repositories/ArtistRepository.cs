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
	public class ArtistRepository : BaseRepository, IArtistRepository
	{
		public ArtistRepository(MusicManagerContext context) : base(context)
		{

		}

		public async Task<IEnumerable<Artist>> ListAsync()
		{
			return await _context.Artists.ToListAsync();
		}

		public async Task<IEnumerable<Artist>> ListActiveAsync()
		{
			return await _context.Artists
									.Where(w => w.DateDeleted == null)
									.Include(w => w.ImageRef)
									.Include(w => w.Albums)
									.ToListAsync();
		}

		public async Task AddAsync(Artist Artist)
		{
			await _context.Artists.AddAsync(Artist);
		}

		public async Task<Artist> FindByIdAsync(int id)
		{
			return await _context.Artists
									.Where(w => w.ArtistId == id)
									.Include(w => w.ImageRef)
									.Include(w => w.Albums)
									.FirstOrDefaultAsync();
		}

		public async Task<Artist> FindByNameAsync(string name)
		{
			return await _context.Artists
				.Where(w => w.Name.ToLower() == name.ToLower() && w.DateDeleted == null)
				.FirstOrDefaultAsync();
		}

		public void Update(Artist Artist)
		{
			_context.Artists.Update(Artist);
		}

		public void Remove(Artist Artist)
		{
			_context.Remove(Artist);
		}
	}
}
