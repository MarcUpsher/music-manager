using MusicManager.Web.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Domain.Repositories
{
	public interface IArtistRepository
	{
		Task<IEnumerable<Artist>> ListAsync();
		Task<IEnumerable<Artist>> ListActiveAsync();
		Task AddAsync(Artist artist);
		Task<Artist> FindByIdAsync(int id);
		Task<Artist> FindByNameAsync(string name);
		void Update(Artist artist);
		void Remove(Artist artist);
	}
}
