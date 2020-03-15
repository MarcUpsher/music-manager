using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Domain.Services
{
	public interface IArtistService
	{
		Task<IEnumerable<Artist>> ListAsync();
		Task<IEnumerable<Artist>> ListActiveAsync();
		Task<ArtistResponse> SaveAsync(Artist artist);
		Task<ArtistResponse> UpdateAsync(int id, Artist artist);
		Task<ArtistResponse> DeleteAsync(int id);
		Task<ArtistResponse> GetByIdAsync(int id);
		Task<Artist> GetByNameAsync(string name);
	}
}
