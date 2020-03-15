using MusicManager.Web.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Domain.Repositories
{
	public interface IAlbumRepository
	{
		Task<IEnumerable<Album>> ListAsync();
		Task<IEnumerable<Album>> ListActiveAsync();
		Task<IEnumerable<Album>> GetByArtistAsync(int id);
		Task AddAsync(Album album);
		Task<Album> FindByIdAsync(int id);
		Task<Album> FindByNameAsync(string name);
		void Update(Album album);
		void Remove(Album album);
	}
}
