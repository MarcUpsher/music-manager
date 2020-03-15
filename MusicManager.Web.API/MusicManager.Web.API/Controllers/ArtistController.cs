using System;
using System.Collections.Generic;
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
	}
}
