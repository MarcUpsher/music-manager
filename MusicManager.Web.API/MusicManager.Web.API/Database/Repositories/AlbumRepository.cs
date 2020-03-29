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
	public class AlbumRepository : BaseRepository, IAlbumRepository
	{
		public AlbumRepository(MusicManagerContext context) : base(context)
		{

		}

		public async Task<IEnumerable<Album>> ListAsync()
		{
			return await _context.Albums.ToListAsync();
		}

		public async Task<IEnumerable<Album>> ListActiveAsync()
		{
			return await _context.Albums									
									.Include(w => w.ImageRef)
									.Include(w => w.Tracks)
									.Include(w => w.AlbumGenres)
										.ThenInclude(w => w.Genre)
									.Where(w => w.DateDeleted == null)
									.Where(t => t.Tracks.Any(x => x.DateDeleted == null))
									.Where(t => t.AlbumGenres.Any(x => x.DateDeleted == null))
									.ToListAsync();
		}

		public async Task<IEnumerable<Album>> GetByArtistAsync(int id)
		{
			var albums = await ListActiveAsync();

			return albums.Where(w => w.ArtistId == id && w.DateDeleted == null).ToList();
		}

		public async Task AddAsync(Album Album)
		{
			await _context.Albums.AddAsync(Album);
		}

		public async Task<Album> FindByIdAsync(int id)
		{
			return await _context.Albums
									.Include(w => w.ImageRef)
									.Include(w => w.Tracks)
									.Include(w => w.AlbumGenres)
										.ThenInclude(w => w.Genre)
									.Include(w => w.Artist)
									.Where(w => w.AlbumId == id)
									.Where(t => t.Tracks.Any(x => x.DateDeleted == null))
									.Where(t => t.AlbumGenres.Any(x => x.DateDeleted == null))
									.FirstOrDefaultAsync();
		}

		public async Task<Album> FindByNameAsync(string name)
		{
			return await _context.Albums
				.Where(w => w.Name.ToLower() == name.ToLower() && w.DateDeleted == null)
				.FirstOrDefaultAsync();
		}

		public void Update(Album Album)
		{
			_context.Albums.Update(Album);
		}

		public void Remove(Album Album)
		{
			_context.Remove(Album);
		}
	}
}
