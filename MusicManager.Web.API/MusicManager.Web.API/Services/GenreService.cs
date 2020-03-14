using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Domain.Repositories;
using MusicManager.Web.API.Domain.Services;
using MusicManager.Web.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Services
{
	public class GenreService : IGenreService
	{
		private readonly IGenreRepository _genreRepository;
		private readonly IUnitOfWork _unitOfWork;

		public GenreService(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
		{
			_genreRepository = genreRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Genre>> ListAsync()
		{
			return await _genreRepository.ListAsync();
		}

		public async Task<IEnumerable<Genre>> ListActiveAsync()
		{
			return await _genreRepository.ListActiveAsync();
		}

		public async Task<GenreResponse> SaveAsync(Genre genre)
		{
			try
			{
				await _genreRepository.AddAsync(genre);
				await _unitOfWork.CompleteAsync();

				return new GenreResponse(genre);
			} catch (Exception ex) {
				return new GenreResponse($"An error occurred when saving the genre, exception: {ex.Message}");
			}
		}

		public async Task<GenreResponse> UpdateAsync(int id, Genre genre)
		{
			var existingGenre = await _genreRepository.FindByIdAsync(id);

			if (existingGenre == null)
			{
				return new GenreResponse("Genre not found.");
			}

			existingGenre.Name = genre.Name;
			existingGenre.DateModified = DateTime.Now;

			try
			{
				_genreRepository.Update(existingGenre);
				await _unitOfWork.CompleteAsync();

				return new GenreResponse(existingGenre);
			} catch (Exception ex)
			{
				return new GenreResponse($"An error occurred when updating the genre, exception: {ex.Message}");
			}
		}

		public async Task<GenreResponse> DeleteAsync(int id)
		{
			var existingGenre = await _genreRepository.FindByIdAsync(id);

			if (existingGenre == null)
			{
				return new GenreResponse("Genre not found.");
			}

			existingGenre.DateDeleted = DateTime.Now; // Setting the date deleted field ensures that data is never lost

			try
			{
				//_genreRepository.Remove(existingGenre); 
				_genreRepository.Update(existingGenre);
				await _unitOfWork.CompleteAsync();

				return new GenreResponse(existingGenre);
			}
			catch (Exception ex)
			{
				return new GenreResponse($"An error occurred when deleting the genre, exception: {ex.Message}");
			}
		}
	}
}
