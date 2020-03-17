using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Domain.Services;
using MusicManager.Web.API.Domain.Models.DTO;
using AutoMapper;

namespace MusicManager.Web.API.Controllers
{
	[Route("api/genres")]
	[ApiController]
	public class GenreController : ControllerBase
	{
		private readonly IGenreService _genreService;
		private readonly IMapper _mapper;

		public GenreController(IGenreService genreService, IMapper mapper)
		{			
			_genreService = genreService;
			_mapper = mapper;
		}

		// GET: api/Genres
		[HttpGet]
		public async Task<IEnumerable<GenreDTO>> GetAsync()
		{
			var genres = await _genreService.ListActiveAsync();
			var genresDto = _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(genres);

			return genresDto;
		}

		[HttpGet("getfilter")]
		public async Task<IActionResult> GetAsyncForFilter()
		{
			var genres = await _genreService.ListActiveAsync();

			var artistsFilterDto = _mapper.Map<IEnumerable<Genre>, IEnumerable<FilterItemDTO>>(genres);

			return Ok(artistsFilterDto);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAsyncById(int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState.GetErrorMessages());
			
			var result = await _genreService.GetByIdAsync(id);

			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			var genreDTO = _mapper.Map<Genre, GenreDTO>(result.Genre);

			return Ok(genreDTO);
		}

		[HttpGet("doesgenreexist")]
		public async Task<IActionResult> GetAsyncByName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return Ok(false);
			}			

			var genre = await _genreService.GetByNameAsync(name);

			var result = genre != null ? true : false;

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> PostAsync([FromBody] GenrePostDTO genrePostDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState.GetErrorMessages());

			var genre = _mapper.Map<GenrePostDTO, Genre>(genrePostDto);

			genre.DateAdded = DateTime.Now;

			var result = await _genreService.SaveAsync(genre);

			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			var genreDto = _mapper.Map<Genre, GenreDTO>(result.Genre);

			return Ok(genreDto);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutAsync(int id, [FromBody] GenrePostDTO genrePostDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState.GetErrorMessages());

			var genre = _mapper.Map<GenrePostDTO, Genre>(genrePostDto);
			var result = await _genreService.UpdateAsync(id, genre);

			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			var genreDto = _mapper.Map<Genre, GenreDTO>(result.Genre);

			return Ok(genreDto);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var result = await _genreService.DeleteAsync(id);

			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			var genreDto = _mapper.Map<Genre, GenreDTO>(result.Genre);

			return Ok(genreDto);
		}
	}
}
