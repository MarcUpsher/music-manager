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
	public class GenreRepository : BaseRepository, IGenreRepository
	{
		public GenreRepository(MusicManagerContext context) : base(context)
		{

		}

		public async Task<IEnumerable<Genre>> ListAsync()
		{
			return await _context.Genres.ToListAsync();
		}

		public async Task<IEnumerable<Genre>> ListActiveAsync()
		{
			return await _context.Genres
									.Where(w => w.DateDeleted == null)
									.Include(i => i.AlbumGenres)
									.ToListAsync();
		}

		public async Task AddAsync(Genre genre)
		{
			await _context.Genres.AddAsync(genre);
		}

		public async Task<Genre> FindByIdAsync(int id)
		{
			return await _context.Genres.FindAsync(id);
		}

		public async Task<Genre> FindByNameAsync(string name)
		{
			return await _context.Genres
				.Where(w => w.Name.ToLower() == name.ToLower() && w.DateDeleted == null)
				.FirstOrDefaultAsync();
		}

		public void Update(Genre genre)
		{
			_context.Genres.Update(genre);
		}

		public void Remove(Genre genre)
		{
			_context.Remove(genre);
		}
	}
}
