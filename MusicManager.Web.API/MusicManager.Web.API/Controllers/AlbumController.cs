using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicManager.Web.API.Database.Contexts;
using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Domain.Models.DTO;
using MusicManager.Web.API.Domain.Services;

namespace MusicManager.Web.API.Controllers
{
	[Route("api/albums")]
	[ApiController]
	public class AlbumController : ControllerBase
	{
		private readonly IAlbumService _albumService;
		private readonly IMapper _mapper;

		public AlbumController(IAlbumService albumService, IMapper mapper)
		{
			_albumService = albumService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IEnumerable<AlbumDTO>> GetAsync()
		{
			var albums = await _albumService.ListActiveAsync();
			var albumsDTO = _mapper.Map<IEnumerable<Album>, IEnumerable<AlbumDTO>>(albums);

			// Hack
			foreach (var albumDTO in albumsDTO)
			{				
				albumDTO.ImageUri = Helpers.GetImageUri(Request, albumDTO.ImageUri);
			}

			return albumsDTO;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAsyncById(int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState.GetErrorMessages());

			var result = await _albumService.GetByIdAsync(id);

			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			var albumDTO = _mapper.Map<Album, AlbumDTO>(result.Album);
			albumDTO.ImageUri = Helpers.GetImageUri(Request, albumDTO.ImageUri);			

			return Ok(albumDTO);
		}
	}
}
