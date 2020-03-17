using System;
using System.Collections.Generic;
using System.IO;
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

		[HttpGet("doesalbumexist")]
		public async Task<IActionResult> GetAsyncByName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return Ok(false);
			}

			var artist = await _albumService.GetByNameAsync(name);

			var result = artist != null ? true : false;

			return Ok(result);
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

		[HttpPost]
		public async Task<IActionResult> PostAsync([FromForm] AlbumPostDTO albumPostDTO)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState.GetErrorMessages());

			var image = albumPostDTO.Image;

			var fileName = $"{Path.GetRandomFileName()}{Path.GetExtension(image.FileName)}";
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images", fileName);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await image.CopyToAsync(fileStream);
			}

			var album = _mapper.Map<AlbumPostDTO, Album>(albumPostDTO);

			album.ImageRef = new ImageRef()
			{
				URI = $"images/{fileName}",
				DateAdded = DateTime.Now
			};

			var result = await _albumService.SaveAsync(album);

			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			var albumDTO = _mapper.Map<Album, AlbumDTO>(result.Album);

			return Ok(albumDTO);
		}
	}
}
