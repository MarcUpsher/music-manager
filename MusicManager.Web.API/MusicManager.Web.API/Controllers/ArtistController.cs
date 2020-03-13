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
	[Route("api/artists")]
	[ApiController]
	public class ArtistsController : ControllerBase
	{
		private readonly MusicManagerContext _context;

		public ArtistsController(MusicManagerContext context)
		{
			_context = context;
		}

		// GET: api/Artists
		[HttpGet]
		public async Task<ActionResult<List<ArtistDTO>>> GetArtists()
		{
			var artists = await QueryGetArtistsAsync();

			var artistDTO = ArtistMap.ListToDTO(artists);

			return artistDTO;
		}

		// GET: api/Artists/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ArtistDTO>> GetArtist(int id)
		{
			var artist = await QueryGetArtistAsync(id);

			if (artist == null)
			{
				return NotFound();
			}

			var artistDTO = ArtistMap.ToDTO(artist);

			return artistDTO;
		}

		// PUT: api/Albums/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://aka.ms/RazorPagesCRUD.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAlbum(int id, ArtistDTO artistDTO)
		{			
			if (id != artistDTO.Id)
			{
				return BadRequest();
			}

			Album album = new Album();

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
		public async Task<ActionResult<Album>> PostAlbum(AlbumDTO albumDTO)
		{
			Album album = new Album()
			{

			};


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

		[HttpGet]
		[Route("/api/artistsforfilter")]
		public async Task<ActionResult<List<FilterItemDTO>>> GetArtistForFilter()
		{
			var artists = await _context.Artists.Where(w => w.DateDeleted == null).ToListAsync();

			var artistsDTO = ArtistMap.ListToFilterItemDTO(artists);

			return artistsDTO;
		}

		private bool AlbumExists(int id)
		{
			return _context.Albums.Any(e => e.AlbumId == id);
		}

		public async Task<List<Artist>> QueryGetArtistsAsync()
		{
			var query = _context.Artists.AsQueryable();

			query = QueryGetArtistFilters(query);

			return await query.ToListAsync();
		}

		public async Task<Artist> QueryGetArtistAsync(int id)
		{
			var query = _context.Artists
				.Where(w => w.ArtistId == id).AsQueryable();

			query = QueryGetArtistFilters(query);

			return await query.FirstOrDefaultAsync();
		}

		public IQueryable<Artist> QueryGetArtistFilters(IQueryable<Artist> query)
		{
			return query.Where(w => w.DateDeleted == null);
		}
	}
}
