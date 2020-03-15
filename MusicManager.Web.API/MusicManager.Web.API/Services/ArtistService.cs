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
	public class ArtistService : IArtistService
	{
		private readonly IArtistRepository _artistRepository;
		private readonly IUnitOfWork _unitOfWork;

		public ArtistService(IArtistRepository ArtistRepository, IUnitOfWork unitOfWork)
		{
			_artistRepository = ArtistRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Artist>> ListAsync()
		{
			return await _artistRepository.ListAsync();
		}

		public async Task<IEnumerable<Artist>> ListActiveAsync()
		{
			return await _artistRepository.ListActiveAsync();
		}

		public async Task<ArtistResponse> GetByIdAsync(int id)
		{
			var existingArtist = await _artistRepository.FindByIdAsync(id);

			if (existingArtist == null)
			{
				return new ArtistResponse("Artist not found.");
			}

			return new ArtistResponse(existingArtist);
		}

		public async Task<Artist> GetByNameAsync(string name)
		{
			return await _artistRepository.FindByNameAsync(name);
		}

		public async Task<ArtistResponse> SaveAsync(Artist Artist)
		{
			try
			{
				await _artistRepository.AddAsync(Artist);
				await _unitOfWork.CompleteAsync();

				return new ArtistResponse(Artist);
			} catch (Exception ex) {
				return new ArtistResponse($"An error occurred when saving the artist, exception: {ex.Message}");
			}
		}

		public async Task<ArtistResponse> UpdateAsync(int id, Artist Artist)
		{
			var existingArtist = await _artistRepository.FindByIdAsync(id);

			if (existingArtist == null)
			{
				return new ArtistResponse("Artist not found.");
			}

			existingArtist.Name = Artist.Name;
			existingArtist.DateModified = DateTime.Now;

			try
			{
				_artistRepository.Update(existingArtist);
				await _unitOfWork.CompleteAsync();

				return new ArtistResponse(existingArtist);
			} catch (Exception ex)
			{
				return new ArtistResponse($"An error occurred when updating the artist, exception: {ex.Message}");
			}
		}

		public async Task<ArtistResponse> DeleteAsync(int id)
		{
			var existingArtist = await _artistRepository.FindByIdAsync(id);

			if (existingArtist == null)
			{
				return new ArtistResponse("Artist not found.");
			}

			existingArtist.DateDeleted = DateTime.Now; // Setting the date deleted field ensures that data is never lost

			try
			{
				//_artistRepository.Remove(existingArtist); 
				_artistRepository.Update(existingArtist);
				await _unitOfWork.CompleteAsync();

				return new ArtistResponse(existingArtist);
			}
			catch (Exception ex)
			{
				return new ArtistResponse($"An error occurred when deleting the Artist, exception: {ex.Message}");
			}
		}
	}
}
