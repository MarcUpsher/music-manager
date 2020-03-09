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
	[Route("api/album")]
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
			var albums = await _context.Albums
				.Include(i => i.Artist)
				.Include(i => i.ImageRef)
				.Include(i => i.AlbumGenres)
					.ThenInclude(it => it.Genre)
				.Include(i => i.Tracks)
				.ToListAsync();

			var albumsDTO = AlbumMap.ToDTO(albums);

			return albumsDTO;
		}

		// GET: api/Albums/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Album>> GetAlbum(int id)
		{
			var album = await _context.Albums.FindAsync(id);

			if (album == null)
			{
				return NotFound();
			}

			return album;
		}

		// PUT: api/Albums/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://aka.ms/RazorPagesCRUD.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAlbum(int id, Album album)
		{
			if (id != album.AlbumId)
			{
				return BadRequest();
			}

			_context.Entry(album).State = EntityState.Modified;

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
	}
}
