using MusicManager.Web.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Domain.Repositories
{
	public interface IGenreRepository
	{
		Task<IEnumerable<Genre>> ListAsync();
		Task<IEnumerable<Genre>> ListActiveAsync();
		Task AddAsync(Genre genre);
		Task<Genre> FindByIdAsync(int id);
		Task<Genre> FindByNameAsync(string name);
		void Update(Genre genre);
		void Remove(Genre genre);
	}
}
