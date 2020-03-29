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
	public class AlbumService : IAlbumService
	{
		private readonly IAlbumRepository _albumRepository;
		private readonly IUnitOfWork _unitOfWork;

		public AlbumService(IAlbumRepository AlbumRepository, IUnitOfWork unitOfWork)
		{
			_albumRepository = AlbumRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Album>> ListAsync()
		{
			return await _albumRepository.ListAsync();
		}

		public async Task<IEnumerable<Album>> ListActiveAsync()
		{
			return await _albumRepository.ListActiveAsync();
		}

		public async Task<IEnumerable<Album>> GetByArtistAsync(int id)
		{
			return await _albumRepository.GetByArtistAsync(id);
		}

		public async Task<AlbumResponse> GetByIdAsync(int id)
		{
			var existingAlbum = await _albumRepository.FindByIdAsync(id);

			if (existingAlbum == null)
			{
				return new AlbumResponse("Album not found.");
			}

			return new AlbumResponse(existingAlbum);
		}

		public async Task<Album> GetByNameAsync(string name)
		{
			return await _albumRepository.FindByNameAsync(name);
		}

		public async Task<AlbumResponse> SaveAsync(Album album)
		{
			try
			{
				foreach (var albumGenre in album.AlbumGenres)
				{
					albumGenre.DateAdded = DateTime.Now;
				}

				foreach (var track in album.Tracks)
				{
					track.DateAdded = DateTime.Now;
				}

				album.DateAdded = DateTime.Now;

				await _albumRepository.AddAsync(album);
				await _unitOfWork.CompleteAsync();

				var albumResponse = await GetByIdAsync(album.AlbumId);

				return new AlbumResponse(albumResponse.Album);
			} catch (Exception ex) {
				return new AlbumResponse($"An error occurred when saving the album, exception: {ex.Message}");
			}
		}

		public async Task<AlbumResponse> UpdateAsync(int id, Album Album)
		{
			var existingAlbum = await _albumRepository.FindByIdAsync(id);

			if (existingAlbum == null)
			{
				return new AlbumResponse("Album not found.");
			}

			existingAlbum.Name = Album.Name;
			existingAlbum.DateModified = DateTime.Now;

			try
			{
				_albumRepository.Update(existingAlbum);
				await _unitOfWork.CompleteAsync();

				return new AlbumResponse(existingAlbum);
			} catch (Exception ex)
			{
				return new AlbumResponse($"An error occurred when updating the album, exception: {ex.Message}");
			}
		}

		public async Task<AlbumResponse> DeleteAsync(int id)
		{
			var existingAlbum = await _albumRepository.FindByIdAsync(id);

			if (existingAlbum == null)
			{
				return new AlbumResponse("Album not found.");
			}

			existingAlbum.DateDeleted = DateTime.Now; // Setting the date deleted field ensures that data is never lost

			try
			{
				//_albumRepository.Remove(existingAlbum); 
				_albumRepository.Update(existingAlbum);
				await _unitOfWork.CompleteAsync();

				return new AlbumResponse(existingAlbum);
			}
			catch (Exception ex)
			{
				return new AlbumResponse($"An error occurred when deleting the album, exception: {ex.Message}");
			}
		}
	}
}
