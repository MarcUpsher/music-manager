using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Domain.Models.DTO;
using MusicManager.Web.API.Domain.Services;

namespace MusicManager.Web.API.Controllers
{
	[Route("api/artists")]
	[ApiController]
	public class ArtistController : ControllerBase
	{
		private readonly IArtistService _artistService;
		private readonly IAlbumService _albumService;
		private readonly IMapper _mapper;

		public ArtistController(IArtistService artistService, IAlbumService albumService, IMapper mapper)
		{
			_artistService = artistService;
			_albumService = albumService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IEnumerable<ArtistDTO>> GetAsync()
		{
			var artists = await _artistService.ListActiveAsync();
			var artistsDto = _mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistDTO>>(artists);

			// Hack
			foreach (var artistDTO in artistsDto)
			{
				artistDTO.ImageUri = Helpers.GetImageUri(Request, artistDTO.ImageUri);
			}

			return artistsDto;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAsyncById(int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState.GetErrorMessages());

			var result = await _artistService.GetByIdAsync(id);			

			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			var artistDTO = _mapper.Map<Artist, ArtistDTO>(result.Artist);
			artistDTO.ImageUri = Helpers.GetImageUri(Request, artistDTO.ImageUri);

			var albums = await _albumService.GetByArtistAsync(result.Artist.ArtistId);
			var albumsDTO = _mapper.Map<IEnumerable<Album>, List<AlbumDTO>>(albums);

			foreach (var albumDTO in albumsDTO)
			{
				albumDTO.ImageUri = Helpers.GetImageUri(Request, albumDTO.ImageUri);
			}
			
			artistDTO.Albums = albumsDTO;			

			return Ok(artistDTO);
		}

		[HttpPost]
		public async Task<IActionResult> PostAsync([FromForm] ArtistPostDTO artistPostDTO)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState.GetErrorMessages());

			var pathToSave = Path.Combine(@"wwwroot\images", Path.GetRandomFileName());
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), pathToSave);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await artistPostDTO.Image.CopyToAsync(fileStream);
			}

			

			var artist = _mapper.Map<ArtistPostDTO, Artist>(artistPostDTO);

			artist.DateAdded = DateTime.Now;

			var result = await _artistService.SaveAsync(artist);

			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			var artistDTO = _mapper.Map<Artist, ArtistDTO>(result.Artist);

			return Ok(artistDTO);
		}

		[HttpPut("{id}")]
		//[Route("/artists/{id:int}")]
		//[HttpPut("content/upload-image")]
		[HttpPut]
		public async Task<IActionResult> PutAsync([FromForm] ArtistPostDTO artistPostDTO, int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState.GetErrorMessages());

			var artist = _mapper.Map<ArtistPostDTO, Artist>(artistPostDTO);

			var pathToSave = Path.Combine(@"wwwroot\images", Path.GetRandomFileName());
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), pathToSave);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await artistPostDTO.Image.CopyToAsync(fileStream);
			}

			var result = await _artistService.UpdateAsync(id, artist);

			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			var artistDTO = _mapper.Map<Artist, ArtistDTO>(result.Artist);

			return Ok(artistDTO);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var result = await _artistService.DeleteAsync(id);

			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			var artistDTO = _mapper.Map<Artist, ArtistDTO>(result.Artist);

			return Ok(artistDTO);
		}
	}
}
