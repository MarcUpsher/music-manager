using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Domain.Services
{
	public interface IAlbumService
	{
		Task<IEnumerable<Album>> ListAsync();
		Task<IEnumerable<Album>> ListActiveAsync();
		Task<IEnumerable<Album>> GetByArtistAsync(int id);
		Task<AlbumResponse> SaveAsync(Album album);
		Task<AlbumResponse> UpdateAsync(int id, Album album);
		Task<AlbumResponse> DeleteAsync(int id);
		Task<AlbumResponse> GetByIdAsync(int id);
		Task<Album> GetByNameAsync(string name);
	}
}
