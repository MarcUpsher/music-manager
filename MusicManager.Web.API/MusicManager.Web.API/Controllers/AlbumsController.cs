using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicManager.DAL;
using MusicManager.Web.API.DTO;
using MusicManager.Web.API.MapToDTO;

namespace MusicManager.Web.API.Controllers
{
	[Route("api/albums")]
	[ApiController]
	public class AlbumsController : ControllerBase
	{
		private readonly MusicManagerContext _context;

		public AlbumsController(MusicManagerContext context)
		{
			_context = context;
		}

		// GET: api/Albums
		[HttpGet]
		public async Task<ActionResult<List<AlbumDTO>>> GetAlbums()
		{
			var albums = await QueryGetAlbumsAsync();

			var albumsDTO = AlbumMap.ListToDTO(albums);

			return albumsDTO;
		}

		// GET: api/Albums/5
		[HttpGet("{id}")]
		public async Task<ActionResult<AlbumDTO>> GetAlbum(int id)
		{
			var album = await QueryGetAlbumAsync(id);

			if (album == null)
			{
				return NotFound();
			}

			var albumDTO = AlbumMap.ToDTO(album);

			return albumDTO;
		}

		// PUT: api/Albums/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://aka.ms/RazorPagesCRUD.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAlbum(int id, AlbumPostDTO albumPostDTO)
		{
			if (id != int.Parse(albumPostDTO.Id))
			{
				return BadRequest();
			}

			Album existingAlbum = await QueryGetAlbumAsync(id);

			existingAlbum.ArtistId = int.Parse(albumPostDTO.ArtistId);
			existingAlbum.Name = albumPostDTO.Name;
			existingAlbum.ReleaseDate = albumPostDTO.ReleaseDate;
			existingAlbum.Summary = albumPostDTO.Summary;

			foreach (var existingTrack in existingAlbum.Tracks)
			{
				existingTrack.DateDeleted = DateTime.Now;
			}

			foreach (var track in albumPostDTO.Tracks)
			{
				var existingTrack = existingAlbum.Tracks.Where(w => w.TrackId == int.Parse(track.Id)).FirstOrDefault();

				if (existingTrack != null)
				{
					existingTrack.DateDeleted = null;
					existingTrack.Position = int.Parse(track.Position);
					existingTrack.Name = track.Name;
					existingTrack.Duration = int.Parse(track.Duration);
				}
				else
				{
					existingAlbum.Tracks.Add(new Track()
					{
						Position = int.Parse(track.Position),
						Name = track.Name,
						Duration = int.Parse(track.Duration)
					});
				}
			}

			// Update image URI

			_context.Entry(existingAlbum).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AlbumExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Albums
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://aka.ms/RazorPagesCRUD.
		[HttpPost]
		public async Task<ActionResult<Album>> PostAlbum(Album album)
		{
			_context.Albums.Add(album);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetAlbum", new { id = album.AlbumId }, album);
		}

		// DELETE: api/Albums/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Album>> DeleteAlbum(int id)
		{
			var album = await _context.Albums.FindAsync(id);
			if (album == null)
			{
				return NotFound();
			}

			_context.Albums.Remove(album);
			await _context.SaveChangesAsync();

			return album;
		}

		private bool AlbumExists(int id)
		{
			return _context.Albums.Any(e => e.AlbumId == id);
		}

		public async Task<List<Album>> QueryGetAlbumsAsync()
		{
			var query = _context.Albums.AsQueryable();

			query = QueryGetAlbumFilters(query);

			return await query.ToListAsync();
		}

		public async Task<Album> QueryGetAlbumAsync(int id)
		{
			var query = _context.Albums
				.Where(w => w.AlbumId == id).AsQueryable();

			query = QueryGetAlbumFilters(query);

			return await query.FirstOrDefaultAsync();
		}

		public IQueryable<Album> QueryGetAlbumFilters(IQueryable<Album> query)
		{
			return query.Where(w => w.DateDeleted == null)
				.Include(i => i.Artist)
				.Include(i => i.ImageRef)
				.Include(i => i.AlbumGenres)
					.ThenInclude(it => it.Genre)
				.Include(i => i.Tracks);
		}
	}
}
